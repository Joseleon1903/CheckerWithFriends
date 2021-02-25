using Assets.Script.WebSocket;
using Assets.Scripts.Profile;
using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket;
using Assets.Scripts.WebSocket.Message;
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

    [SerializeField] private GameObject HostMatchPanel;

    [SerializeField] private GameObject playerRoomPanel;

    //menu
    [Tooltip("Map option Gameobject")]
    [SerializeField] private GameObject MapDowpDown;

    [Tooltip("Time option Gameobject")]
    [SerializeField] private GameObject TimeDowpDown;

    [Tooltip("Match type toogle group")]
    [SerializeField] private GameObject typeGroup;

    [Tooltip("Match bet toogle group")]
    [SerializeField] private GameObject betGroup;

    public string HostGameMapSelection { get; private set; }

    public string HostGameTimeSelection { get; private set; }

    public string HostGameTypeSelection { get; private set; }

    private void OnEnable()
    {
        if (FindObjectOfType<ServerBehavour>() == null) {

            FindObjectOfType<SocketConfig>().InitServerConnection();
        }

    }

    public void PressHostGame() {

        Debug.Log("Entering in PressHostGame");

        //HostMatchPanel.SetActive(false);

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

        string gameType = "CHECKER";

        HostGameTypeSelection = gameType;

        Toggle betSelected = GetSelectedToggle(betGroup);
        string bet = betSelected.GetComponentInChildren<Text>().text;

        FindObjectOfType<ServerBehavour>().CreateLobby(map.ToString(), time.ToString(), type, lobbyCode, gameType, bet);

        PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_LOBBY_MAP, map.ToString());
        PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_LOBBY_TIME, time.ToString());
        PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_LOBBY_TYPE, type);
        PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_LOBBY_GAME_TYPE, gameType);
        PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_LOBBY_CODE, lobbyCode);
        PlayerPrefs.SetString(PlayerPreferenceKey.PLAYER_LOBBY_BET, bet);
    }

    public void ShowRoomCreated() {

        gameObject.SetActive(false);

        playerRoomPanel.SetActive(true);
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
}
