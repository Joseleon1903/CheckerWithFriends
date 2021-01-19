namespace Assets.Scripts.WebSocket.Message
{
    class StartGameMessageResp
    {
        public string destination ="";
        public string operation = "";
        public string result ="";
        public string map;
        public string time;
        public string gameType;
        public string gameLobby;

        public StartGameMessageResp(string message)
        {
            ConvertMessage(message);
        }

        private void ConvertMessage(string message)
        {
            string[] eData = message.Split('|');
            if (eData.Length > 5)
            {
                this.destination = eData[0];
                this.operation = eData[1];
                this.result = eData[2];
                this.gameLobby = eData[3];
                this.map = eData[4];
                this.time = eData[5];
                this.gameType = eData[6];
            }
        }

    }
}
