namespace Assets.Scripts.WebSocket.Message
{
    public class ChessPromotedPieceReq 
    {

        private string origin;
        private string operationCode;
        private string lobbyCode;
        private string gameType;
        private string player;
        private int pieceType;

        private string piecePosX;
        private string piecePosY;

        public ChessPromotedPieceReq(string lobbyCode, string gameType,string player, int pieceType,string piecePosX, string piecePosY)
        {
            this.origin = "CLIENT";
            this.operationCode = "102PLPROMOTED";
            this.lobbyCode = lobbyCode;
            this.gameType = gameType;
            this.player = player;
            this.pieceType = pieceType;
            this.piecePosX = piecePosX;
            this.piecePosY = piecePosY;
        }

        public string GetMessageText()
        {
            return $"{origin}&{operationCode}&{lobbyCode}&{gameType}&{player}&{pieceType}&{piecePosX}&{piecePosY}";
        }

    }
}