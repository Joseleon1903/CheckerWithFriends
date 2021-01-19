namespace Assets.Scripts.WebSocket.Message
{
    class StartGameMessageReq
    {
        public string destination = "";
        public string operation = "";
        public string result = "";

        public string lobbyCode = "";
        public string map = "";
        public string time= "";
        public string gameType = "";

        public StartGameMessageReq(string lobbyCode , string map, string time, string gameType)
        {
            this.destination = "SERVER";
            this.operation = "202LB";
            this.result = "START";
            this.lobbyCode = lobbyCode;
            this.map = map;
            this.time = time;
            this.gameType = gameType;
        }

        public string GetMessageText()
        {
            return $"{destination}&{operation}&{result}&{lobbyCode}&{map}&{time}&{gameType}";
        }
    }
}
