using Assets.Scripts.Profile;
using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket;
using Assets.Scripts.WebSocket.Message;
using UnityEngine;
using UnityEngine.UI;

public class HostGameRoomBehavior : MonoBehaviour
{

    [SerializeField] private GameObject playerOneFrame;

    [SerializeField] private GameObject playerOneAvatar;

    [SerializeField] private Text playerOneName;

    [SerializeField] private Text playerOneId;

    [SerializeField] private Text playerOneNationality;

    [SerializeField] private Text lobbyDescription;

    [SerializeField] private GameObject playerTwoPanel;

    [SerializeField] private GameObject playerLoadingPanel;

    [SerializeField] private GameObject playerTwoFrame;

    [SerializeField] private GameObject playerTwoAvatar;

    [SerializeField] private Text playerTwoName;

    [SerializeField] private Text playerTwoId;

    [SerializeField] private Text playerTwoNationality;

    private void OnEnable()
    {

        string serverLobbyCode = PlayerPrefs.GetString(PlayerPreferenceKey.PLAYER_LOBBY_CODE);
        string type = PlayerPrefs.GetString(PlayerPreferenceKey.PLAYER_LOBBY_TYPE);

        playerTwoPanel.SetActive(false);

        var clientWS = FindObjectOfType<ClientWSBehavour>();

        clientWS.profile.isHost = true;

        clientWS.profile.lobbyCode = serverLobbyCode;

        clientWS.profile.name = FindObjectOfType<GuestProfile>().name;

        clientWS.ConnectToLobby(serverLobbyCode, type, "PL1");

        string map = PlayerPrefs.GetString(PlayerPreferenceKey.PLAYER_LOBBY_MAP);
        string time = PlayerPrefs.GetString(PlayerPreferenceKey.PLAYER_LOBBY_TIME);
        string bet = PlayerPrefs.GetString(PlayerPreferenceKey.PLAYER_LOBBY_BET);

        string description = $"Game Map: {map} , Game Time: {time} , lobby type: {type} , Bet: {bet}";

        lobbyDescription.text = description;

        BaseProfile profile = Finder.FindGameProfile().GetComponent<BaseProfile>();

        string hostFrame = PlayerPrefs.GetString(PlayerPreferenceKey.PROFILE_ONE_KEY_FRAME);
        string hostAvater = PlayerPrefs.GetString(PlayerPreferenceKey.PROFILE_ONE_KEY_AVATAR); ;
        string hostName = profile._nameProfile;
        string hostId = (profile._isGuest) ? profile._guestUserId : profile._facebookUserId;
        string hostNationality = ProfileUtil.GetNationalityName(profile._nationality); 
        ProfileUtil.SetupProfileImageFromResources(hostAvater, hostFrame, playerOneAvatar, playerOneFrame);
        playerOneName.text = hostName;
        playerOneId.text = hostId;
        playerOneNationality.text = hostNationality;

    }

    public void AddOpponentPlayer(PlayerInfo playerTwoInfo) {

        Debug.Log("Entering in method AddOpponentPlayer");
        playerTwoPanel.SetActive(true);
        playerLoadingPanel.SetActive(false);

        string[] sprite = playerTwoInfo.picture.Split('%');
        string frameRoot = sprite[0];
        string avatarRoot = sprite[1];

        ProfileUtil.SetupProfileImageFromResources(avatarRoot, frameRoot, playerTwoFrame, playerTwoAvatar);

        playerTwoName.text = playerTwoInfo.name;
        playerTwoId.text = playerTwoInfo.playerId;
        playerTwoNationality.text = ProfileUtil.GetNationalityName(playerTwoInfo.nationality);
    }

    public void CloseButton() {

        Debug.Log("Press cancel game room");

        gameObject.SetActive(false);

        var client = GameObject.FindGameObjectWithTag("ClientWS");

        Destroy(client);

        var server = GameObject.FindGameObjectWithTag("ServerWS");

        Destroy(server);
    }

}
