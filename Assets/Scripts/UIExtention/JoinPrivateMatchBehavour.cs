using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket;
using Unity.Pandora.Core.Mobile.Animation.Tween;
using UnityEngine;
using UnityEngine.UI;

public class JoinPrivateMatchBehavour : MonoBehaviour
{
    [SerializeField] private GameObject joinPanel;

    private InputField lobbyCodeInput;

    private void Awake()
    {
        lobbyCodeInput = ComponentChildrenUtil.FindComponentInChildWithName<InputField>(joinPanel, "LobbyInputField");

    }

    private void Start()
    {
        //Animation
        LeanTween.scale(joinPanel, new Vector3(1f, 1f,1f), 1.5f).setEaseOutBounce();
    }

    public void PressJoinPrivateButton() {

        LoggerFile.Instance.DEBUG_LINE("Press Join private button");

        Debug.Log("Press Join private button");

        string lobbyCode = lobbyCodeInput.text;

        if (lobbyCode.Length == 0) {

            Debug.Log("Lobby code is required");

            LoggerFile.Instance.DEBUG_LINE("Lobby Code is Requiere");

            return;
        }

        Debug.Log("try lobby connection");
        LoggerFile.Instance.DEBUG_LINE("try lobby connection");

        var clientWs = FindObjectOfType<ClientWSBehavour>();

        clientWs.profile.isHost = false;

        clientWs.profile.lobbyCode = lobbyCode;

        clientWs.profile.name = FindObjectOfType<GuestProfile>().name;

        clientWs.ConnectToLobby(lobbyCode, "Private", "PL2");

    }

    public void ClosePanel() {

        var client = GameObject.FindGameObjectWithTag("ClientWS");

        Destroy(client);

        Destroy(gameObject);
    }
}
