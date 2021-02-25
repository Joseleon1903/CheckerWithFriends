using Assets.Scripts.Json;
using Assets.Scripts.Network.Model;
using Assets.Scripts.Network.Service;
using Assets.Scripts.Profile;
using Assets.Scripts.Utils;
using Proyecto26;
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
        LIGHT_BLUE_FEMALE = 1,
        YELLOW_MALE = 2,
        GRAY_MALE = 3,
        LIGHT_BLUE_MALE= 4,
        PURPLE_FEMALE = 5,
        RED_FEMALE = 6,
        VIOLET_FEMALE = 7,
        VIOLET_MALE = 8,
        YELLOW_FEMALE = 9
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
    private Material selectionMaterial;

    [SerializeField]
    private Material defaultMaterial;

    [SerializeField]
    private GameObject prefabGuestProfile;

    private AvatarJson[] avatars;
    private FrameJson[] frames;

    public void PressFrameButton(GameObject objectClick) {
        int frame = (int)objectClick.GetComponent<FrameItem>().ItemKey;
        string frameKey = EnumHelper.FindKeybyIdInDictionary(frame, frames);
        Sprite sprite = frameDictionary[frameKey];
        ModifyUserSelectionAvatar(sprite, UserSelection.FRAME);

        ModifySelectionButton(UserSelection.FRAME, frame);
    }

    public void PressHeroButton(GameObject objectClick)
    {
        int hero = (int)objectClick.GetComponent<HeroItem>().ItemKey;
        string heroKey = EnumHelper.FindKeybyIdInDictionary(hero, avatars);
        Sprite sprite = spriteDictionary[heroKey];
        ModifyUserSelectionAvatar(sprite, UserSelection.AVATAR);

        ModifySelectionButton(UserSelection.AVATAR, hero);
    }

    public void ModifySelectionButton(UserSelection selection,int itemkey) {

        if (selection.Equals(UserSelection.AVATAR)) {

            GameObject[] sceneButton = GameObject.FindGameObjectsWithTag("AvatarButton");

            foreach (GameObject item in sceneButton)
            {

                if ((int)item.GetComponent<HeroItem>().ItemKey == itemkey)
                {

                    item.GetComponent<Image>().color = selectionMaterial.color;
                }
                else 
                {
                    item.GetComponent<Image>().color = defaultMaterial.color;

                }
            }
        }

        if (selection.Equals(UserSelection.FRAME))
        {
            GameObject[] sceneButton = GameObject.FindGameObjectsWithTag("FrameButton");

            foreach (GameObject item in sceneButton)
            {

                if ((int)item.GetComponent<FrameItem>().ItemKey == itemkey)
                {

                    item.GetComponent<Image>().color = selectionMaterial.color;
                }
                else
                {
                    item.GetComponent<Image>().color = defaultMaterial.color;

                }
            }


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

    private void Awake()
    {
        string avatarsJson = ResourcesUtil.FindStringJsonFileInResource(ResourcesUtil.JSON_AVATAR);

        avatars = JsonHelper.ArrayFromJson<AvatarJson>(avatarsJson);

        foreach (AvatarJson av in avatars) {

            Sprite avatarSprite = ResourcesUtil.FindProfileSpriteInResource(av.avatarFileName);
            spriteDictionary.Add(av.avatarKey, avatarSprite);
        }

        string frameJson = ResourcesUtil.FindStringJsonFileInResource(ResourcesUtil.JSON_FRAME);

        frames = JsonHelper.ArrayFromJson<FrameJson>(frameJson);

        foreach (FrameJson fr in frames)
        {
            Sprite avatarSprite = ResourcesUtil.FindProfileSpriteInResource(fr.frameFileName);
            frameDictionary.Add(fr.frameKey, avatarSprite);
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
            string coins = ProfileUtil.GetCoinsGuestProfileDefaultValue();
            profileM = new ProfileModel(0, name, "","", guestid,"", profilePic, coins, "DOM",EnumHelper.TRUE);
            PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_GAME_LANGUAGE, "EN");
        }
        else if(EnumHelper.FALSE.Equals(PlayerPrefs.GetString(PlayerPreferenceKey.GUEST_PROFILE_KEY)))
        {
            Debug.Log("Create facebook profile");

        }

        //send Post request
        string api = RestClientBehavour.Instance.ApiBaseUrl + ProfileService.CreateProfilePath;

        ProfileService serviceVersion = new ProfileService();

        serviceVersion.PostProfile(api, profileM).Then(response => {

            GameObject profile = Finder.FindGameProfile();

            if (profile == null)
            {
                profile = Instantiate(prefabGuestProfile);
            }

            profileM.id = response.id;
            profile = BaseProfile.FetchFromModel(profile, profileM);

            profile.GetComponent<GuestProfile>().ProfileAvatarSprite = avatarSprite.GetComponent<Image>().sprite;
            profile.GetComponent<GuestProfile>().ProfileFrameSprite = frameSprite.GetComponent<Image>().sprite;

            string[] sprites = profileM.profilePicture.Split('%');

            PlayerPrefs.SetString(PlayerPreferenceKey.PROFILE_ONE_KEY_AVATAR, sprites[1]);
            PlayerPrefs.SetString(PlayerPreferenceKey.PROFILE_ONE_KEY_FRAME, sprites[0]);

        }).Then( ()=> {
            //fordware next scene
            SceneManager.LoadSceneAsync("MainMenu");
        });

    }

    private string ProfilePictureString(GameObject avatar , GameObject frame) {
        string avatarSprite = avatar.GetComponent<Image>().sprite.name;
        string frameSprite = frame.GetComponent<Image>().sprite.name;
        return frameSprite + "%"+ avatarSprite;
    }

}
