using Assets.Scripts.Profile;
using Assets.Scripts.Utils;
using UnityEngine;

public class ProfileCanvasBehavour : MonoBehaviour
{

    [SerializeField] private GameObject optionMenu;

    [SerializeField] private GameObject profileMenu;

    [SerializeField] private GameObject profileAvatarImage;

    [SerializeField] private GameObject profileFrameImage;

    private GameObject profile;

    private void Awake()
    {
        profile = Finder.FindGameProfile();
        string[] sprites = profile.GetComponent<BaseProfile>()._profilePicture.Split('%');
        string avatar = sprites[1];
        string frame = sprites[0];
        ProfileUtil.SetupProfileImageFromResources(avatar, frame, profileAvatarImage, profileFrameImage);
    }

    public void PressOptionMenu() {
        Debug.Log("Press option button");

        Instantiate(optionMenu);
    }

    public void PressProfileMenu()
    {
        Debug.Log("Press profile button");

        Instantiate(profileMenu);
    }


}