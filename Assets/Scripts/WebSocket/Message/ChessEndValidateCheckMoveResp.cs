namespace Assets.Scripts.WebSocket.Message
{
    public class ChessEndValidateCheckMoveResp
    {
        public string origin;
        public string operationCode;
        public string lobbyCode;
        public string gameType;
        public string playerCheck;
        public string isValidMove;
        public int startPosX;
        public int startPosY;

        public int endPosX;
        public int endPosY;

        public ChessEndValidateCheckMoveResp(string message)
        {
            ConvertMessage(message);
        }

        private void ConvertMessage(string message)
        {
            string[] eData = message.Split('&');

            if (eData.Length >= 8)
            {
                this.origin = eData[0];
                this.operationCode = eData[1];
                if (string.Equals("102PLCHECKRESP", eData[1])) {
                    this.lobbyCode = eData[2];
                    this.gameType = eData[3];
                    this.isValidMove = eData[4];
                    this.playerCheck = eData[5];
                    this.startPosX = int.Parse(eData[6]);
                    this.startPosY = int.Parse(eData[7]);
                    this.endPosX = int.Parse(eData[8]);
                    this.endPosY = int.Parse(eData[9]);
                }
                
            }
        }
    }
}