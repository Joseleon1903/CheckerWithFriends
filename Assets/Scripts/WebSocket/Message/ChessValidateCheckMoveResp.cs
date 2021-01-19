namespace Assets.Scripts.WebSocket.Message
{
    public class ChessValidateCheckMoveResp 
    {
        public string origin;
        public string operationCode;
        public string lobbyCode;
        public string gameType;
        public string playerCheck;
        public int startPosX;
        public int startPosY;

        public int endPosX;
        public int endPosY;

        public ChessValidateCheckMoveResp(string message)
        {
            ConvertMessage(message);
        }

        private void ConvertMessage(string message)
        {
            string[] eData = message.Split('&');

            if (eData.Length >=8)
            {
                this.origin = eData[0];
                this.operationCode = eData[1];

                if (string.Equals(eData[1], "102PLCHECK")) {
                    this.lobbyCode = eData[2];
                    this.gameType = eData[3];
                    this.playerCheck = eData[4];
                    this.startPosX = int.Parse(eData[5]);
                    this.startPosY = int.Parse(eData[6]);
                    this.endPosX = int.Parse(eData[7]);
                    this.endPosY = int.Parse(eData[8]);

                }

            }

        }

    }
}