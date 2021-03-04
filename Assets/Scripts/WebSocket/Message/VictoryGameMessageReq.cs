namespace Assets.Scripts.WebSocket.Message
{
    public class VictoryGameMessageReq
    {

        private string origin;
        private string operationCode;
        private string lobbyCode;

        private string gametype;

        public string gameOverType;

        private string playerWin;

        private string playerOneId;

        private string playerTwoId;

        public VictoryGameMessageReq(string lobbyCode, string gametype,string gameOverType, string playerWin, string playerOneId, string playerTwoId)
        {
            this.origin = "CLIENT";
            this.operationCode = "100GW";
            this.lobbyCode = lobbyCode;
            this.gametype = gametype;
            this.gameOverType = gameOverType;
            this.playerWin = playerWin;
            this.playerOneId = playerOneId;
            this.playerTwoId = playerTwoId;
        }

        public string GetMessageText()
        {
            return $"{origin}&{operationCode}&{lobbyCode}&{gametype}&{gameOverType}&{playerWin}&{playerOneId}&{playerTwoId}";
        }
    }
}