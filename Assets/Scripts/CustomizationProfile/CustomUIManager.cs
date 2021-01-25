using Assets.Scripts.Network.Model;
using Assets.Scripts.Network.Service;
using Assets.Scripts.Profile;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomUIManager : MonoBehaviour
{
    public enum FrameKey
    {
        FRAME_GREEN = 2,
        FRAME_YELLOW = 3,
        FRAME_BLUE = 1
    }

    public enum AvatarKey
    {
        LIGHT_BLUE_FEMALE_HERO = 1,
        YELLOW_MALE_HERO = 2,
        GRAY_MALE_HERO = 3,
        LIGHT_BLUE_MALE_HERO= 4,
        VIOLET_MALE_HERO = 5,
        VIOLET_FEMALE_HERO = 6,
        PURPLE_FEMALE_HERO = 7,
        RED_FEMALE_HERO = 8,
        YELLOW_FEMALE_HERO = 9
    }

    public enum UserSelection
    {
        AVATAR = 1,
        FRAME = 2,
    }

    
    [SerializeField]
    [Tooltip("All hero sprite dictionary key value")]
    private KeySpriteDictionary spriteDictionary;

    [SerializeField]
    [Tooltip("All frame sprite dictionary key value")]
    private KeySpriteDictionary frameDictionary;

    [SerializeField]
    private GameObject avatarSprite;

    [SerializeField]
    private GameObject frameSprite;

    [SerializeField]
    private GameObject prefabGuestProfile;

    public void PressFrameButton(GameObject objectClick) {

        Debug.Log("Entering in method PressFrameButton");
        Debug.Log("Key " + objectClick.gameObject.name);

        FrameKey frame = objectClick.GetComponent<FrameItem>().ItemKey;
        Sprite sprite;
        switch (frame)
        {
            case FrameKey.FRAME_BLUE:

                sprite = frameDictionary["Frame_Blue"];

                ModifyUserSelectionAvatar(sprite, UserSelection.FRAME);

                break;
            case FrameKey.FRAME_GREEN:
                sprite = frameDictionary["Frame_Green"];

                ModifyUserSelectionAvatar(sprite, UserSelection.FRAME);

                break;
            case FrameKey.FRAME_YELLOW:

                sprite = frameDictionary["Frame_Yellow"];

                ModifyUserSelectionAvatar(sprite, UserSelection.FRAME);

                break;
        }

    }

    public void PressHeroButton(GameObject objectClick)
    {
        Debug.Log("Entering in method PressHeroButton");
        Debug.Log("Key "+ objectClick.gameObject.name);

        AvatarKey avatar = objectClick.GetComponent<HeroItem>().ItemKey;
        Sprite sprite;
        switch (avatar)
        {
            case AvatarKey.GRAY_MALE_HERO:

                sprite = spriteDictionary["Gray_Male_Hero"];

                ModifyUserSelectionAvatar(sprite, UserSelection.AVATAR);

                break;
            case AvatarKey.LIGHT_BLUE_FEMALE_HERO:

                sprite = spriteDictionary["Blue_Female_Hero"];

                ModifyUserSelectionAvatar(sprite, UserSelection.AVATAR);

                break;
            case AvatarKey.LIGHT_BLUE_MALE_HERO:

                sprite = spriteDictionary["Light_Blue_Male_Hero"];

                ModifyUserSelectionAvatar(sprite, UserSelection.AVATAR);

                break;

           case AvatarKey.PURPLE_FEMALE_HERO:

                sprite = spriteDictionary["Purple_Female_Hero"];

                ModifyUserSelectionAvatar(sprite, UserSelection.AVATAR);

                break;

            case AvatarKey.RED_FEMALE_HERO:

                sprite = spriteDictionary["Red_Female_Hero"];

                ModifyUserSelectionAvatar(sprite, UserSelection.AVATAR);

                break;

            case AvatarKey.VIOLET_FEMALE_HERO:

                sprite = spriteDictionary["Violet_Female_Hero"];

                ModifyUserSelectionAvatar(sprite, UserSelection.AVATAR);

                break;

            case AvatarKey.VIOLET_MALE_HERO:

                sprite = spriteDictionary["Violet_Male_Hero"];

                ModifyUserSelectionAvatar(sprite, UserSelection.AVATAR);

                break;

            case AvatarKey.YELLOW_FEMALE_HERO:

                sprite = spriteDictionary["Yellow_Female_Hero"];

                ModifyUserSelectionAvatar(sprite, UserSelection.AVATAR);

                break;

            case AvatarKey.YELLOW_MALE_HERO:

                sprite = spriteDictionary["Yellow_Male_Hero"];

                ModifyUserSelectionAvatar(sprite, UserSelection.AVATAR);

                break;

        }

    }

    private void ModifyUserSelectionAvatar(Sprite sprite, UserSelection selection ) {

        if (selection.Equals(UserSelection.FRAME)) {

            Image image = frameSprite.GetComponent<Image>();

            image.sprite = sprite;
        }

        if (selection.Equals(UserSelection.AVATAR))
        {
            Image image = avatarSprite.GetComponent<Image>();

            image.sprite = sprite;
        }

    }

    public void PressContinueButton() {

        Debug.Log("Entering in method PressContinueButton");
        ProfileModel profileM = new ProfileModel();
        if (EnumHelper.TRUE.Equals(PlayerPrefs.GetString(PlayerPreferenceKey.GUEST_PROFILE_KEY)))
        {
            Debug.Log("Create guest profile");

            string name = ProfileUtil.GenerateGuestName(5);
            string guestid = "GUEST-"+ ProfileUtil.GenerateRandomCode(9);

            string profilePic = ProfilePictureString(avatarSprite, frameSprite);

            profileM = new ProfileModel(0, name, "","", guestid, profilePic, GameType.CHECKER.ToString().ToUpper(), "Dominican Republic",EnumHelper.TRUE);

        }
        else if(EnumHelper.FALSE.Equals(PlayerPrefs.GetString(PlayerPreferenceKey.GUEST_PROFILE_KEY)))
        {
            Debug.Log("Create facebook profile");

        }

        //send Post request
        string api = RestClientBehavour.Instance.ApiBaseUrl + ProfileService.CreateProfilePath;

        ProfileService serviceVersion = new ProfileService();

        serviceVersion.PostProfile(api, profileM).Then(response => {

            Debug.Log("Registration fetch to local profile");

            GameObject profile = Finder.FindGameProfile();

            if (profile == null)
            {
                profile = Instantiate(prefabGuestProfile);
            }

            profile = BaseProfile.FetchFromModel(profile, profileM );

            profile.GetComponent<GuestProfile>().ProfileAvatarSprite = avatarSprite.GetComponent<Image>().sprite;
            profile.GetComponent<GuestProfile>().ProfileFrameSprite = frameSprite.GetComponent<Image>().sprite;

        });

        //fordware next scene
        SceneManager.LoadScene("MainMenu");

    }

    private string ProfilePictureString(GameObject avatar , GameObject frame) {
        string avatarSprite = avatar.GetComponent<Image>().sprite.name;
        string frameSprite = frame.GetComponent<Image>().sprite.name;
        return frameSprite + "%"+ avatarSprite;
    }

}
