using Assets.Scripts.Profile;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Camera))]
public class SceneMainMenuValidation : MonoBehaviour
{
   
    void Awake()
    {
        ValidationRequireObjectForMainMenuScene();
    }

    #region  Validation Require object for scene

    public void ValidationRequireObjectForMainMenuScene() {

        // profile
        BaseProfile profileBase = FindObjectOfType<BaseProfile>();

        Assert.IsNotNull(profileBase, "Profile Game object is requiere");

    }
    #endregion


}
