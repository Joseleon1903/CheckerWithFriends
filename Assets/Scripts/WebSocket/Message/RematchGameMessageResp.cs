using System;

namespace Assets.Scripts.WebSocket.Message
{
    class RematchGameMessageResp
    {
        public string origin;
        public string operationCode;
        public string lobbyCode;

        public int rematchCounter;

        public RematchGameMessageResp(string msg) {

            ConvertMessage(msg);
        }

        private void ConvertMessage(string message)
        {
            string[] eData = message.Split('|');
            if (eData.Length >= 4)
            {
                this.origin = eData[0];
                this.operationCode = eData[1];
                this.lobbyCode = eData[2];
                this.rematchCounter = int.Parse(eData[3]);
            }
        }
    }
}
