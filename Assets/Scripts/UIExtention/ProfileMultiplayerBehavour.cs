using Assets.Scripts.WebSocket;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileMultiplayerBehavour : MonoBehaviour
{
    public void PressBackButton() {

        LoggerFile.Instance.DEBUG_LINE("Press Back Button");

        SceneManager.LoadScene("MainMenu");

        if (FindObjectOfType<ClientWSBehavour>() != null) {
            var client = FindObjectOfType<ClientWSBehavour>();
            Destroy(client);
        }

        if (FindObjectOfType<ServerBehavour>() != null)
        {
            var server = FindObjectOfType<ServerBehavour>();
            Destroy(server);
        }
    }

}
