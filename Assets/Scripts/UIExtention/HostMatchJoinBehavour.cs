using Assets.Scripts.Profile;
using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket.Message;
using UnityEngine;
using UnityEngine.UI;

class HostMatchJoinBehavour: MonoBehaviour
{

    [SerializeField] private GameObject PlayerTwoFrame;

    [SerializeField] private GameObject PlayerTwoAvatar;

    [SerializeField] private GameObject PlayerOneFrame;

    [SerializeField] private GameObject PlayerOneAvatar;

    [SerializeField] private Text PlayerOneNameField;

    [SerializeField] private Text PlayerOneIdField;

    [SerializeField] private Text PlayerOneNatioNalityField;

    [SerializeField] private Text PlayerTwoNameField;

    [SerializeField] private Text PlayerTwoIdField;

    [SerializeField] private Text PlayerTwoNationalityField;

    public void SetUpView(PlayerInfo playerOne) {

        //player One 
        PlayerOneNameField.text = playerOne.name;
        PlayerOneIdField.text = playerOne.playerId;
        PlayerOneNatioNalityField.text = playerOne.nationality;
        string[] sprite = playerOne.picture.Split('%');
        string frameRoot = sprite[0];
        string avatarRoot = sprite[1];

        ProfileUtil.SetupProfileImageFromResources(avatarRoot, frameRoot, PlayerOneAvatar, PlayerOneFrame);

        //player Two
        GameObject profile = Finder.FindGameProfile();
        BaseProfile profileClient = profile.GetComponent<BaseProfile>();
        PlayerTwoNameField.text = profileClient._nameProfile;
        PlayerTwoIdField.text = (profileClient._isGuest) ? profileClient._guestUserId : profileClient._facebookUserId;
        PlayerTwoNationalityField.text = profileClient._nationality;

        ProfileUtil.SetUpProfileImage(profile, PlayerTwoAvatar, PlayerTwoFrame);

        //set preference
        PlayerPrefs.SetString(PlayerPreferenceKey.PROFILE_ONE_KEY_AVATAR, avatarRoot);
        PlayerPrefs.SetString(PlayerPreferenceKey.PROFILE_ONE_KEY_FRAME, frameRoot);

        string[] sprites = profileClient._profilePicture.Split('%');
        PlayerPrefs.SetString(PlayerPreferenceKey.PROFILE_TWO_KEY_AVATAR, sprites[1]);
        PlayerPrefs.SetString(PlayerPreferenceKey.PROFILE_TWO_KEY_FRAME, sprites[0]);
    }

}
