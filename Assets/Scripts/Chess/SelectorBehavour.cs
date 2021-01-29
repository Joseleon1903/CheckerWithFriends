using Assets.Scripts.Chess;
using Assets.Scripts.General;
using UnityEngine;

public class SelectorBehavour : Singleton<SelectorBehavour>
{

    [SerializeField] private Material selectedMaterial;

    public GameObject selectedChess;

    private Material previusMat;

    void Update()
    {
        //condiction for detect input if player turn onlien mode 
        if (!OnlinePlayerBehavour.Instance.isOnlineGame ||
            (OnlinePlayerBehavour.Instance.isOnlineGame && ChessBoarderManager.Instance.isWhiteTurn && OnlinePlayerBehavour.Instance.playerType == PlayerType.P1) ||
            (OnlinePlayerBehavour.Instance.isOnlineGame && !ChessBoarderManager.Instance.isWhiteTurn && OnlinePlayerBehavour.Instance.playerType == PlayerType.P2))
        { 
        
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("entering Selector click Execution");

                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f, LayerMask.GetMask("CheesPlane")))
                {
                    int x = (int)hit.point.x;
                    int y = (int)hit.point.z;


                    if (ChessBoarderManager.Instance.ChessTable[x, y] != null && ChessBoarderManager.Instance.ChessTable[x, y].isWhite == ChessBoarderManager.Instance.isWhiteTurn) {

                        if (selectedChess != null && selectedChess.GetComponent<ChessBehaviour>().isWhite == ChessBoarderManager.Instance.isWhiteTurn)
                        {
                            RemoveSelectedPiece();
                            BoardHightLight.Intance.HidenHightLight();
                            return;
                        }

                        selectedChess = ChessBoarderManager.Instance.ChessTable[x, y].gameObject;

                        HighlighSelectedPiece(x, y);

                        return;
                    }
                }
                RemoveSelectedPiece();
            }
        
        }

    }

    public void HighlighSelectedPiece(int x , int y)
    {
        previusMat = selectedChess.GetComponent<Renderer>().material;
        selectedChess.GetComponent<Renderer>().material = selectedMaterial;
        BoardHightLight.Intance.HightLightAllowedMoves(ChessBoarderManager.Instance.ChessTable[x, y].PossibleMove());
    }

    public void RemoveSelectedPiece()
    {
        if (selectedChess != null)
        {
            //selectedChess.GetComponent<MeshRenderer>().material = previusMat;
            //BoardHightLight.Intance.HidenHightLight();
            //selectedChess = null;
            //if (!GameManager.Instance.gameState.IsHolding) {
            //    InputManager.Instance.InvokeInputEvent(InputActionType.CANCEL_PIECE);
            //}
        }
    }


}
