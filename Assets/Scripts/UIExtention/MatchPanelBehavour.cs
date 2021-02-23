using Assets.Scripts.WebSocket;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MatchPanelBehavour : MonoBehaviour
{
    public string sessionIdentifier;

    public string lobbyCode;

    [SerializeField] private Image MapImage;

    [SerializeField] private Text MapText;

    [SerializeField] private Text TimeText;

    [SerializeField] private Text LobbyBet;

    [SerializeField] private Text PlayersText;

    [SerializeField] private Button JoinButton;

    public delegate void OnPressJoinPublicGame(string lobbyCode);

    public void  SetupItem(string map, string time,string bet,  string player, string session, string lobbyCode) {

        OnPressJoinPublicGame joinMethod = new OnPressJoinPublicGame(PressJoinPublicGame);

        if (map.ToUpper().Equals("CITY")) 
        {
            Sprite sprite = ResourcesUtil.FindMapSpriteInResource(HostMatchGameBehavour.Map.City);
            MapImage.sprite = sprite;
        }
        else if (map.ToUpper().Equals("PARK")) 
        {
            Sprite sprite = ResourcesUtil.FindMapSpriteInResource(HostMatchGameBehavour.Map.Park);
            MapImage.sprite = sprite;
        }

        LobbyBet.text = bet;

        MapText.text = map;

        TimeText.text = time;

        PlayersText.text = player+"/2";

        sessionIdentifier = session;

        JoinButton.onClick.AddListener(delegate { joinMethod(lobbyCode); });
    }


    public void PressJoinPublicGame(string lobbyCode)
    {
        Debug.Log("Entering method PressJoinPublicGame");
        Debug.Log("Join to lobby : "+ lobbyCode);
        Debug.Log("try lobby connection");

        FindObjectOfType<MultiplayerButtonActionBehavour>().InizilizedSingleClient();

        StartCoroutine(Join(lobbyCode));
    }

    private  IEnumerator Join(string lobbyCode)
    {
        yield return new WaitForSeconds(2.5f);

        var clientWs = FindObjectOfType<ClientWSBehavour>();

        clientWs.profile.isHost = false;

        clientWs.profile.lobbyCode = lobbyCode;

        clientWs.profile.name = FindObjectOfType<GuestProfile>().name;

        if (clientWs != null)
        {
            clientWs.ConnectToLobby(lobbyCode, "Public","PL2");
        }
    }

}
