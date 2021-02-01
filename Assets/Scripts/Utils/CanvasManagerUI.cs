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
        //Debug.Log("Winner player "+ winner.Type);
        //ProfilePlayerCanvas.SetActive(false);
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

    public void SetSelectedPiece(int pieceOption)
    {

        Debug.Log("option : " + pieceOption);

        switch (pieceOption)
        {

            case (int)PieceOption.Bishop:
                Debug.Log("selected piece Bishop");

                if (isWhite)
                {
                    SpawnPiece(2);

                }
                else
                {
                    SpawnPiece(8);
                }

                break;

            case (int)PieceOption.Knight:
                Debug.Log("selected piece Knight");


                if (isWhite)
                {
                    SpawnPiece(3);
                }
                else
                {
                    SpawnPiece(9);
                }
                break;

            case (int)PieceOption.Queen:
                Debug.Log("selected piece Queen");

                if (isWhite)
                {
                    SpawnPiece(2);
                }
                else
                {
                    SpawnPiece(7);
                }
                break;

            case (int)PieceOption.Rook:
                Debug.Log("selected piece Rook");


                if (isWhite)
                {
                    SpawnPiece(4);
                }
                else
                {
                    SpawnPiece(10);
                }
                break;
        }

    }

    private void SpawnPiece(int index)
    {
        ChessBehaviour c = ChessBoarderManager.Instance.ChessTable[x, y];
        Destroy(c);
        Quaternion quaterionWhite = Quaternion.Euler(0, 90, 0);

        ChessBoarderManager.Instance.SpwanChess(index, x, y, quaterionWhite);
    }

}
