using Assets.Scripts.WebSocket;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerManager : MonoBehaviour
{
    public static MultiplayerManager Instance;

    public string clientName;

    public GameObject mainMenu;
    public GameObject serverMenu;
    public GameObject connectMenu;

    public GameObject serverPrefab;
    public GameObject clientPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        //DontDestroyOnLoad(gameObject);

        serverMenu.SetActive(false);
        connectMenu.SetActive(false);
        
    }

    public void ConnectButton() 
    {
        Debug.Log("Connect");

        clientName = GameObject.Find("PlayerNameInput").GetComponent<InputField>().text;

        if (clientName == "")
        {
            Debug.Log("Player name is mandatory");
            return;
        }

        Instantiate(clientPrefab);

        mainMenu.SetActive(false);
        connectMenu.SetActive(true);
    
    }

    public void HostButton() 
    {
        Debug.Log("Host");
        clientName = GameObject.Find("PlayerNameInput").GetComponent<InputField>().text;

        if (clientName == "")
        {
            Debug.Log("Player name is mandatory");
            return;
        }

        try
        {
            Debug.Log("create a server");
            Instantiate(serverPrefab);

            Instantiate(clientPrefab);

        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

        mainMenu.SetActive(false);
        serverMenu.SetActive(true);

    }

    public void ConnectToServerButton()
    {
        Debug.Log("entering ConnectToServerButton");

        string lobbyCode = GameObject.Find("LobbyCodeInputField").GetComponent<InputField>().text;

        if (lobbyCode.Equals("")) {
            Debug.Log("lobby code is mandatory");
            return;
        }

        var client = FindObjectOfType<ClientWSBehavour>();
        client.profile.lobbyCode = lobbyCode;
        //client.ConnectToLobby(lobbyCode, "");
    }

    public void CreateLocalClient() {

        ClientWSBehavour client = FindObjectOfType<ClientWSBehavour>();
        client.profile.name = clientName;
        string lobbyCode = FindObjectOfType<ServerBehavour>().ServerLobbyCode;
        client.profile.lobbyCode = lobbyCode;
        //client.ConnectToLobby(lobbyCode, "");
    }


    public void BackButton() {

        mainMenu.SetActive(true);
        serverMenu.SetActive(false);
        connectMenu.SetActive(false);

        ServerBehavour s = FindObjectOfType<ServerBehavour>();
        if (s != null)
            Destroy(s.gameObject);

        ClientWSBehavour c = FindObjectOfType<ClientWSBehavour>();
        if (c != null)
            Destroy(s.gameObject);

    }

    public void StartGame() {

        SceneManager.LoadScene("CheckerGameScene");
    }

    public void WriteLobbyCode(string code) {
        //write lobby code 
        var labelLobbycode = GameObject.Find("LobbyPlayerLabel").GetComponent<Text>();
        labelLobbycode.text = "Waiting for Another player lobby: " + code;
    }

}
