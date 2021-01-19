namespace Assets.Scripts.WebSocket.Message
{
    public class ChessValidateCheckMoveReq
    {

        private string origin;
        private string operationCode;
        private string lobbyCode;
        private string gameType;
        private string playerCheck;
        private string startPosX;
        private string startPosY;

        private string endPosX;
        private string endPosY;

        public ChessValidateCheckMoveReq(string lobbyCode, string gameType, string playerCheck, string startPosX, string startPosY, string endPosX, string endPosY)
        {
            this.origin = "CLIENT";
            this.operationCode = "102PLCHECK";
            this.lobbyCode = lobbyCode;
            this.gameType = gameType;
            this.playerCheck = playerCheck;
            this.startPosX = startPosX;
            this.startPosY = startPosY;
            this.endPosX = endPosX;
            this.endPosY = endPosY;
        }

        public string GetMessageText()
        {
            return $"{origin}&{operationCode}&{lobbyCode}&{gameType}&{playerCheck}&{startPosX}&{startPosY}&{endPosX}&{endPosY}";
        }
    }
}