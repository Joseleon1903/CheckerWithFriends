using Assets.Scripts.Json;
using UnityEngine;
using static HostMatchGameBehavour;

public class ResourcesUtil
{
    public static string JSON_AVATAR = "AvatarsJson";
    public static string JSON_FRAME = "FramesJson";

    public static string JSON_NATIONALITY = "Flag/Nationality_Flag";

    public static string JSON_RULES = "Rules/RulesInstruction";

    public static string MATERIAL_COLOR_GOLD = "Gold_Color";
    public static string MATERIAL_COLOR_RED = "Red_Color";

    public static Material FindMaterialInResource(string material) { 
        return Resources.Load<Material>("Material/Color/" + material); ;
    }

    public static string FindStringJsonFileInResource(string jsonRoot)
    {
        string filePath = "Json/" + jsonRoot;

        TextAsset targetFile = Resources.Load<TextAsset>(filePath);

        return targetFile.text;
    }

    public static Sprite FindProfileSpriteInResource(string spriteName)
    {
        string filePath = "Sprites/Profile/" + spriteName;

        Sprite targetFile = Resources.Load<Sprite>(filePath);

        return targetFile;
    }

    public static Sprite FindMapSpriteInResource(Map mapType)
    {
        string root = string.Empty;

        switch (mapType) {

            case Map.City:

                root = "Sprites/Map/City-Map";

                break;

            case Map.Park:
                root = "Sprites/Map/Park-Map";

                break;
        }

        Sprite targetFile = Resources.Load<Sprite>(root);

        return targetFile;
    }

    public static Sprite FindNationalityFlagSpriteInResource(string flagName)
    {
        string filePath = "Sprites/Flags/" + flagName;

        Sprite targetFile = Resources.Load<Sprite>(filePath);

        return targetFile;
    }

    public static string FindLanguageInResource(string languagekey)
    {
        string filePath = "Json/Lang/Language_" + languagekey;

        TextAsset targetFile = Resources.Load<TextAsset>(filePath);

        return targetFile.text;
    }

}
