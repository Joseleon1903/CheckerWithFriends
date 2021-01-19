namespace Assets.Scripts.WebSocket.Message
{
    class CreateLobbyMessageReq
    {
        private string origin;
        private string operationCode;
        private string lobbyType;
        private string lobbyCode;

        private string lobbyMap;
        private string lobbyTime;

        private int lobbyCapacity;
        private string lobbyIdentifier;
        private string lobbyStatus;
        private string lobbyGameType;


        public CreateLobbyMessageReq(string lobbyType,string map, string time, string lobbyCode, int lobbyCapacity, string lobbyStatus, string gameType)
        {
            this.origin = "SERVER";
            this.operationCode = "201LB";
            this.lobbyType = lobbyType;
            this.lobbyCode = lobbyCode;
            this.lobbyCapacity = lobbyCapacity;
            this.lobbyIdentifier = "UN:IDENT:FIER";
            this.lobbyStatus = lobbyStatus;
            this.lobbyMap = map;
            this.lobbyTime = time;
            this.lobbyGameType = gameType;
        }

        public string GetMessageText() {
            return $"{origin}&{operationCode}&{lobbyType}&{lobbyCode}&{lobbyMap}&{lobbyTime}&{lobbyCapacity}&{lobbyGameType}&{lobbyIdentifier}&{lobbyStatus}";
        } 

    }
}
