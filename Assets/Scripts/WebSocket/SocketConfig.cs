using UnityEngine;

namespace Assets.Script.WebSocket
{
    public enum MultiplayerLobbyType
    {
        Chess,
        Checker,
    }
    public enum ConnectionConfiguration { 
        LOCAL,
        INTERNET,
    }

    class SocketConfig : MonoBehaviour
    {
        [SerializeField] private ConnectionConfiguration connectionType;

        [SerializeField] private MultiplayerLobbyType _lobbyType;

        public MultiplayerLobbyType LobbyType => this._lobbyType;

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

                        if (LobbyType.Equals(MultiplayerLobbyType.Checker)) {

                            url = $"ws://localhost:{port}/checker/lobby-manager";

                        }else if (LobbyType.Equals(MultiplayerLobbyType.Chess)) {

                            url = $"ws://localhost:{port}/chess/lobby-manager";
                        }

                        break;

                    case ConnectionConfiguration.INTERNET:

                        if (LobbyType.Equals(MultiplayerLobbyType.Checker))
                        {

                            url += remoteHost + "/checker/lobby-manager";

                        }
                        else if (LobbyType.Equals(MultiplayerLobbyType.Chess))
                        {

                            url += remoteHost + "/chess/lobby-manager";
                        }

                        break;
                }
            }
            Debug.Log("Host connection : "+ url);
            return url;
        }
    }
}
