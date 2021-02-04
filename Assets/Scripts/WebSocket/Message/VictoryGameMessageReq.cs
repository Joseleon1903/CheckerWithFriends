namespace Assets.Scripts.WebSocket.Message
{
    public class VictoryGameMessageReq
    {

        private string origin;
        private string operationCode;
        private string lobbyCode;

        private string gametype;
        private string playerWin;

        public VictoryGameMessageReq(string lobbyCode, string gametype, string playerWin)
        {
            this.origin = "CLIENT";
            this.operationCode = "100GW";
            this.lobbyCode = lobbyCode;
            this.gametype = gametype;
            this.playerWin = playerWin;
        }

        public string GetMessageText()
        {
            return $"{origin}&{operationCode}&{lobbyCode}&{gametype}&{playerWin}";
        }
    }
}