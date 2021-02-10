using System.Collections.Generic;

namespace Assets.Scripts.Utils
{
    public class LangUtils
    {
        public static string TittleGame = "CheckerWithFriends";

        public static string LANGUAGE_ITALIAN_KEY = "IT";
        public static string LANGUAGE_SPANISH_KEY = "ES";
        public static string LANGUAGE_ENGLISH_KEY = "EN";

        public static Dictionary<string, string> LanguageKeyDictionary = new Dictionary<string, string>() {
            { LANGUAGE_ITALIAN_KEY , "Language_IT"},
            { LANGUAGE_SPANISH_KEY , "Language_ES"},
            { LANGUAGE_ENGLISH_KEY , "Language_EN"},
        };

    }
}