﻿namespace Assets.Scripts.WebSocket.Message
{
    public class VictoryGameMessageResp 
    {
        public string origin;
        public string operationCode;
        public string lobbyCode;

        public string gametype;
        public string gameOverType;
        public string playerWin;

        public VictoryGameMessageResp(string msg)
        {
            ConvertMessage(msg);
        }

        private void ConvertMessage(string message)
        {
            string[] eData = message.Split('&');
            if (eData.Length >= 5)
            {
                this.origin = eData[0];
                this.operationCode = eData[1];
                this.lobbyCode = eData[2];
                this.gametype = eData[3];
                this.gameOverType = eData[4];
                this.playerWin = eData[5];
            }
        }
    }
}