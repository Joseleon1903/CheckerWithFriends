namespace Assets.Scripts.WebSocket.Message
{
    public class ChessEndValidateCheckMoveReq
    {
        private string origin;
        private string operationCode;
        private string lobbyCode;
        private string gameType;
        private string playerCheck;
        private string isValidMove;
        private int startPosX;
        private int startPosY;

        private int endPosX;
        private int endPosY;

        public ChessEndValidateCheckMoveReq(string lobbyCode,string gameType, string isValidMove, string playerCheck, int startPosX, int startPosY, int endPosX, int endPosY) 
        {
            this.origin = "CLIENT";
            this.operationCode = "102PLCHECKRESP";
            this.lobbyCode = lobbyCode;
            this.gameType = gameType;
            this.playerCheck = playerCheck;
            this.isValidMove = isValidMove;
            this.startPosX = startPosX;
            this.startPosY = startPosY;
            this.endPosX = endPosX;
            this.endPosY = endPosY;
        }

        public string GetMessageText()
        {
            return $"{origin}&{operationCode}&{lobbyCode}&{gameType}&{isValidMove}&{playerCheck}&{startPosX}&{startPosY}&{endPosX}&{endPosY}";
        }


    }
}