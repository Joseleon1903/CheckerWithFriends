using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static HostMatchGameBehavour;
using Time = HostMatchGameBehavour.Time;

namespace Assets.Script.WebSocket
{
    public class GameClient
    {
        public string name;
        public bool isHost;
        public string lobbyCode;

    }

    public enum ConnectionConfiguration { 
        LOCAL,
        INTERNET,
    }

    class SocketConfig : MonoBehaviour
    {
        [SerializeField] private ConnectionConfiguration connectionType;

        [SerializeField] private GameObject serverClientPrefab;

        [SerializeField] private GameObject messageErrorPrefab;

        private int port = 8085;

        private string remoteHost = "router-game-server.herokuapp.com";

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            InitServerConnection();
        }

        public void InitServerConnection() {
            Instantiate(serverClientPrefab);
        }

        public string GetSocketHost() {

            string url= "ws://";

            if (Application.platform == RuntimePlatform.WindowsEditor  || Application.platform == RuntimePlatform.WindowsPlayer
                || Application.platform == RuntimePlatform.WebGLPlayer)
            {

                switch (connectionType)
                {

                    default:
                    case ConnectionConfiguration.LOCAL:

                          url = $"ws://localhost:{port}/checker/lobby-manager";

                        break;

                    case ConnectionConfiguration.INTERNET:      

                          url += remoteHost + "/checker/lobby-manager";
                       
                        break;
                }
            }
            Debug.Log("Host connection : "+ url);
            return url;
        }

        public void StartGame(string map, string time, string gameType)
        {

            // Game In Scene Checker - Park - Day
            if (string.Equals(map.ToUpper(), Map.Park.ToString().ToUpper()) && string.Equals(time.ToUpper(), Time.Day.ToString().ToUpper())
                && string.Equals(gameType.ToUpper(), GameType.CHECKER.ToString().ToUpper()))
            {

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

        private IEnumerator LauncherNewSceneAfterTime(string sceneName, float time)
        {

            Debug.Log("Entering in caroutines LauncherNewSceneAfterTime");
            yield return new WaitForSeconds(time);

            Debug.Log("Wait yime is finish star game scene");
            SceneManager.LoadSceneAsync(sceneName);
        }


        public void ShowMessageError() {
            Instantiate(messageErrorPrefab);
        }

    }
}
