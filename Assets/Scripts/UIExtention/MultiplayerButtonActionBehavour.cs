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

        panelJoin.SetActive(true);

        Instantiate(clientPrefab);
    }

    public void ShowHostPanel() {

        Debug.Log("Show host game panel");

        panelHostGame.SetActive(true);

        Debug.Log("create a server and client for host");

        Instantiate(serverPrefab);

        Instantiate(clientPrefab);
    }

    public void ShowPublicGamePanel() {

        Debug.Log("Show public game panel");

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

        Debug.Log("Show client Join private game panel");

        panelJoin.SetActive(false);

        clientMatchJoinPanel = Instantiate(clientMatchJoinPanel);

        clientMatchJoinPanel.GetComponent<HostMatchJoinBehavour>().SetUpView(playerOnInfo);
    }

    public void StartGame(string map, string time, string gameType) {

        // Game In Scene Checker - Park - Day
        if (string.Equals(map.ToUpper(), Map.Park.ToString().ToUpper())  && string.Equals(time.ToUpper(), Time.Day.ToString().ToUpper())
            && string.Equals(gameType.ToUpper(), GameType.CHECKER.ToString().ToUpper())) {

            StartCoroutine(LauncherNewSceneAfterTime("CheckerParkDayGameScene", 5.0f));

        }
        // Game In Scene Checker - Park - Night
        else if (string.Equals(map.ToUpper(), Map.Park.ToString().ToUpper()) && string.Equals(time.ToUpper(), Time.Night.ToString().ToUpper())
            && string.Equals(gameType.ToUpper(), GameType.CHECKER.ToString().ToUpper()))
        {

            StartCoroutine(LauncherNewSceneAfterTime("CheckerParkNightGameScene", 5.0f));
        }

        // Game In Scene Checker - City - Day
        else if (string.Equals(map.ToUpper(), Map.City.ToString().ToUpper()) && string.Equals(time.ToUpper(), Time.Day.ToString().ToUpper())
            && string.Equals(gameType.ToUpper(), GameType.CHECKER.ToString().ToUpper()))
        {

            StartCoroutine(LauncherNewSceneAfterTime("CheckerCityDayScene", 5.0f));
        }

        // Game In Scene Checker - City - Night
        else if (string.Equals(map.ToUpper(), Map.City.ToString().ToUpper()) && string.Equals(time.ToUpper(), Time.Night.ToString().ToUpper())
            && string.Equals(gameType.ToUpper(), GameType.CHECKER.ToString().ToUpper()))
        {

            StartCoroutine(LauncherNewSceneAfterTime("CheckerCityNightScene", 5.0f));
        }

    }

    private IEnumerator LauncherNewSceneAfterTime(string sceneName, float time) {

        Debug.Log("Entering in caroutines LauncherNewSceneAfterTime");
        yield return new WaitForSeconds(time);

        Debug.Log("Wait yime is finish star game scene");
        SceneManager.LoadSceneAsync(sceneName);
    }
}
