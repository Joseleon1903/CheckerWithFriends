using System;

namespace Assets.Scripts.Network.Model
{
    [Serializable]
    public class ProfileModel 
    {
        public int id;
        public string name;
        public string email;
        public string facebookId;
        public string guestUserId;
        public string lastTokenInfoString;
        public string profilePicture;
        public string creationDate;
        public string playerCoins;
        public string gameName;
        public int totalCheckerGame;
        public int totalCheckerGameWin;
        public string language;
        public string nationality;
        public string isGuest;

        public ProfileModel() { }

        public ProfileModel(int id, string name, string email, string facebookId, string guestUserId, string lastTokenInfoString, string profilePicture, string playerCoins, string nationality, string isGuest)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.facebookId = facebookId;
            this.guestUserId = guestUserId;
            this.lastTokenInfoString = lastTokenInfoString;
            this.profilePicture = profilePicture;
            this.creationDate = DateTime.Now.ToString("MM/dd/yyyy H:mm");
            this.playerCoins = playerCoins;
            this.gameName = "CheckerWithFriends";
            this.totalCheckerGame = 0;
            this.totalCheckerGameWin = 0;
            this.language = "English";
            this.nationality = nationality;
            this.isGuest = isGuest;
        }

        public override string ToString()
        {
            return UnityEngine.JsonUtility.ToJson(this, true);
        }
    }
}