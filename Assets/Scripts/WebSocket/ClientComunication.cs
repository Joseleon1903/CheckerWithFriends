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

                if (GameType.CHESS.ToString().ToUpper().Equals(dataResp.gameType.ToUpper()))
                {
                    ChessBoarderManager.Instance.isWhiteTurn = !ChessBoarderManager.Instance.isWhiteTurn;
                    ChessBoarderManager.Instance.TryOnlinePlayerMove(dataResp);
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
                    FindObjectOfType<MultiplayerButtonActionBehavour>().ShowJoinPrivateGamePanel(respR.playerOne, respR.playerTwo);
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

            ChessPromotedPieceResp respP = new ChessPromotedPieceResp(msg);

            if (string.Equals("102PLPROMOTED", respP.operationCode))
            {
                Debug.Log("Recived promoted price client request");

                SpawnPosition spawnPos = new SpawnPosition();
                spawnPos.PosX = respP.piecePosX;
                spawnPos.PosY = respP.piecePosY;
                PlayerType pType = (string.Equals(respP.player, PlayerType.P1.ToString().ToUpper())) ? PlayerType.P1 : PlayerType.P2;
                ChessBoarderManager.Instance.ChessBoardPromotedPiece(pType, respP.pieceType, spawnPos);
            }

            ChessValidateCheckMoveResp vCheck = new ChessValidateCheckMoveResp(msg);

            if (string.Equals("102PLCHECK", vCheck.operationCode))
            {
                Debug.Log("Validation mover for check player");
                ChessBoarderManager.Instance.ValidateUncheckPlayerMove(vCheck);
            }

            ChessEndValidateCheckMoveResp rCheck = new ChessEndValidateCheckMoveResp(msg);

            if (string.Equals("102PLCHECKRESP", rCheck.operationCode)) {

                if (rCheck.isValidMove.Equals(EnumHelper.TRUE)) {
                    Debug.Log("Is valid move");
                    DataMessageResp datamesage = new DataMessageResp();
                    datamesage.gameType = rCheck.gameType;
                    datamesage.checketdMove = EnumHelper.FALSE;
                    datamesage.startPosX = rCheck.startPosX.ToString();
                    datamesage.startPosY = rCheck.startPosY.ToString();
                    datamesage.endPosX = rCheck.endPosX.ToString();
                    datamesage.endPosY = rCheck.endPosY.ToString();
                    ChessBoarderManager.Instance.TryOnlinePlayerMove(datamesage);

                    CanvasManagerUI.Instance.ShowAlertText("It's player P1 turn");

                    BoardHightLight.Intance.HideHightLightKingChecked();
                }

                if (rCheck.isValidMove.Equals(EnumHelper.FALSE))
                {
                    Debug.Log("Is no valid move player is checked");
                    CanvasManagerUI.Instance.ShowAlertText("Player is check");
                }

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
