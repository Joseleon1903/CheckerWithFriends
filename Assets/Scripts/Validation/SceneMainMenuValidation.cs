using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SceneMainMenuValidation : MonoBehaviour
{
   
    void Awake()
    {
        ValidationRequireObjectForMainMenuScene();
    }

    #region  Validation Require object for scene

    public void ValidationRequireObjectForMainMenuScene() {

        GameObject serverWS = GameObject.FindGameObjectWithTag("ServerWS");

        if (serverWS != null) {
            Destroy(serverWS, 1.0f);
        }

        GameObject clientWS = GameObject.FindGameObjectWithTag("ClientWS");

        if (clientWS != null)
        {
            Destroy(clientWS);
        }

    }
    #endregion


}
