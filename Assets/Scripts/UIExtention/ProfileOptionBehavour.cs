using UnityEngine;
using Unity.Pandora.Core.Mobile.Animation.Tween;
using UnityEngine.UI;
using Assets.Scripts.Utils;
using Assets.Scripts.Profile;

public class ProfileOptionBehavour : MonoBehaviour
{

    [SerializeField] private GameObject optionPanel;

    [SerializeField] private GameObject avatar;

    [SerializeField] private GameObject frame;

    [SerializeField] private Text profileName;

    [SerializeField] private Text profileId;

    [SerializeField] private Text nationality;

    [SerializeField] private Text TotalCheckerGame;

    [SerializeField] private Text TotalCheckerGameWin;

    // Start is called before the first frame update
    void Start()
    {
        if (Finder.FindGameProfile() != null)
        {

            SetUpProfileInformation();
        }

        LeanTween.scale(optionPanel, new Vector3(0.8f, 0.8f, 0.8f), 1.5f).setEaseOutBounce();
    }

    private void SetUpProfileInformation()
    {
        GameObject profile = Finder.FindGameProfile();

        string name,id , nation, totChecker, totCheckerWin;

        BaseProfile profileBase = profile.GetComponent<BaseProfile>();
        if (profileBase._isGuest)
        {
            avatar.GetComponent<Image>().sprite = profile.GetComponent<GuestProfile>().ProfileAvatarSprite;
            frame.GetComponent<Image>().sprite = profile.GetComponent<GuestProfile>().ProfileFrameSprite;
            name = profile.GetComponent<GuestProfile>().ProfileName;
            id = profile.GetComponent<GuestProfile>().ProfileId;
        }
        else
        {
            avatar.GetComponent<Image>().sprite = profile.GetComponent<FacebookProfile>().ProfileAvatarSprite;
            frame.GetComponent<Image>().sprite = profile.GetComponent<FacebookProfile>().ProfileFrameSprite;
            name = profile.GetComponent<GuestProfile>().ProfileName;
            id = profile.GetComponent<GuestProfile>().ProfileId;
        }
        nation = profile.GetComponent<BaseProfile>()._nationality;
        totChecker = "Total Checker Game: " + profile.GetComponent<BaseProfile>()._totalCheckerGame;
        totCheckerWin = "Total Checker Game Win: " + profile.GetComponent<BaseProfile>()._totalCheckerGameWin;

        //set visual 

        profileName.text = name;
        profileId.text = id;
        nationality.text = nation;
        TotalCheckerGame.text = totChecker;
        TotalCheckerGameWin.text = totCheckerWin;
    }

    public void PressCloseBtn()
    {
        Destroy(gameObject);
    }

}
