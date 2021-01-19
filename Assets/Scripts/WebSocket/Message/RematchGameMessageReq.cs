namespace Assets.Scripts.WebSocket.Message
{
    class RematchGameMessageReq
    {
        private string origin;
        private string operationCode;
        private string lobbyCode;

        private int rematchCounter;

        public RematchGameMessageReq(string lobbyCode, int rematchCounter)
        {
            this.origin = "CLIENT";
            this.operationCode = "300LB";
            this.lobbyCode = lobbyCode;
            this.rematchCounter = rematchCounter;
        }

        public string GetMessageText()
        {
            return $"{origin}&{operationCode}&{lobbyCode}&{rematchCounter}";
        }

    }
}
