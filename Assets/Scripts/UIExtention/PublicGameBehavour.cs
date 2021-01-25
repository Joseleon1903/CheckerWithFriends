using Assets.Script.WebSocket;
using Assets.Scripts.Utils;
using Proyecto26;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PublicGameBehavour: MonoBehaviour
{
    private SortedSet<PublicAvaliableGameObject> PublicGameList = new SortedSet<PublicAvaliableGameObject>();

    [SerializeField] private GameObject publicGameItemPrefab;

    [SerializeField] private GameObject publicGameEmptyPrefab;

    [SerializeField] private GameObject publicGamePanelContent;

    private void Start()
    {
        CheckServerPublic();
    }

    private void OnEnable()
    {
        Debug.Log("On Enable Public game panel");

        GameObject[] element = GameObject.FindGameObjectsWithTag("Element");

        foreach (var ele in element) {

            Destroy(ele.gameObject);
        }
        PublicGameList.Clear();

    }

    public void PressRefreshButton() {

        Debug.Log("Press reflesh Button");

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

        LoggerFile.Instance.DEBUG_LINE("Entering in RefreshPanelView");
        Debug.Log("Entering in RefreshPanelView");

        Component element = publicGamePanelContent.FindComponentInChildWithTag<Component>("Element");
        if (PublicGameList.Count == 0 && element == null) {

            Debug.Log("Public Game is empty");

            Instantiate(publicGameEmptyPrefab, publicGamePanelContent.transform);
        }

        LoggerFile.Instance.DEBUG_LINE("Process Public game list");

        foreach (var item in PublicGameList) {

            Debug.Log("Process id : " + item.id);
            LoggerFile.Instance.DEBUG_LINE("Process Game id: " + item.id);

            var ItemPrefab = Instantiate(publicGameItemPrefab, publicGamePanelContent.transform);

            ItemPrefab.GetComponent<MatchPanelBehavour>().SetupItem(item.lobbyMap, item.lobbyTime,
                item.playerCount.ToString(), item.sessionIdentifier, item.lobbyCode);
        }
    
    }


    /// <summary>
    /// Method which find public game
    /// </summary>
    public void CheckServerPublic()
    {
        Debug.Log("Entering method CheckServerPublic");

        SocketConfig config = FindObjectOfType<SocketConfig>();

        //var api = RestClientImpl.GetPublicMatchRestPath(config.LobbyType, 10);

        //OnRequestError errorHandler = new OnRequestError(HandlerRequestError);

        //RestClient.GetArray<PublicAvaliableGameObject>(api ).Then(reponse => {

        //    Debug.Log("Response list size:"+reponse.Length);

        //    foreach (var item in reponse) {
        //        PublicGameList.Add(item);
        //    }

        //    RefreshPanelView();

        //}).Catch(err => errorHandler(err));
    }

    public delegate void OnRequestError(Exception var);


    public void HandlerRequestError(Exception response) {
        Debug.Log("Error" + response.Message + "Ok");

        RefreshPanelView();
    }

}

