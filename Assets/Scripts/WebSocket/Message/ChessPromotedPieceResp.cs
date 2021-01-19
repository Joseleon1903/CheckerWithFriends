namespace Assets.Scripts.WebSocket.Message
{
    public class ChessPromotedPieceResp 
    {
        public string origin;
        public string operationCode;
        public string lobbyCode;
        public string gameType;
        public string player;
        public int pieceType;

        public int piecePosX;
        public int piecePosY;

        public ChessPromotedPieceResp(string message)
        {
            ConvertMessage(message);
        }

        private void ConvertMessage(string message)
        {
            string[] eData = message.Split('&');

            if (eData.Length >=5)
            {
                this.origin = eData[0];
                this.operationCode = eData[1];
                if (string.Equals(eData[1],"102PLPROMOTED")) {
                    this.lobbyCode = eData[2];
                    this.gameType = eData[3];
                    this.player = eData[4];
                    this.pieceType = int.Parse(eData[5]);
                    this.piecePosX = int.Parse(eData[6]);
                    this.piecePosY = int.Parse(eData[7]);
                }
            }

        }
    }
}