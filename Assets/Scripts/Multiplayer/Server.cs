using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class Server : MonoBehaviour
{
    public int port = 6321;

    private List<ServerClient> clients;
    private List<ServerClient> disconectList;

    private TcpListener server;

    private bool serverStartd;

    public void Init() {

        DontDestroyOnLoad(gameObject);

        clients = new List<ServerClient>();

        disconectList = new List<ServerClient>();

        try 
        {

            server = new TcpListener(IPAddress.Any, port);
            server.Start();

            StartListeningConnection();

        }
        catch(Exception ex)
        {

            Debug.Log("Socket error: "+ ex.Message);
        
        }

    }

    private void Update()
    {
        if (!serverStartd)
            return;

        foreach (ServerClient c in clients ) {

            //is the client still connected?
            if (!IsConnected(c.tcp))
            {
                c.tcp.Close();
                disconectList.Add(c);
                continue;
            }
            else 
            {
                NetworkStream s = c.tcp.GetStream();
                if (s.DataAvailable) 
                {
                    StreamReader reader = new StreamReader(s, true);
                    string data = reader.ReadLine();

                    if (data != null)
                        OnIncomingData(c, data);
                }
            }
        }

        for (int i =0; i< disconectList.Count -1; i++) 
        {
            //tell all player somebody has disconected

            clients.Remove(disconectList[i]);
            disconectList.RemoveAt(i);
        }

    }

    private void StartListeningConnection() {

        server.BeginAcceptTcpClient(AcceptTcpClient, server);
    }

    private bool IsConnected(TcpClient c) {

        try
        {
            if (c != null && c.Client != null && c.Client.Connected)
            {
                if (c.Client.Poll(0, SelectMode.SelectRead))
                    return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);

                return true;
            }
            else 
            { 
                return false; 
            }

        }
        catch {
            return false;
        }
    }

    private void AcceptTcpClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;
        string allUsers = "";

        foreach (ServerClient client in clients)
        {

            allUsers += client.clientName + '|';
        }

        ServerClient sc = new ServerClient(listener.EndAcceptTcpClient(ar));

        clients.Add(sc);

        StartListeningConnection();

        Broadcast("SWHO|"+allUsers, clients[clients.Count-1]);

        Debug.Log("Somebody has connected!");
    }

    //Server send
    private void Broadcast(string data, List<ServerClient> cl) {

        foreach (ServerClient sc in cl) {

            try 
            {
                StreamWriter writer = new StreamWriter(sc.tcp.GetStream());
                writer.WriteLine(data);
                writer.Flush();
            
            } 
            catch (Exception ex) 
            {
                Debug.Log("Write error: " + ex.Message);
            
            }
        }
    }

    //Server send
    private void Broadcast(string data, ServerClient c)
    {
        List<ServerClient> sc = new List<ServerClient> { c };
        Broadcast(data, sc);
    }

    //Server read
    private void OnIncomingData(ServerClient sc , string data) {
        Debug.Log(sc.clientName + " : "+ data);

        string[] aData = data.Split('|');

        switch (aData[0])
        {
            case "CWHO":
                sc.clientName = aData[1];
                sc.isHost = (aData[2] == "0") ? false : true;
                Broadcast("SCNN|" + sc.clientName, clients);
                break;

            case "CMOV":
                
                Broadcast("SMOV|"+aData[1] + "|" + aData[2] + "|" + aData[3] + "|" + aData[4] + "|", clients);

                break;
        }
    }
}

public class ServerClient {

    public string clientName;

    public bool isHost;

    public TcpClient tcp;

    public ServerClient(TcpClient tcp) {
        this.tcp = tcp;
    }
}



