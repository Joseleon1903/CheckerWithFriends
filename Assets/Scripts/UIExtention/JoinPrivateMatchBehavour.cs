using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket;
using UnityEngine;
using UnityEngine.UI;

public class JoinPrivateMatchBehavour : MonoBehaviour
{

    [SerializeField] private InputField lobbyCodeInput;

    [SerializeField] private GameObject lobbyErrorPanel;

    private void Start()
    {
        //Animation
        //LeanTween.scale(joinPanel, new Vector3(1f, 1f,1f), 1.5f).setEaseOutBounce();
    }
     
    public void PressJoinPrivateButton() {

        Debug.Log("Press Join private button");

        string lobbyCode = lobbyCodeInput.text;

        if (lobbyCode.Length == 0) {

            Debug.Log("Lobby code is required");

            ShowErrorPanel("Lobby code is required");

            return;
        }

        Debug.Log("try lobby connection");

        var clientWs = FindObjectOfType<ClientWSBehavour>();

        clientWs.profile.isHost = false;

        clientWs.profile.lobbyCode = lobbyCode;

        clientWs.profile.name = PlayerPrefs.GetString(PlayerPreferenceKey.PROFILE_ONE_KEY_NAME);

        clientWs.ConnectToLobby(lobbyCode, "Private", "PL2");

    }


    public void ShowErrorPanel(string text) {
        Debug.Log("Entering in method ShowErrorPanel");

        lobbyErrorPanel.SetActive(true);

        lobbyErrorPanel.GetComponent<JoinErrorPanelBehavour>().ChangeMessage(text);

    }

    public void ClosePanel() {

        var client = GameObject.FindGameObjectWithTag("ClientWS");

        Destroy(client);
    }
}
