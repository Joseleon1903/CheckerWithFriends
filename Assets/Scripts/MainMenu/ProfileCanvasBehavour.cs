using Assets.Scripts.MainMenu;
using Assets.Scripts.Profile;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

public class ProfileCanvasBehavour : MonoBehaviour
{

    [SerializeField] private GameObject optionMenu;

    [SerializeField] private GameObject profileMenu;

    [SerializeField] private GameObject profileAvatarImage;

    [SerializeField] private GameObject profileFrameImage;

    [SerializeField] private Text profileCoins;

    private GameObject profile;

    private void Awake()
    {
        profile = Finder.FindGameProfile();
        string[] sprites = profile.GetComponent<BaseProfile>()._profilePicture.Split('%');
        string avatar = sprites[1];
        string frame = sprites[0];
        ProfileUtil.SetupProfileImageFromResources(avatar, frame, profileAvatarImage, profileFrameImage);
        profileCoins.text = profile.GetComponent<BaseProfile>()._playerCoins;
    }

    public void PressOptionMenu() {
        Debug.Log("Press option button");

        MainMenuUICanvasManager.Instance.ShowSettingCanvas();
    }

    public void PressProfileMenu()
    {
        Debug.Log("Press profile button");

        Instantiate(profileMenu);
    }


}