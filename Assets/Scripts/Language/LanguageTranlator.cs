using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LanguageTranlator : MonoBehaviour
{
    private Text label;

    private LanguagePack languagePack;

    [SerializeField] private string labelKey;

    [SerializeField] private bool ToUpper;

    [SerializeField] private bool ToLower;


    private void Awake()
    {
        if (PlayerPrefs.HasKey(PlayerPreferenceKey.PLAYER_GAME_LANGUAGE))
        {
            string languageKey = PlayerPrefs.GetString(PlayerPreferenceKey.PLAYER_GAME_LANGUAGE);
            languagePack = new LanguagePack(languageKey);
        }
        else
        {
            languagePack = new LanguagePack(LangUtils.LANGUAGE_ENGLISH_KEY);
        }
    }

    void Start()
    {
        label = GetComponent<Text>();

        string text = languagePack.FindKeyInDictionary(labelKey);

        if (ToUpper) 
        {
            text = text.ToUpper();
        }

        if (ToLower)
        {
            text = text.ToLower();
        }

        label.text = text;
    }

}
