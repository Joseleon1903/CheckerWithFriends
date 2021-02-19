using UnityEngine;
using Unity.Pandora.Core.Mobile.Animation.Tween;
using UnityEngine.UI;
using Assets.Scripts.Utils;
using Assets.Scripts.Profile;
using Assets.Scripts.Json;
using Proyecto26;

public class ProfileOptionBehavour : MonoBehaviour
{

    [SerializeField] private GameObject avatar;

    [SerializeField] private GameObject frame;

    [SerializeField] private GameObject profileFlag;

    [SerializeField] private Text profileName;

    [SerializeField] private Text profileId;

    [SerializeField] private Text profileNationality;

    [SerializeField] private Text TotalCheckerGame;

    [SerializeField] private Text TotalCheckerGameWin;

    [SerializeField] private GameObject panelContent;


    // Start is called before the first frame update
    void Awake()
    {
        if (Finder.FindGameProfile() != null)
        {
            SetUpProfileInformation();
        }
    }

    private void OnEnable()
    {
        LeanTween.scale(panelContent, new Vector3(1f, 1f, 1f), 1.5f).setEaseOutBounce();
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
            //fix for facebook account 
            avatar.GetComponent<Image>().sprite = profile.GetComponent<FacebookProfile>().ProfileAvatarSprite;
            frame.GetComponent<Image>().sprite = profile.GetComponent<FacebookProfile>().ProfileFrameSprite;
            name = profile.GetComponent<GuestProfile>().ProfileName;
            id = profile.GetComponent<GuestProfile>().ProfileId;
        }

        profileFlag.GetComponent<Image>().sprite = GetNationalitySprite(profile.GetComponent<BaseProfile>()._nationality);
        nation = ProfileUtil.GetNationalityName(profile.GetComponent<BaseProfile>()._nationality);
        totChecker = "Total Checker Game: " + profile.GetComponent<BaseProfile>()._totalCheckerGame;
        totCheckerWin = "Total Checker Game Win: " + profile.GetComponent<BaseProfile>()._totalCheckerGameWin;

        //set visual 

        profileName.text = name;
        profileId.text = id;
        profileNationality.text = nation;
        TotalCheckerGame.text = totChecker;
        TotalCheckerGameWin.text = totCheckerWin;
    }

    private Sprite GetNationalitySprite(string key)
    {
        string jsonNation = ResourcesUtil.FindStringJsonFileInResource(ResourcesUtil.JSON_NATIONALITY);

        NationalityJson[] jsonArray = JsonHelper.ArrayFromJson<NationalityJson>(jsonNation);

        string fileName = string.Empty;

        foreach (NationalityJson j in jsonArray)
        {
            if (j.flagKey.Equals(key))
            {

                fileName = j.flagFileName;
            }
        }
        return ResourcesUtil.FindNationalityFlagSpriteInResource(fileName);
    }


}
