using Assets.Script.WebSocket;
using Assets.Scripts.WebSocket.Message;
using System;
using UnityEngine;

public class ServerCommunication : MonoBehaviour
{
    // Final server address
    private SocketConfig serverConfig;

    private string server;

    // WebSocket Client 
    private WsClient client;

    /// <summary>
    /// Unity method called on initialization
    /// </summary>
    private void Start()
    {
        serverConfig = FindObjectOfType<SocketConfig>();
        if (serverConfig == null) { throw new Exception("Not found Socket Configuration"); }

        server = serverConfig.GetSocketHost();
        client = new WsClient(server);
    }

    /// <summary>
    /// Unity method called every frame
    /// </summary>
    private void Update()
    {
        // Check if server send new messages
        var cqueue = client.receiveQueue;
        string msg;
        while (cqueue.TryPeek(out msg))
        {
            // Parse newly received messages
            cqueue.TryDequeue(out msg);
            HandleMessage(msg);
        }



    }

    /// <summary>
    /// Method responsible for handling server messages
    /// </summary>
    /// <param name="msg">Message.</param>
    private void HandleMessage(string msg)
    {
        Debug.Log("Server: " + msg);

        CreateLobbyMessageResp response = new CreateLobbyMessageResp(msg);

        if ("OK".Equals(response.result)) {

            Debug.Log("Lobby success created");

            //FindObjectOfType<MultiplayerManager>().CreateLocalClient();

        }

    }

    /// <summary>
    /// Call this method to get the status of connection 
    /// true is conncetion open 
    /// false is conection not open
    /// </summary>
    public bool GetConnectionStatusOpen() {
        return client.IsConnectionOpen();
    }    

    /// <summary>
    /// Call this method to connect to the server
    /// </summary>
    public async void ConnectToServer()
    {
        await client.Connect();
    }

    /// <summary>
    /// Method which sends data through websocket
    /// </summary>
    /// <param name="message">Message.</param>
    public void SendRequest(string message)
    {
        client.Send(message);
    }

    public async void DisconectToServer()
    {
        await client.Disconect();
    }
}
