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
        Debug.Log("Entering awake in profile canvas behavour");

        profile = Finder.FindGameProfile();

    }

    private void Start()
    {

        Invoke("SetupProfile", 0.3f);
    }

    private void SetupProfile() {
        ProfileUtil.SetUpProfileImage(profile, profileAvatarImage, profileFrameImage);
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
