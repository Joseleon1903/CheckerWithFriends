namespace Assets.Scripts.WebSocket.Message
{
    public class DataMessageResp
    {
        public string origin ="";
        public string operationCode= "";
        public string lobbyCode= "";
        public string gameType;
        public string switchPlayer;
        public string startPosX;
        public string startPosY;

        public string endPosX;
        public string endPosY;

        public DataMessageResp(string message)
        {
            ConvertMessage(message);
        }

        public DataMessageResp(){}

        private void ConvertMessage(string message)
        {
            string[] eData = message.Split('&');
            if (eData.Length >= 8)
            {
                this.origin = eData[0];
                this.operationCode = eData[1];
                this.lobbyCode = eData[2];
                this.gameType = eData[3];
                this.switchPlayer = eData[4];
                this.startPosX = eData[5];
                this.startPosY = eData[6];
                this.endPosX = eData[7];
                this.endPosY = eData[8];
            }
        }

    }
}
