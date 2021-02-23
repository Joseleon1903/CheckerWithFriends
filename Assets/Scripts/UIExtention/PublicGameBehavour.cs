using Assets.Script.WebSocket;
using Assets.Scripts.Network.Service;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PublicGameBehavour: MonoBehaviour
{
    private SortedSet<PublicAvaliableGameObject> PublicGameList = new SortedSet<PublicAvaliableGameObject>();

    [SerializeField] private GameObject publicGameItemPrefab;

    [SerializeField] private GameObject publicGamePanelContent;

    private void Start()
    {
        CheckServerPublic();
    }

    private void OnEnable()
    {
        GameObject[] element = GameObject.FindGameObjectsWithTag("Element");

        foreach (var ele in element) {

            Destroy(ele.gameObject);
        }
        PublicGameList.Clear();
    }

    public void PressRefreshButton() {

        MatchPanelBehavour[] itemList = publicGamePanelContent.GetComponentsInChildren<MatchPanelBehavour>();

        foreach (var item in itemList) {
            Destroy(item.gameObject);
        }

        PublicGameList.Clear();

        CheckServerPublic();


    }

    public IEnumerator RefreashAfterDelay(float delay) {

        yield return new WaitForSeconds(2);

        PressRefreshButton();

    }

    public void RefreshPanelView() {

        Debug.Log("Entering in RefreshPanelView");

        foreach (var item in PublicGameList) {

            var ItemPrefab = Instantiate(publicGameItemPrefab, publicGamePanelContent.transform);

            ItemPrefab.GetComponent<MatchPanelBehavour>().SetupItem(item.lobbyMap, item.lobbyTime, item.lobbyBet,
                item.playerCount.ToString(), item.sessionIdentifier, item.lobbyCode);
        }
    
    }


    /// <summary>
    /// Method which find public game
    /// </summary>
    public void CheckServerPublic()
    {
        SocketConfig config = FindObjectOfType<SocketConfig>();

        var api = RestClientBehavour.Instance.ApiBaseUrl +PublicLobbyService.GetPublicLobbyPath;

        PublicLobbyService serviceLobby = new PublicLobbyService();

        serviceLobby.GetPublicLobbys(api, 10).Then(response => {

            foreach (var item in response)
            {
                PublicGameList.Add(item);
            }

            RefreshPanelView();
        });
    }

}

