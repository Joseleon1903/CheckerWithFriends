namespace Assets.Scripts.WebSocket.Message
{
    class ConnectToLobbyReq
    {
        private string origin;
        private string operationCode;
        private string lobbyType;
        private string lobbyCode;
        private string playerName;
        private string playerId;
        private string PlayerNationaity;
        private string playerCode;
        private string playerSession;
        private int isHost;

        public ConnectToLobbyReq(string lobbyType, string lobbyCode, string code ,string playerName, string id, string nationality)
        {
            this.origin = "CLIENT";
            this.operationCode = "202LB";
            this.lobbyType = lobbyType;
            this.lobbyCode = lobbyCode;
            this.playerName = playerName;
            this.playerSession = "UN:IDENT:FIER";
            this.playerId = id;
            this.PlayerNationaity = nationality;
            this.isHost = 0;
            this.playerCode = code;
        }

        public string GetMessageText()
        {
            return $"{origin}&{operationCode}&{lobbyType}&{lobbyCode}&{playerCode}&{playerName}&{playerId}&{PlayerNationaity}&{playerSession}&{isHost}";
        }

    }
}
