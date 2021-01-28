using Assets.Script.WebSocket;
using Assets.Scripts.Chess;
using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket.Message;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.WebSocket
{
    class ClientComunication : MonoBehaviour
    {

        // Final server address
        private SocketConfig serverConfig;

        private string server;

        // WebSocket Client 
        private WsClient client;

        /// <summary>
        /// Unity method called on initialization
        /// </summary>
        private void Start()
        {
            serverConfig = FindObjectOfType<SocketConfig>();
            if (serverConfig == null) { throw new Exception("Not found Socket Configuration"); }

            server = serverConfig.GetSocketHost();
            client = new WsClient(server);
        }

        /// <summary>
        /// Unity method called every frame
        /// </summary>
        private void Update()
        {
            // Check if server send new messages
            var cqueue = client.receiveQueue;
            string msg;
            while (cqueue.TryPeek(out msg))
            {
                // Parse newly received messages
                cqueue.TryDequeue(out msg);
                HandleMessage(msg);
            }
        }

        public bool GetConnectionStatusOpen()
        {
            return client.IsConnectionOpen();
        }

        /// <summary>
        /// Method responsible for handling server messages
        /// </summary>
        /// <param name="msg">Message.</param>
        private void HandleMessage(string msg)
        {
            Debug.Log("Client: " + msg);

            DataMessageResp dataResp = new DataMessageResp(msg);

            if (string.Equals("102LB", dataResp.operationCode))
            {
                if (GameType.CHECKER.ToString().ToUpper().Equals(dataResp.gameType.ToUpper())) {
                    CheckersBoard.Instance.TryMove(int.Parse(dataResp.startPosX), int.Parse(dataResp.startPosY),
                       int.Parse(dataResp.endPosX), int.Parse(dataResp.endPosY));
                }

                return;
            }


            LobbyReadyMessageResp respR = new LobbyReadyMessageResp(msg);

            if (string.Equals("LOBBYREADY", respR.result)) {

                if (FindObjectOfType<ServerBehavour>() != null) {

                    FindObjectOfType<HostMatchGameBehavour>().ShowClientConnect(respR.playerTwo);

                    FindObjectOfType<ServerBehavour>().SendStartGameMessage();
                }
                else
                {
                    FindObjectOfType<MultiplayerButtonActionBehavour>().HidePublicGamePanel();
                    FindObjectOfType<MultiplayerButtonActionBehavour>().ShowJoinPrivateGamePanel(respR.playerOne);
                }

                return;
            }

            StartGameMessageResp resp = new StartGameMessageResp(msg);

            if (string.Equals("START", resp.result)) {

                Debug.Log("Lobby is full Start the game");

                //Load Lobby
                FindObjectOfType<MultiplayerButtonActionBehavour>().StartGame(resp.map, resp.time, resp.gameType);

                return;
            
            }

            LostConnectionPlayerResp respConLost = new LostConnectionPlayerResp(msg);
            if (string.Equals("CONNECTIONLOST", respConLost.result)) {

                Debug.Log("Lost connection of player");

                SceneManager.LoadScene("MainMenu");
            }

            RematchGameMessageResp rematchDataResp = new RematchGameMessageResp(msg);

            if (string.Equals("300LB", rematchDataResp.operationCode))
            {
                Debug.Log("Recive Rematch client request");
                FindObjectOfType<CheckerEndGameBehavour>().ReciveRematchRequest(rematchDataResp.rematchCounter);
            }


            VictoryGameMessageResp respV = new VictoryGameMessageResp(msg);

            if (string.Equals("102PL", respV.operationCode))
            {
                Debug.Log("Recived Victory client request");
                PlayerType plType = (respV.playerWin.Equals(PlayerType.P1)) ? PlayerType.P1 : PlayerType.P2;
                var NewState = new ChessboardState { isGameOver = true, gameOverType = GameOverType.CHECKMATE, playerWin = plType };
                ChessBoarderManager.Instance.ChessboardState = NewState;
                ChessBoarderManager.Instance.ValidatePlayerVictory();
            }       
           

        }

        /// <summary>
        /// Call this method to connect to the server
        /// </summary>
        public async void ConnectToServer()
        {
            await client.Connect();
        }

        /// <summary>
        /// Method which sends data through websocket
        /// </summary>
        /// <param name="message">Message.</param>
        public void SendRequest(string message)
        {
            client.Send(message);
        }

        public async void DisconectToServer()
        {
            await client.Disconect();
        }
    }
}
