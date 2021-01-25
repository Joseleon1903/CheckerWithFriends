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


    private void Awake()
    {
        Debug.Log("Entering awake in profile canvas behavour");

        GameObject profile = Finder.FindGameProfile();

        SetUpProfileImage(profile);
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


    public void SetUpProfileImage(GameObject profile) {

        Debug.Log("Entering in SetUpProfileImage");

        BaseProfile profileBase = profile.GetComponent<BaseProfile>();
        if (profileBase._isGuest)
        {
            profileAvatarImage.GetComponent<Image>().sprite = profile.GetComponent<GuestProfile>().ProfileAvatarSprite;
            profileFrameImage.GetComponent<Image>().sprite = profile.GetComponent<GuestProfile>().ProfileFrameSprite;
        }
        else 
        {
            profileAvatarImage.GetComponent<Image>().sprite = profile.GetComponent<FacebookProfile>().ProfileAvatarSprite;
            profileFrameImage.GetComponent<Image>().sprite = profile.GetComponent<FacebookProfile>().ProfileFrameSprite;
        }

    }
}
