namespace Assets.Scripts.Utils
{
    class ProfileUtil
    {
        private static string GuestName = "GuestPlayer";

        public static string[] Number = { "0", "1", "2", "3","4","5","6","7","8","9"};

        public static string GenerateGuestName(int lenght) {

            string name = GuestName;

            for (int ind = 0; ind <= lenght; ind++)
            {
                int Rnumber = UnityEngine.Random.Range(0, Number.Length - 1);
                name += Number[Rnumber];
            }
            return name;
        }

        public static string GenerateRandomCode(int lenght)
        {
            string code= "";

            for (int ind = 0; ind <= lenght; ind++)
            {
                int Rnumber = UnityEngine.Random.Range(0, Number.Length - 1);
                code += Number[Rnumber];
            }
            return code;
        }


    }
}
