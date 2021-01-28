using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket;
using Assets.Scripts.WebSocket.Message;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static HostMatchGameBehavour;
using Time = HostMatchGameBehavour.Time;

public class MultiplayerButtonActionBehavour : MonoBehaviour
{
    public static MultiplayerButtonActionBehavour Instance;

    private void Awake()
    {
        Instance = this;
    }

    [Tooltip("Panel for join new private match")]
    [SerializeField] private GameObject panelJoin;

    [Tooltip("Panel for host new private or public match")]
    [SerializeField] private GameObject panelHostGame;

    [Tooltip("Panel see public match")]
    [SerializeField] private GameObject panelPublicGame;

    [Tooltip("Panel prepered join private match")]
    [SerializeField] private GameObject clientMatchJoinPanel;

    [Tooltip("Component for Host web socket connection")]
    [SerializeField] private GameObject serverPrefab;

    [Tooltip("Component for client web socket connection")]
    [SerializeField] private GameObject clientPrefab;

    public void InizilizedSingleClient() {

        if (FindObjectOfType<ClientWSBehavour>() == null) {
            Instantiate(clientPrefab);
        }
    }

    public void ShowJoinPrivatePanel() {

        LoggerFile.Instance.DEBUG_LINE("Show join private game panel");

        if (FindObjectOfType<JoinPrivateMatchBehavour>() == null) {
            panelJoin = Instantiate(panelJoin);
        }

        panelJoin.SetActive(true);

        Instantiate(clientPrefab);
    }

    public void ShowHostPanel() {

        LoggerFile.Instance.DEBUG_LINE("Show host game panel");

        panelPublicGame.SetActive(false);

        if (FindObjectOfType<HostMatchGameBehavour>() == null) {
            panelHostGame = Instantiate(panelHostGame);
        }
        panelHostGame.SetActive(true);

        LoggerFile.Instance.DEBUG_LINE("create a server and client for host");

        Instantiate(serverPrefab);

        Instantiate(clientPrefab);
    }

    public void ShowPublicGamePanel() {

        LoggerFile.Instance.DEBUG_LINE("Show public game panel");

        if (panelHostGame.activeSelf) {
            panelHostGame.SetActive(false);
        }

        panelPublicGame.SetActive(true);

        if (panelPublicGame.activeSelf) {
            FindObjectOfType<PublicGameBehavour>().PressRefreshButton();
        }
    }

    public void HidePublicGamePanel() {

        panelPublicGame.SetActive(false);
    }

    public void ShowJoinPrivateGamePanel(PlayerInfo playerOnInfo) {

        LoggerFile.Instance.DEBUG_LINE("Show client Join private game panel");

        panelJoin.SetActive(false);

        clientMatchJoinPanel = Instantiate(clientMatchJoinPanel);

        clientMatchJoinPanel.GetComponent<HostMatchJoinBehavour>().SetUpView(playerOnInfo);
    }

    public void StartGame(string map, string time, string gameType) {

        // Game In Scene Checker - Park - Day
        if (string.Equals(map.ToUpper(), Map.Park.ToString().ToUpper())  && string.Equals(time.ToUpper(), Time.Day.ToString().ToUpper())
            && string.Equals(gameType.ToUpper(), GameType.CHECKER.ToString().ToUpper())) {

            LoggerFile.Instance.DEBUG_LINE("Start game after delay Map: CheckerParkDayGameScene");

            StartCoroutine(LauncherNewSceneAfterTime("CheckerParkDayGameScene", 5.0f));

        }
        // Game In Scene Checker - Park - Night
        else if (string.Equals(map.ToUpper(), Map.Park.ToString().ToUpper()) && string.Equals(time.ToUpper(), Time.Night.ToString().ToUpper())
            && string.Equals(gameType.ToUpper(), GameType.CHECKER.ToString().ToUpper()))
        {
            LoggerFile.Instance.DEBUG_LINE("Start game after delay Map: CheckerParkNightGameScene");

            StartCoroutine(LauncherNewSceneAfterTime("CheckerParkNightGameScene", 5.0f));
        }

        // Game In Scene Checker - City - Day
        else if (string.Equals(map.ToUpper(), Map.City.ToString().ToUpper()) && string.Equals(time.ToUpper(), Time.Day.ToString().ToUpper())
            && string.Equals(gameType.ToUpper(), GameType.CHECKER.ToString().ToUpper()))
        {
            LoggerFile.Instance.DEBUG_LINE("Start game after delay Map: CheckerCityDayScene");

            StartCoroutine(LauncherNewSceneAfterTime("CheckerCityDayScene", 5.0f));
        }

        // Game In Scene Checker - City - Night
        else if (string.Equals(map.ToUpper(), Map.City.ToString().ToUpper()) && string.Equals(time.ToUpper(), Time.Night.ToString().ToUpper())
            && string.Equals(gameType.ToUpper(), GameType.CHECKER.ToString().ToUpper()))
        {
            LoggerFile.Instance.DEBUG_LINE("Start game after delay Map: CheckerCityNightScene"); 

            StartCoroutine(LauncherNewSceneAfterTime("CheckerCityNightScene", 5.0f));
        }

    }

    private IEnumerator LauncherNewSceneAfterTime(string sceneName, float time) {

        yield return new WaitForSeconds(time);

        SceneManager.LoadSceneAsync(sceneName);
    }
}
