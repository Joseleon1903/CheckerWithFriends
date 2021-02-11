using Assets.Scripts.Network.Model;
using Assets.Scripts.Profile;
using System;
using UnityEngine;
using UnityEngine.UI;

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

        public static void SetUpProfileImage(GameObject profile, GameObject profileAvatar, GameObject profileFrame)
        {
            Debug.Log("Entering in SetUpProfileImage");

            BaseProfile profileBase = profile.GetComponent<BaseProfile>();
            if (profileBase._isGuest)
            {
                profileAvatar.GetComponent<Image>().sprite = profile.GetComponent<GuestProfile>().ProfileAvatarSprite;
                profileFrame.GetComponent<Image>().sprite = profile.GetComponent<GuestProfile>().ProfileFrameSprite;
            }
            else
            {
                profileAvatar.GetComponent<Image>().sprite = profile.GetComponent<FacebookProfile>().ProfileAvatarSprite;
                profileFrame.GetComponent<Image>().sprite = profile.GetComponent<FacebookProfile>().ProfileFrameSprite;
            }

        }

        public static void SetupProfileImageFromResources(string avatarkey, string framekey , GameObject profileAvatar, GameObject profileFrame) {

            Sprite avatar = Resources.Load<Sprite>("Sprites/Profile/"+ avatarkey);
            Sprite frame = Resources.Load<Sprite>("Sprites/Profile/" + framekey);

            profileAvatar.GetComponent<Image>().sprite = avatar;
            profileFrame.GetComponent<Image>().sprite = frame;
        }

        public static string GetCoinsGuestProfileDefaultValue()
        {
            string value = "600";
            return value;
          
        }

        public static ProfileModel GetProfileModel()
        {
            ProfileModel model = new ProfileModel();
            BaseProfile profileBase = Finder.FindGameProfile().GetComponent<BaseProfile>();

            model.id = profileBase.id;
            model.name = profileBase._nameProfile;
            model.email = profileBase._email;
            model.facebookId = profileBase._facebookUserId;
            model.guestUserId = profileBase._guestUserId;
            model.lastTokenInfoString = profileBase._lastTokenInfoString;
            model.profilePicture = profileBase._profilePicture;
            model.creationDate = profileBase._cretionDate;
            model.playerCoins = profileBase._playerCoins;
            model.gameName = GameType.CHECKER.ToString().ToUpper();
            model.totalCheckerGame = profileBase._totalCheckerGame;
            model.totalCheckerGameWin = profileBase._totalCheckerGameWin;
            model.language = PlayerPrefs.GetString(PlayerPreferenceKey.PLAYER_GAME_LANGUAGE);
            model.nationality = profileBase._nationality;
            model.isGuest = profileBase._isGuest ? EnumHelper.TRUE : EnumHelper.FALSE;

            return model;
        }
    }
}
