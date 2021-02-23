using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanelBehavour : MonoBehaviour
{

    [SerializeField] private GameObject tittlePanel;

    [SerializeField] private GameObject messageText;

    private void Awake()
    {

        string tittle = PlayerPrefs.GetString(PlayerPreferenceKey.PLAYER_MESSAGE_PANEL_TITTLE);
        string message = PlayerPrefs.GetString(PlayerPreferenceKey.PLAYER_MESSAGE_PANEL_TEXT);

        tittlePanel.GetComponent<Text>().text = tittle;
        messageText.GetComponent<Text>().text = message;
    }

    public void CloseButton() {
        Debug.Log("method CloseButton press");
        PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_MESSAGE_PANEL_TITTLE, string.Empty);
        PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_MESSAGE_PANEL_TEXT, string.Empty);
        Destroy(gameObject);
    }
}
