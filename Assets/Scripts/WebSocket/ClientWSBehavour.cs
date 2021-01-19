using Assets.Scripts.Multiplayer;
using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket.Message;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.WebSocket
{
    [RequireComponent(typeof(ClientComunication))]
    class ClientWSBehavour : MonoBehaviour
    {
        public GameClient profile;

        private ClientComunication communication;

        private Queue<string> messageQueue;

        private void Start()
        {
            communication = GetComponent<ClientComunication>();

            DontDestroyOnLoad(gameObject);

            profile = new GameClient();

            messageQueue = new Queue<string>();

            Debug.Log("Client connect to server");

            communication.ConnectToServer();

            if (FindObjectOfType<ServerBehavour>() != null)
            {
                profile.isHost = true;
            }

            // Starting in 20 seconds.
            // a method will be launched every 5 seconds
            //InvokeRepeating("CheckSocketConnection", 20.0f, 15.0f);
        }

        private void OnDestroy()
        {
            communication.DisconectToServer();
        }


        public void ConnectToLobby(string lobbyCode, string type, string playerCode) {

            string LobbyType = (type.Equals("Private")) ? LobbyCodeGenerator.LOBBY_PRIVATE : LobbyCodeGenerator.LOBBY_PUBLIC;

            GuestProfile client = FindObjectOfType<GuestProfile>();

            string playerName= client.profileName;
            string playerId = client.profileId;
            string playerNationality= client.profileNationality;

            ConnectToLobbyReq request = new ConnectToLobbyReq(LobbyType, lobbyCode, playerCode,  playerName, playerId, playerNationality);

            Send(request.GetMessageText());
        }

        public void Send(string message) {

            messageQueue.Enqueue(message);
        }

        private void Update()
        {

            if (communication.GetConnectionStatusOpen() && messageQueue.Count != 0)
            {
                Debug.Log("Server send message ");
                string message = messageQueue.Dequeue();

                communication.SendRequest(message);
            }
        }

        private void CheckSocketConnection() {

            Debug.Log("PressSpace");
            Debug.Log("validate server connection ");

            if (!communication.GetConnectionStatusOpen())
            {
                Debug.Log("Connection is Dead ");

                SceneManager.LoadScene("MainMenu");

            }
            else
            {
                Debug.Log("Connection is Alive ");
            }

        }

    }
}
