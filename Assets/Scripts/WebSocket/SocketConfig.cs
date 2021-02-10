using UnityEngine;

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

        private int port = 8085;

        private string remoteHost = "router-game-server.herokuapp.com";

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
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
    }
}
