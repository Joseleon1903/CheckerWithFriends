using Assets.Scripts.WebSocket;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerBehavour : MonoBehaviour
{

    private void Awake()
    {
        // remove client and server for lost connection player
        StartCoroutine(CleanScene());
    }

    public void ChangeSceneS(string scene) {
        Debug.Log($"Load scene {scene}");
        SceneManager.LoadScene(scene);
    }

    public void ChangeSceneN(int scene)
    {
        Debug.Log($"Load scene {scene}");
        SceneManager.LoadScene(scene);
    }

    private IEnumerator CleanScene()
    {
        if (FindObjectOfType<ClientWSBehavour>() != null)
        {
            var client = FindObjectOfType<ClientWSBehavour>();
            Destroy(client.gameObject);
        }
        yield return new WaitForSeconds(1.5f);

        if (FindObjectOfType<ServerBehavour>() != null)
        {
            var server = FindObjectOfType<ServerBehavour>();
            Destroy(server.gameObject);
        }
    }

}
