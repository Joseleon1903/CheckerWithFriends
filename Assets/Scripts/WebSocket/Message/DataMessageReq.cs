namespace Assets.Scripts.WebSocket.Message
{
    class DataMessageReq
    {

        private string origin;
        private string operationCode;
        private string lobbyCode;
        private string gameType;
        private string switchPlayer;
        private string startPosX;
        private string startPosY;
         
        private string endPosX;
        private string endPosY;

        public DataMessageReq(string lobbyCode,string gameType,string switchPlayer,  string startPosX, string startPosY, string endPosX, string endPosY)
        {
            this.origin = "CLIENT";
            this.operationCode = "102LB";
            this.lobbyCode = lobbyCode;
            this.gameType = gameType;
            this.switchPlayer = switchPlayer;
            this.startPosX = startPosX;
            this.startPosY = startPosY;
            this.endPosX = endPosX;
            this.endPosY = endPosY;
        }

        public string GetMessageText()
        {
            return $"{origin}&{operationCode}&{lobbyCode}&{gameType}&{switchPlayer}&{startPosX}&{startPosY}&{endPosX}&{endPosY}";
        }

    }
}
