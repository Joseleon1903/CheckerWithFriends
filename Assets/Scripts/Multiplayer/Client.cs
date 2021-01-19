using UnityEngine;
using System.Net.Sockets;
using System.IO;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Multiplayer
{
    public class Client : MonoBehaviour
    {
        public GameClient profile;
        public bool isHost = false;

        private List<GameClient> players = new List<GameClient>();

        private bool socketReady;
        private TcpClient socket;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public bool ConnectToServer(string host , int port) {

            if (socketReady)
                return false; ;

            try 
            {
                socket = new TcpClient(host, port);
                stream = socket.GetStream();
                writer = new StreamWriter(stream);
                reader = new StreamReader(stream);

                socketReady = true;

            } 
            catch (Exception ex) 
            {
                Debug.Log("Socket error: " + ex.Message);
            }

            return socketReady;
        }

        private void Update()
        {
            if (socketReady) {

                if (stream.DataAvailable) 
                {
                    string data = reader.ReadLine();
                    if (data != null) 
                    {
                        OnIncomingData(data);
                    }
                
                }
            }
        }

        //read message from the server
        private void OnIncomingData(string data) {
            Debug.Log(data);

            string[] aData = data.Split('|');

            switch (aData[0]) {

                case "SWHO":

                    for (int i =1; i < aData.Length-1; i++) {
                        UserConnected(aData[i], false);
                    }

                    Send("CWHO|"+ profile.name + "|"+ ((isHost)? 1:0).ToString());
                    break;

                case "SCNN":

                    UserConnected(aData[1], false);

                    break;

                case "SMOV":

                    CheckersBoard.Instance.TryMove(int.Parse(aData[1]), int.Parse(aData[2]),
                        int.Parse(aData[3]), int.Parse(aData[4]));

                    break;

            }


        }

        //sending message to the server
        public void Send(string data) {

            if (!socketReady)
                return;

            writer.Write(data);
            writer.Flush();
        }

        private void UserConnected(string name, bool isHost) {
            GameClient c = new GameClient();
            c.name = name;
            c.isHost = isHost;

            players.Add(c);

            if (players.Count == 2)
                MultiplayerManager.Instance.StartGame();
        }

        private void CloseScoket() {
            if (!socketReady)
                return;

            writer.Close();
            reader.Close();
            socket.Close();
            socketReady = false;
        }

        private void OnApplicationQuit()
        {
            CloseScoket();
        }

        private void OnDisable()
        {
            CloseScoket();
        }

    }

    public class GameClient {

        public string name;
        public bool isHost;
        public string lobbyCode;
    
    }

}