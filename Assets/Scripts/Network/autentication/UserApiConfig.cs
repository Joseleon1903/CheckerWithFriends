using System.Text;

namespace Assets.Scripts.Network.Autentication
{
    public class UserApiConfig
    {

        public static string username = "sea.lion.entertainment.studio";

        public static string password = "tzUqOh.V$";


        public static string UserApiGen()
        {

            string svcCredentials = System.Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));
            return "Basic " + svcCredentials;
        }

    }
}