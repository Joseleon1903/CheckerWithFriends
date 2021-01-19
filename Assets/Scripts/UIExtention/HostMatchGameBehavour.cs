using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket;
using Assets.Scripts.WebSocket.Message;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HostMatchGameBehavour : MonoBehaviour
{
    public enum Map
    {
        Park = 0,
        City = 1
    }

    public enum Time
    {
        Day = 0,
        Night = 1
    }

    [SerializeField] private GameObject HostGamePanel;

    [SerializeField] private GameObject HostMatchPanel;

    [SerializeField] private GameObject PlayerLobbyContentPanel;

    [SerializeField] private GameObject MatchPanelPrefab;

    //menu
    [Tooltip("Map option Gameobject")]
    [SerializeField] private GameObject MapDowpDown;

    [Tooltip("Time option Gameobject")]
    [SerializeField] private GameObject TimeDowpDown;

    [Tooltip("Match type toogle group")]
    [SerializeField] private GameObject typeGroup;

    public string HostGameMapSelection { get; private set; }

    public string HostGameTimeSelection { get; private set; }

    public string HostGameTypeSelection { get; private set; }

    private void OnEnable()
    {
        HostGamePanel.SetActive(true);
        HostMatchPanel.SetActive(true);
    }

    public void PressHostGame() {

        Debug.Log("Entering in PressHostGame");

        HostMatchPanel.SetActive(false);

        //LeanTween.moveX(HostGamePanel,550f, 1f).setEaseLinear();

        //get menu value 

        int mapUI = MapDowpDown.GetComponentInChildren<Dropdown>().value;
        Map map = EnumHelper.GetEnumValue<Map>(mapUI);
        HostGameMapSelection = map.ToString();

        int timeUI = TimeDowpDown.GetComponentInChildren<Dropdown>().value;
        Time time = EnumHelper.GetEnumValue<Time>(timeUI);
        HostGameTimeSelection = time.ToString();

        Toggle selected = GetSelectedToggle(typeGroup);
        string type = selected.GetComponentInChildren<Text>().text;

        Debug.Log($"Host match: {map}  - {time} - {type}");

        string lobbyCode = LobbyCodeGenerator.GenerateLobbyCode(LobbyCodeGenerator.CODE_LENGHT);

        Debug.Log("Lobby Code Generated: " + lobbyCode);

        string gameType = GameTypeUtil.GetGameConfigType().ToString().ToUpper();

        HostGameTypeSelection = gameType;

        FindObjectOfType<ServerBehavour>().CreateLobby(map.ToString(), time.ToString(), type, lobbyCode, gameType);

        //connect local client to lobby

        StartCoroutine(ConnectLocalClientLobby(lobbyCode, type));
        
    }

    public void PressCloseHostPanel() {

        Debug.Log("Entering in CloseHostPanel");

        MultiplayerButtonActionBehavour.Instance.ShowPublicGamePanel();

        var client = GameObject.FindGameObjectWithTag("ClientWS");

        Destroy(client);

        var server = GameObject.FindGameObjectWithTag("ServerWS");

        Destroy(server);

    }

    public void ShowClientConnect(PlayerInfo playerTwoInfo) {

        var panel = ComponentChildrenUtil.FindComponentInChildWithName<Component>(PlayerLobbyContentPanel, "PlayerTwoPanel");

        if (panel != null) {
            Destroy(panel.gameObject);
        }

        MatchPanelPrefab = Instantiate(MatchPanelPrefab, PlayerLobbyContentPanel.transform);

        var playerName = ComponentChildrenUtil.FindComponentInChildWithName<Text>(MatchPanelPrefab, "PlayerName");
        playerName.text = playerTwoInfo.name;

        var playerId= ComponentChildrenUtil.FindComponentInChildWithName<Text>(MatchPanelPrefab, "PlayerId");
        playerId.text = playerTwoInfo.playerId;

        var playerNationality = ComponentChildrenUtil.FindComponentInChildWithName<Text>(MatchPanelPrefab, "PlayerNation");
        playerNationality.text = playerTwoInfo.nationality;

    }

    private Toggle GetSelectedToggle(GameObject toggleGoup)
    {
        Toggle[] toggles = toggleGoup.GetComponentsInChildren<Toggle>();
        foreach (var t in toggles)
        {
            if (t.isOn) return t;
        }
        return null;
    }

    IEnumerator ConnectLocalClientLobby(string lobbyCode, string type) {

        yield return new WaitForSeconds(3);

        var clientWS = FindObjectOfType<ClientWSBehavour>();

        clientWS.profile.isHost = true;

        clientWS.profile.lobbyCode = lobbyCode;

        clientWS.profile.name = FindObjectOfType<GuestProfile>().name;

        clientWS.ConnectToLobby(lobbyCode, type, "PL1");
    }

}
