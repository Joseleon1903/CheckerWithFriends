using Assets.Scripts.Checkers;
using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket;
using Assets.Scripts.WebSocket.Message;
using System.Collections;
using Unity.Pandora.Core.Mobile.Animation.Tween;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckerEndGameBehavour : MonoBehaviour
{

    [SerializeField] private Button playAgainButton;

    [SerializeField] private GameObject panelGameOver;

    [SerializeField] private Text gameTimeLabel;

    [Header("Player One Attribute")]
    [SerializeField] private Text PlayerOneName;

    [SerializeField] private GameObject PlayerOneFrame;

    [SerializeField] private GameObject PlayerOneAvatar;

    [SerializeField] private GameObject PlayerOnePanel;

    [SerializeField] private Text PlayerOneStatus;

    [SerializeField] private Text PlayerOneWonCounter;

    [SerializeField] private Text PlayerOneCoinCounter;

    [Header("Player Two Attribute")]
    [SerializeField] private Text PlayerTwoName;

    [SerializeField] private GameObject PlayerTwoFrame;

    [SerializeField] private GameObject PlayerTwoAvatar;

    [SerializeField] private GameObject PlayerTwoPanel;

    [SerializeField] private Text PlayerTwoStatus;

    [SerializeField] private Text PlayerTwoWonCounter;

    [SerializeField] private Text PlayerTwoCoinCounter;


    private int remachPressCounter;

    private void OnEnable()
    {

        CoolDownUtil.ResetCoolDownPlayer(PlayerType.P1);

        CoolDownUtil.ResetCoolDownPlayer(PlayerType.P2);

        remachPressCounter = 0;

        SendGameOverVictoryMessage();

        LeanTween.scale(panelGameOver, Vector3.one, 1.8f).setEase(LeanTweenType.easeOutElastic);

        SetUpProfilePlayer();
    }

    public void ShowCanvas() {
        Debug.Log("Show Canvas method execute");
        gameObject.SetActive(true);
    }

    private void Start()
    {
    }

    public void SetUpProfilePlayer() 
    {
        string time = FindObjectOfType<TimerBehavour>().GetGameTime();
        gameTimeLabel.text = $"Game Time: {time}";

        //setup attribute player one
        Debug.Log("Player One set attribute");
        string profileName = PlayerPrefs.GetString(PlayerPreferenceKey.PROFILE_ONE_KEY_NAME);
        string profileFrame = PlayerPrefs.GetString(PlayerPreferenceKey.PROFILE_ONE_KEY_FRAME);
        string profileAvatar = PlayerPrefs.GetString(PlayerPreferenceKey.PROFILE_ONE_KEY_AVATAR);
        string profilePlayerWont = PlayerPrefs.GetString(PlayerPreferenceKey.PROFILE_ONE_KEY_COUNTER_WON);
        string profilePlayerCoin = PlayerPrefs.GetString(PlayerPreferenceKey.PROFILE_ONE_KEY_COUNTER_COIN);

        PlayerOneWonCounter.text  = profilePlayerWont;
        PlayerOneCoinCounter.text = profilePlayerCoin;
        PlayerOneName.text = profileName;
        ProfileUtil.SetupProfileImageFromResources(profileAvatar, profileFrame, PlayerOneAvatar, PlayerOneFrame);

        Debug.Log("Player Two set attribute");
        profileName = PlayerPrefs.GetString(PlayerPreferenceKey.PROFILE_TWO_KEY_NAME);
        profileFrame = PlayerPrefs.GetString(PlayerPreferenceKey.PROFILE_TWO_KEY_FRAME);
        profileAvatar = PlayerPrefs.GetString(PlayerPreferenceKey.PROFILE_TWO_KEY_AVATAR);
        profilePlayerWont = PlayerPrefs.GetString(PlayerPreferenceKey.PROFILE_TWO_KEY_COUNTER_WON);
        profilePlayerCoin = PlayerPrefs.GetString(PlayerPreferenceKey.PROFILE_TWO_KEY_COUNTER_COIN);

        PlayerTwoWonCounter.text = profilePlayerWont;
        PlayerTwoCoinCounter.text = profilePlayerCoin;
        PlayerTwoName.text = profileName;
        ProfileUtil.SetupProfileImageFromResources(profileAvatar, profileFrame, PlayerTwoAvatar, PlayerTwoFrame);

        if (CheckerGameManager.Instance.GameState.PlayerWin.Equals(PlayerType.P1))
        {
            PlayerOneStatus.text = "Winner";
            PlayerTwoStatus.text = "Loser";

            PlayerOnePanel.GetComponent<Image>().color = ResourcesUtil.FindMaterialInResource(ResourcesUtil.MATERIAL_COLOR_GOLD).color;

            PlayerTwoPanel.GetComponent<Image>().color = ResourcesUtil.FindMaterialInResource(ResourcesUtil.MATERIAL_COLOR_RED).color;
        }
        else if (CheckerGameManager.Instance.GameState.PlayerWin.Equals(PlayerType.P2))
        {
            PlayerOneStatus.text = "Loser";
            PlayerTwoStatus.text = "Winner";

            PlayerOnePanel.GetComponent<Image>().color = ResourcesUtil.FindMaterialInResource(ResourcesUtil.MATERIAL_COLOR_RED).color;

            PlayerTwoPanel.GetComponent<Image>().color = ResourcesUtil.FindMaterialInResource(ResourcesUtil.MATERIAL_COLOR_GOLD).color;
        }
    }

    public void MainMenuButton() {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayAgainButton()
    {
        Debug.Log("Play Again Button Pressed");

        remachPressCounter++;

        ReciveRematchRequest(remachPressCounter);

        string lobbyCode = FindObjectOfType<ClientWSBehavour>().profile.lobbyCode;

        RematchGameMessageReq rematchM = new RematchGameMessageReq(lobbyCode, remachPressCounter);

        FindObjectOfType<ClientWSBehavour>().Send(rematchM.GetMessageText());

        playAgainButton.interactable = false;
    }

    public void ReciveRematchRequest(int Rematchcounter) {

        remachPressCounter = Rematchcounter;
        if (Rematchcounter == 1) {

            Debug.Log("One client want rematch");

            string rematchText = $"Play Again ({Rematchcounter})";

            playAgainButton.GetComponentInChildren<Text>().text = rematchText;

            CanvasManagerUI.Instance.ShowAlertText("Oponent want a rematch");

        } else if (Rematchcounter == 2) {

            Debug.Log("Two client want rematch");

            string rematchText = $"Play Again ({remachPressCounter})";

            playAgainButton.GetComponentInChildren<Text>().text = rematchText;

            CanvasManagerUI.Instance.ShowAlertText("Rematch was Acepted Start new Game");

            StartCoroutine(Rematch(3.0f));

        }

    }

    private IEnumerator Rematch(float delay)
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("Start a rematch");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShareButton()
    {
        Debug.Log("Share Button Pressed");

    }

    private void SendGameOverVictoryMessage() {
        Debug.Log("Send message Victory Player");

        ClientWSBehavour client = FindObjectOfType<ClientWSBehavour>();

        if (client != null && CheckerGameManager.Instance.GameState.PlayerWin != CheckerGameManager.Instance.Player.Type)
        {

            string lobbyCode = client.profile.lobbyCode;
            string gameType = GameType.CHECKER.ToString().ToUpper();
            string playerWin = CheckerGameManager.Instance.GameState.PlayerWin.ToString().ToUpper();
            string gameoverType = CheckerGameManager.Instance.GameState.GameOverType.ToString().ToUpper();
            VictoryGameMessageReq victoryReq = new VictoryGameMessageReq(lobbyCode, gameType, gameoverType, playerWin);
            client.Send(victoryReq.GetMessageText());
        }
        else if(CheckerGameManager.Instance.GameState.IsGameOver && CheckerGameManager.Instance.GameState.GameOverType.Equals(GameOverType.CHECKMATE) && CheckerGameManager.Instance.GameState.PlayerWin == CheckerGameManager.Instance.Player.Type)
        {
            string lobbyCode = client.profile.lobbyCode;
            string gameType = GameType.CHECKER.ToString().ToUpper();
            string playerWin = CheckerGameManager.Instance.GameState.PlayerWin.ToString().ToUpper();
            string gameoverType = CheckerGameManager.Instance.GameState.GameOverType.ToString().ToUpper();
            VictoryGameMessageReq victoryReq = new VictoryGameMessageReq(lobbyCode, gameType, gameoverType, playerWin);
            client.Send(victoryReq.GetMessageText());
        }
    }
}
