namespace Assets.Scripts.WebSocket.Message
{
    class CreateLobbyMessageResp
    {
        public string destination;
        public string operation;
        public string result;

        public string lobbyMap;
        public string lobbyTime;
        public string lobbyCode;
        public string lobbyBet;

        public CreateLobbyMessageResp(string message) {
            ConvertMessage(message);
        }

        private void ConvertMessage(string message) {
            string[] eData = message.Split('&');

            if (eData.Length == 7) {
                this.destination = eData[0];
                this.operation = eData[1];
                this.result = eData[2];
                this.lobbyMap = eData[3];
                this.lobbyTime = eData[4];
                this.lobbyCode = eData[5];
                this.lobbyBet = eData[6];
            }
        }
    }

}
