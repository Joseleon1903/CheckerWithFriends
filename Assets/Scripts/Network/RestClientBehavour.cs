using Assets.Script.WebSocket;
using UnityEngine;

public class RestClientBehavour : MonoBehaviour
{
    public static RestClientBehavour Instance;

    [SerializeField]
    private string hostPort = string.Empty;

    public string ApiBaseUrl { get{ return GetApi(); } }

    [SerializeField]
    [Tooltip("Type of connection for service")]
    private ConnectionConfiguration connectionType;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    /// <summary>
    ///  get api url base for your ConnectionConfiguration
    /// </summary>
    /// <returns>string</returns>
    private string GetApi()
    {
        string api = "";
        if (Application.platform == RuntimePlatform.Android)
        {

            return "https://router-game-server.herokuapp.com";
        }

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            return "https://router-game-server.herokuapp.com";
        }

        switch (connectionType)
        {
            case ConnectionConfiguration.INTERNET:
                Debug.Log("Internet Api connection type");
                api = "https://router-game-server.herokuapp.com";
                break;

            case ConnectionConfiguration.LOCAL:
                Debug.Log("Local Api connection type");
                string host = "http://localhost:";
                string port ="8080";
                if (!hostPort.Equals(string.Empty)) {
                    port = hostPort;
                }
                api = host + port;
                break;
        }
        return api;
    }

}
