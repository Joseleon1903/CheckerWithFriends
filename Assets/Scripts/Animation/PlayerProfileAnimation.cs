using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket;
using Unity.Pandora.Core.Mobile.Animation.Tween;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProfileAnimation : MonoBehaviour
{

    [SerializeField]private GameObject panelProfile;


    //player One
    [SerializeField] private GameObject profileOneFrame;
    [SerializeField] private GameObject profileOneAvatar;
    [SerializeField] private Text profileOneName;

    //player Two
    [SerializeField] private GameObject profileTwoFrame;
    [SerializeField] private GameObject profileTwoAvatar;
    [SerializeField] private Text profileTwoName;


    private void Awake()
    {
        Debug.Log("Setup profile player images");
        if (FindObjectOfType<ClientWSBehavour>() == null) {
            return;
        }

        string avatar, frame, name;
        avatar = PlayerPrefs.GetString(PlayerPreferenceKey.PROFILE_ONE_KEY_AVATAR);
        frame = PlayerPrefs.GetString(PlayerPreferenceKey.PROFILE_ONE_KEY_FRAME);
        name = PlayerPrefs.GetString(PlayerPreferenceKey.PROFILE_ONE_KEY_NAME);

        ProfileUtil.SetupProfileImageFromResources(avatar, frame, profileOneAvatar, profileOneFrame);
        profileOneName.text = name;

        avatar = PlayerPrefs.GetString(PlayerPreferenceKey.PROFILE_TWO_KEY_AVATAR);
        frame = PlayerPrefs.GetString(PlayerPreferenceKey.PROFILE_TWO_KEY_FRAME);
        name = PlayerPrefs.GetString(PlayerPreferenceKey.PROFILE_TWO_KEY_NAME);

        ProfileUtil.SetupProfileImageFromResources(avatar, frame, profileTwoAvatar, profileTwoFrame);
        profileTwoName.text = name;
    }


    private void OnEnable()
    {
        LeanTween.scale(panelProfile, Vector3.one, 1.5f).setEase(LeanTweenType.easeOutElastic);
    }

    private void OnDisable()
    {
        
    }
}
