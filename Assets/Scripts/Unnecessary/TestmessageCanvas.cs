using Assets.Scripts.Utils;
using UnityEngine;

public class TestmessageCanvas : MonoBehaviour
{

    [SerializeField] private GameObject messageCanvas;

    private void Start()
    {
        PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_MESSAGE_PANEL_TITTLE, "Internet Connection Error");
        PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_MESSAGE_PANEL_TEXT, "An error has been verified trying to connect with the server, please validate your internet connection");
    }
       

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(messageCanvas);
        }

    }
}
