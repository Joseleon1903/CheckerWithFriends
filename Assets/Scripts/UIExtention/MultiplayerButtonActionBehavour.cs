using Assets.Scripts.WebSocket;
using Assets.Scripts.WebSocket.Message;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerButtonActionBehavour : MonoBehaviour
{

    [Tooltip("Panel for join new private match")]
    [SerializeField] private GameObject panelJoin;

    [Tooltip("Panel for host new private or public match")]
    [SerializeField] private GameObject panelHostGame;

    [Tooltip("Panel see public match")]
    [SerializeField] private GameObject panelPublicGame;

    [Tooltip("Component for client web socket connection")]
    [SerializeField] private GameObject clientPrefab;

    [Tooltip("Component for counter match pending to update")]
    [SerializeField] private GameObject pendingCounter;

    [Tooltip("Component for player room panel")]
    [SerializeField] private GameObject playerRoomPanel;

    private int counterPendingNumber = 0;

    public void InizilizedSingleClient() {

        if (FindObjectOfType<ClientWSBehavour>() == null) {
            Instantiate(clientPrefab);
        }

    }

    private void OnEnable()
    {
        counterPendingNumber = 0;
    }

    public void ShowJoinPrivatePanel() {

        panelJoin.SetActive(true);

        Instantiate(clientPrefab);
    }

    public void ShowHostPanel() {

        Debug.Log("Show host game panel");

        panelHostGame.SetActive(true);

        Debug.Log("create a server and client for host");

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

        ResetPendingMatch();
    }

    public void HidePublicGamePanel() {

        panelPublicGame.SetActive(false);
    }

    public void ShowJoinPrivateGamePanel(PlayerInfo playerOneInfo, PlayerInfo playerTwoInfo) {

        Debug.Log("Show client Join private game panel");

        playerRoomPanel.SetActive(true);

        playerRoomPanel.GetComponent<HostGameRoomBehavior>().SetupJoiningPlayer(playerOneInfo, playerTwoInfo);
    }

    internal void AddPendingMatch()
    {
        counterPendingNumber++;

        pendingCounter.SetActive(true);

        pendingCounter.GetComponentInChildren<Text>().text = counterPendingNumber+"";
    }

    internal void ResetPendingMatch()
    {
        pendingCounter.SetActive(false);
    }

}