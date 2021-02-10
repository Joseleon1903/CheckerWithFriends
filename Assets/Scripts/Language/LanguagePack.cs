using UnityEngine;

public class LanguagePack 
{
    public LanguageJsonModel LanguageDictionary { get; }

    public LanguagePack(string languageId) 
    {
        string json = ResourcesUtil.FindLanguageInResource(languageId);

        Debug.Log("language json: "+ json);

        LanguageDictionary = JsonUtility.FromJson<LanguageJsonModel>(json);
    }


    public string  FindKeyInDictionary(string key) {
        foreach (LanguageKeys k in LanguageDictionary.languageKeys) {
            if (key.Equals(k.id)) {
               return k.value;
            }
        }
        return null;
    }

}

[System.Serializable]
public class LanguageJsonModel {

    public string languageId;
    public string gameName;

    public LanguageKeys[] languageKeys;

}

[System.Serializable]
public class LanguageKeys
{
    public string id;
    public string value;
}