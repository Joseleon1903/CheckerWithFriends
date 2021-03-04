using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket.Message;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.WebSocket
{
    [RequireComponent(typeof(ServerCommunication))]
    public class ServerBehavour : MonoBehaviour
    {
        private ServerCommunication communication;

        public string ServerLobbyCode;

        private bool isPlayerHost = false;

        public bool IsPlayerHost { get { return isPlayerHost; } }

        private Queue<string> messageQueue;

        private void Start()
        {
            communication = GetComponent<ServerCommunication>();

            DontDestroyOnLoad(gameObject);

            messageQueue = new Queue<string>();

            Debug.Log("Init Host server ");

            communication.ConnectToServer();
        }

        private void OnDestroy()
        {
            communication.DisconectToServer();
        }

        public void SendStartGameMessage()
        {
            string gameType = PlayerPrefs.GetString(PlayerPreferenceKey.PLAYER_LOBBY_GAME_TYPE);
            string map = PlayerPrefs.GetString(PlayerPreferenceKey.PLAYER_LOBBY_MAP);
            string time = PlayerPrefs.GetString(PlayerPreferenceKey.PLAYER_LOBBY_TIME);
            StartGameMessageReq startRequest = new StartGameMessageReq(ServerLobbyCode, map, time, gameType);
            communication.SendRequest(startRequest.GetMessageText());
        }

        public void CreateLobby(string map, string time, string type, string lobbyCode, string gameType, string lobbyCoinReward)
        {
            isPlayerHost = true;
            int coinsValue = int.Parse(lobbyCoinReward);
            ServerLobbyCode = lobbyCode;
            string LobbyType = (type.Equals("Private")) ? LobbyCodeGenerator.LOBBY_PRIVATE : LobbyCodeGenerator.LOBBY_PUBLIC;
            CreateLobbyMessageReq msg = new CreateLobbyMessageReq(LobbyType, map, time, ServerLobbyCode, 2, LobbyCodeGenerator.ONLINE_STATUS, gameType, coinsValue);

            messageQueue.Enqueue(msg.GetMessageText());
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
    }
}