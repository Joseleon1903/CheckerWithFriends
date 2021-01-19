using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket.Message;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ServerCommunication))]
public class ServerBehavour : MonoBehaviour
{
    private ServerCommunication communication;

    public string ServerLobbyCode;

    private Queue<string> messageQueue;

    private void Start()
    {
        communication = GetComponent<ServerCommunication>();

        DontDestroyOnLoad(gameObject);

        messageQueue = new Queue<string>();

        Debug.Log("Init Host server ");

        communication.ConnectToServer();
    }

    private void OnDestroy()
    {
        LoggerFile.Instance.DEBUG_LINE("Server try disconnect communication");
        communication.DisconectToServer();
    }
    public void SendStartGameMessage()
    {
        var hostPanel = FindObjectOfType<HostMatchGameBehavour>();
        string gameType = hostPanel.HostGameTypeSelection;
        string map = hostPanel.HostGameMapSelection;
        string time = hostPanel.HostGameTimeSelection;
        StartGameMessageReq startRequest = new StartGameMessageReq(ServerLobbyCode, map, time, gameType);
        communication.SendRequest(startRequest.GetMessageText());
    }

    public void CreateLobby(string map, string time, string type, string lobbyCode, string gameType) {

        ServerLobbyCode = lobbyCode;
        string LobbyType = (type.Equals("Private")) ? LobbyCodeGenerator.LOBBY_PRIVATE : LobbyCodeGenerator.LOBBY_PUBLIC;
        CreateLobbyMessageReq msg = new CreateLobbyMessageReq(LobbyType, map, time, ServerLobbyCode, 2, LobbyCodeGenerator.ONLINE_STATUS, gameType);

        messageQueue.Enqueue(msg.GetMessageText());

        // update status message 
        if (type.Equals("Private"))
        {
            FindObjectOfType<StatusPanelBehavour>().ChangeMessage("Code: "+ ServerLobbyCode + "\n Wait for player");
        }
        else
        {
            FindObjectOfType<StatusPanelBehavour>().ChangeMessage("Wait for Player");
        }
    }


    private void Update()
    {
        if (communication.GetConnectionStatusOpen() && messageQueue.Count != 0) {
            Debug.Log("Server send message ");
            string message = messageQueue.Dequeue();

            communication.SendRequest(message);
        }
    }
}
