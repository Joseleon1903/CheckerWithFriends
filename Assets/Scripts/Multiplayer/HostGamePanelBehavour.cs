using Assets.Scripts.Profile;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

public class HostGamePanelBehavour : MonoBehaviour
{

    [SerializeField] private GameObject profileAvatar;

    [SerializeField] private GameObject profileFrame;

    [SerializeField] private Text profileName;

    [SerializeField] private Text profileId;

    [SerializeField] private Text nationality;

    private GameObject profile;

    private void Awake()
    {
        Debug.Log("Entering in Awake method HostGamePanelBehavour");
        profile = Finder.FindGameProfile();
    }
    // Start is called before the first frame update
    void Start()
    {
        BaseProfile baseProfile = profile.GetComponent<BaseProfile>();
        profileName.text = baseProfile._nameProfile;
        profileId.text = (baseProfile._isGuest) ? baseProfile._guestUserId : baseProfile._facebookUserId;
        nationality.text = baseProfile._nationality;
        ProfileUtil.SetUpProfileImage(profile, profileAvatar, profileFrame);
    }

}
