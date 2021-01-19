namespace Assets.Scripts.WebSocket.Message
{
    class DataMessageReq
    {

        private string origin;
        private string operationCode;
        private string lobbyCode;
        private string gameType;
        private string checketdMove;
        private string startPosX;
        private string startPosY;
         
        private string endPosX;
        private string endPosY;

        public DataMessageReq(string lobbyCode,string gameType,string checketdMove,  string startPosX, string startPosY, string endPosX, string endPosY)
        {
            this.origin = "CLIENT";
            this.operationCode = "102LB";
            this.lobbyCode = lobbyCode;
            this.gameType = gameType;
            this.checketdMove = checketdMove;
            this.startPosX = startPosX;
            this.startPosY = startPosY;
            this.endPosX = endPosX;
            this.endPosY = endPosY;
        }

        public string GetMessageText()
        {
            return $"{origin}&{operationCode}&{lobbyCode}&{gameType}&{checketdMove}&{startPosX}&{startPosY}&{endPosX}&{endPosY}";
        }

    }
}
