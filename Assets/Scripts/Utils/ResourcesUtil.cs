using UnityEngine;

public class ResourcesUtil
{
    public static string JSON_AVATAR = "AvatarsJson";
    public static string JSON_FRAME = "FramesJson";

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

}
