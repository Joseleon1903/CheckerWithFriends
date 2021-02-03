using Assets.Scripts.General;
using UnityEngine;

public class CanvasManagerUI : Singleton<CanvasManagerUI>
{
    private int x, y;

    private bool isWhite;
    public enum PieceOption
    {
        Bishop = 0,
        Queen = 1,
        Rook = 2,
        Knight = 3
    }

    [SerializeField] private GameObject AlertCanvas;

    [SerializeField] private GameObject playerControlCanvas;

    [SerializeField] private GameObject ProfilePlayerCanvas;

    [SerializeField] private GameObject EndGameCanvas;

    [SerializeField] private GameObject OptionCanvas;

    private void Start()
    {
        playerControlCanvas.SetActive(false);
        ProfilePlayerCanvas.SetActive(false);
        EndGameCanvas.SetActive(false);
    }

    public void StartGameCanvasView() {

        Debug.Log("Initialize animation Player layout");

        playerControlCanvas.SetActive(true);

        ProfilePlayerCanvas.SetActive(true);

    }


    public void ShowAlertText(string text) {
        AlertCanvas.SetActive(true);
        AlertCanvas.GetComponent<AlertBehavour>().ShowAlert(text);
    }

    public void ShowGameOverCanvas()
    {
        EndGameCanvas.SetActive(true);
    }

    public void ShowGameOptionMenu() {
        Debug.Log("Entering in method ShowGameOptionMenu");
        OptionCanvas.SetActive(true);
    }

    public void SetPiecePosition(bool white, int Posx, int Posy)
    {
        isWhite = white;
        x = Posx;
        y = Posy;
    }
}
