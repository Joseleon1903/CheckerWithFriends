using Assets.Scripts.Profile;
using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProfileMultiplayerBehavour : MonoBehaviour
{
    private GameObject profile;

    [SerializeField] private Text playerCoins;

    private void Awake()
    {
        Debug.Log("Entering awake in profile Multiplayer canvas behavour");

        profile = Finder.FindGameProfile();

        playerCoins.text = profile.GetComponent<BaseProfile>()._playerCoins;

    }

    public void PressBackButton() {

        Debug.Log("Press Back Button");

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
