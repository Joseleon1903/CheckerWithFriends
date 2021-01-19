using Assets.Scripts.WebSocket;
using Assets.Scripts.WebSocket.Message;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckerEndGameBehavour : MonoBehaviour
{
    [SerializeField] private Text timertext;

    [SerializeField] private Button playAgainButton;

    private int remachPressCounter;

    private void OnEnable()
    {
        remachPressCounter = 0;

    }

    private void Start()
    {
        string time = FindObjectOfType<TimerBehavour>().GetGameTime();
        timertext.text = "Time: "+ time;
    }

    public void MainMenuButton() {
        SceneManager.LoadScene("MainMenu");
    }


    public void PlayAgainButton()
    {
        Debug.Log("Play Again Button Pressed");

        remachPressCounter++;

        ReciveRematchRequest(remachPressCounter);

        //if (remachPressCounter <= 1) {

        string lobbyCode = FindObjectOfType<ClientWSBehavour>().profile.lobbyCode;

        RematchGameMessageReq rematchM = new RematchGameMessageReq(lobbyCode, remachPressCounter);

        FindObjectOfType<ClientWSBehavour>().Send(rematchM.GetMessageText());
        //}
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

    public void ShareButton() {
        Debug.Log("Share Button Pressed");

    }

    private IEnumerator Rematch(float delay)
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("Start a rematch");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
