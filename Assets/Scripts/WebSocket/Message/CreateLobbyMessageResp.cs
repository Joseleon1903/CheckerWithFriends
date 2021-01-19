namespace Assets.Scripts.WebSocket.Message
{
    class CreateLobbyMessageResp
    {
        public string destination;
        public string operation;
        public string result;

        public CreateLobbyMessageResp(string message) {
            ConvertMessage(message);
        }

        private void ConvertMessage(string message) {
            string[] eData = message.Split('|');

            if (eData.Length == 3) {
                this.destination = eData[0];
                this.operation = eData[1];
                this.result = eData[2];
            }

        }
    }

}
