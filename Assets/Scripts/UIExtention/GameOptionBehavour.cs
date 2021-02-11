using Assets.Scripts.Network.Model;
using Assets.Scripts.Network.Service;
using Assets.Scripts.Utils;
using Unity.Pandora.Core.Mobile.Animation.Tween;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionBehavour : MonoBehaviour
{
    [SerializeField] private GameObject optionPanel;

    [SerializeField] private Material activeToggle;

    [SerializeField] private Material inactiveToggle;

    [SerializeField] private GameObject soundOptionObj;

    [SerializeField] private GameObject effectOptionObj;

    [SerializeField] private GameObject languageOptionObj;

    private string language, soundOption, effectOption;

    private void Awake()
    {
        LoadPlayerPreference();
    }

    private void OnEnable()
    {
        LoadPlayerPreference();

        bool optionSound = soundOption.Equals(EnumHelper.TRUE) ? true: false;
        bool optionEffect = effectOption.Equals(EnumHelper.TRUE) ? true : false;

        SetUpToogleContent(optionSound, soundOptionObj);
        SetUpToogleContent(optionEffect, effectOptionObj);

        SetUpLanguageContent(language, languageOptionObj);

        LeanTween.scale(optionPanel, new Vector3(1f, 1f, 1f), 1.5f).setEaseOutBounce();
    }

    private void LoadPlayerPreference() {
        language = PlayerPrefs.GetString(PlayerPreferenceKey.PLAYER_GAME_LANGUAGE);
        soundOption = PlayerPrefs.GetString(PlayerPreferenceKey.PLAYER_GAME_SOUND);
        effectOption = PlayerPrefs.GetString(PlayerPreferenceKey.PLAYER_GAME_SOUND_EFFECT);
    }

    private void SetUpToogleContent(bool value, GameObject toggleObj) {

        Image toggleimage = ComponentChildrenUtil.FindComponentInChildWithName<Image>(toggleObj, "ToggleImage");
        Toggle toggle = ComponentChildrenUtil.FindComponentInChildWithName<Toggle>(toggleObj, "ToggleImage");
        toggle.isOn = value;
        if (!value)
        {
            toggleimage.color = inactiveToggle.color;
        }
        else if (value)
        {
            toggleimage.color = activeToggle.color;
        }
    }

    private void SetUpLanguageContent(string language , GameObject selectObj) {

        Dropdown languageDropdown = ComponentChildrenUtil.FindComponentInChildWithName<Dropdown>(languageOptionObj, "DropdownLanguage");

        Debug.Log("Current value : " + languageDropdown.value);

        if (language.Equals("EN")) {

            languageDropdown.value = 0;        
        }

        if (language.Equals("ES"))
        {
            languageDropdown.value = 1;
        }

        if (language.Equals("IT"))
        {
            languageDropdown.value = 2;
        }
    }

    public void SoundToggleValueChanged(bool value) {

        Debug.Log("Sound Value : "+ value);
        if (value)
        {
            soundOption = EnumHelper.TRUE;
        }
        else 
        {
            soundOption = EnumHelper.FALSE;
        }

        SetUpToogleContent(value, soundOptionObj);
    }

    public void EffectToggleValueChanged(bool value)
    {
        Debug.Log("Effect Value : " + value);
        if (value)
        {
            effectOption = EnumHelper.TRUE;
        }
        else
        {
            effectOption = EnumHelper.FALSE;
        }
        SetUpToogleContent(value, effectOptionObj);
    }

    public void OnLanguageChangeValue(int value) {
        Debug.Log("Entering in Language change value");
        switch (value) {
            case 0:
                language = "EN";
                PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_GAME_LANGUAGE, "EN");
                break;
            case 1:
                language = "ES";
                PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_GAME_LANGUAGE, "ES");
                break;
            case 2:
                language = "IT";
                PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_GAME_LANGUAGE, "IT");
                break;
        }
    }

    private void OnDisable()
    {
        Debug.Log("Entering in disable");
        PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_GAME_LANGUAGE, language);
        PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_GAME_SOUND,soundOption);
        PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_GAME_SOUND_EFFECT, effectOption);
    }

    public void PressCloseBtn() {

        Debug.Log("Press on Close");

        ProfileModel profileM = new ProfileModel();

        profileM = ProfileUtil.GetProfileModel();

        //send Post request
        string api = RestClientBehavour.Instance.ApiBaseUrl + ProfileService.CreateProfilePath;

        ProfileService serviceVersion = new ProfileService();

        serviceVersion.PostProfile(api, profileM).Then(response =>
        {
            //fordware next scene
            gameObject.SetActive(false);
        });

    } 
}
