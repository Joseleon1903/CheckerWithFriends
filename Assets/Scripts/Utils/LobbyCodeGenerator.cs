namespace Assets.Scripts.Utils
{
    class LobbyCodeGenerator
    {

        public static string LOBBY_PUBLIC = "Public";
        public static string LOBBY_PRIVATE = "Private";

        public static string ONLINE_STATUS = "Online";
        public static string OFFLINE_STATUS = "Offline";


        public static int CODE_LENGHT = 4;

        public static string[] Alphabet = { "A", "B", "C", "D","E","F","G","H","I","L","M","N","O",
                                            "P", "Q","R","S","T","U","V","Z","Y","X"};

        public static string GenerateLobbyCode(int lenght) {

            string code = "";

            for (int ind = 0; ind <= lenght; ind++) {
                int Rnumber = UnityEngine.Random.Range(0, Alphabet.Length -1);
                code += Alphabet[Rnumber];
            }
            return code;
        }

    }
}
