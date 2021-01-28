namespace Assets.Scripts.WebSocket.Message
{
    class LobbyReadyMessageResp
    {

        public string destination = "";
        public string operation = "";
        public string result = "";


        public PlayerInfo playerOne;
        public PlayerInfo playerTwo;


        public LobbyReadyMessageResp(string message)
        {
            ConvertMessage(message);
        }

        private void ConvertMessage(string message)
        {
            string[] eData = message.Split('|');
            if (eData.Length >= 10)
            {
                this.destination = eData[0];
                this.operation = eData[1];
                this.result = eData[2];

                if (string.Equals("LOBBYREADY", eData[2]))
                {
                    PlayerInfo playerOneRef = new PlayerInfo
                    {
                        code = eData[3],
                        playerId = eData[5],
                        name = eData[4],
                        nationality = eData[6],
                        picture = eData[7]
                    };

                    PlayerInfo playerTwoRef = new PlayerInfo
                    {
                        code = eData[8],
                        playerId = eData[10],
                        name = eData[9],
                        nationality = eData[11],
                        picture = eData[12]
                    };
                    playerOne = playerOneRef;
                    playerTwo = playerTwoRef;
                }
            }
        }
        
    }

    public class PlayerInfo
    {
        public string code;
        public string name;
        public string playerId;
        public string nationality;
        public string picture;
    }
}
