using Assets.Scripts.Checkers;
using Assets.Scripts.General;
using Assets.Scripts.Profile;
using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket;
using Assets.Scripts.WebSocket.Message;
using UnityEngine;

public enum PlayerType
{
    P1, P2
}

public class CheckerPlayer : IClicker, IInputReceiver
{
    private BaseProfile playerSession;

    private ClientWSBehavour clientSession;

    private PlayerType type;

    private HitPoint point;

    public PlayerType Type
    {
        get { return type; }
    }

    public CheckerPlayer() {
        LoadProfile();
    }

    public CheckerPlayer(PlayerType typeIn)
    {
        LoadProfile();
        type = typeIn;
    }

    private void LoadProfile() {
        Debug.Log("Load Profile");
        if (Finder.FindGameProfile() != null) {
            playerSession = Finder.FindGameProfile().GetComponent<BaseProfile>();
        }
        clientSession = GameObject.FindObjectOfType<ClientWSBehavour>();
        type = PlayerType.P1;
        if (clientSession != null) {
            type = clientSession.profile.isHost ? PlayerType.P1 : PlayerType.P2;
        }
        Debug.Log("End Load Profile");
    }


    public bool Click(IClickable clickable)
    {
        if (clickable == null) return false;
        return clickable.Inform<CheckerPlayer>(this);
    }

    public void DisableInput()
    {
        InputManager.InputEvent -= OnInputEvent;
    }

    public void EnableInput()
    {
        InputManager.InputEvent += OnInputEvent;
    }
    
    void OnDisable()
    {
        DisableInput();
    }

    public void OnInputEvent(InputActionType action)
    {
        Debug.Log("Entering in OnInputEvent");

        switch (action)
        {

            case InputActionType.GRAB_PIECE:
                Debug.Log("Entering in player grab piece");

                point = Finder.RayHitPointFromScreen(Input.mousePosition);

                if (point.positionX == -1) break;

                CheckersBoard.Instance.SelectedPiece = CheckersBoard.Instance.pieces[point.positionX, point.positionY];

                if (CheckersBoard.Instance.SelectedPiece == null) break;

                if (CheckersBoard.Instance.SelectedPiece != null)
                {
                    int currentX = point.positionX;
                    int currentY = point.positionY;
                    Debug.Log($"Hit Point X={currentX} , Y={currentY}");
                    CheckersBoard.Instance.SelectPiece(currentX, currentY);
                    CheckerGameManager.Instance.GameState.Grab();
                }

                break;

            case InputActionType.CANCEL_PIECE:
                Debug.Log("Entering in player cancel piece");
                if (CheckersBoard.Instance.SelectedPiece != null)
                {
                    //disable hight light piece
                    CheckersBoard.Instance.SelectedPiece.DisableOutline();
                    CheckersBoard.Instance.SelectedPiece = null;
                    HightLightTiled.Instance.HideHightLight();
                }
                CheckerGameManager.Instance.GameState.Cancel();
                break;

            case InputActionType.PLACE_PIECE:
                Debug.Log("Entering in player place piece");

                HitPoint nextPoint = Finder.RayHitPointFromScreen(Input.mousePosition);

                if (CheckersBoard.Instance.SelectedPiece == null || nextPoint.positionX == -1 || nextPoint.positionX == -1) {
                    CheckerGameManager.Instance.GameState.Cancel();
                    CheckersBoard.Instance.SelectedPiece.DisableOutline();
                    CheckersBoard.Instance.SelectedPiece = null;
                    HightLightTiled.Instance.HideHightLight();
                    break;
                }

                if (!CheckersBoard.Instance.SelectedPiece.ValidMove(CheckersBoard.Instance.pieces, point.positionX, point.positionY, nextPoint.positionX, nextPoint.positionY))
                {
                    CheckerGameManager.Instance.GameState.Cancel();
                    CheckersBoard.Instance.SelectedPiece.DisableOutline();
                    CheckersBoard.Instance.SelectedPiece = null;
                    HightLightTiled.Instance.HideHightLight();
                    break;
                }

                bool hasMove = CheckersBoard.Instance.TryMove(point.positionX, point.positionY,
                nextPoint.positionX, nextPoint.positionY);

                if (hasMove) {

                    CheckersBoard.Instance.CheckVictory();

                    CheckersBoard.Instance.SendMovementMessage(point, nextPoint);

                    CheckerGameManager.Instance.SwitchPlayer();

                }

                break;

        }
    }
}
