using Assets.Script.WebSocket;
using Assets.Scripts.Profile;
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

            BaseProfile profile = FindObjectOfType<BaseProfile>();

            string playerName= profile._nameProfile;
            string playerId = (profile._isGuest) ? profile._guestUserId : profile._facebookUserId;
            string playerNationality= profile._nationality;

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
