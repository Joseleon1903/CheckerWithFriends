using Assets.Scripts.General;
using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket;
using UnityEngine;

namespace Assets.Scripts.Checkers
{
    public enum PlayerType
    {
        P1, P2
    }

    public class CheckerPlayer : IClicker, IInputReceiver
    {
        private ClientWSBehavour clientSession;

        private PlayerType type;

        private HitPoint point;

        public PlayerType Type
        {
            get { return type; }
        }

        public CheckerPlayer()
        {
            LoadProfile();
        }

        public CheckerPlayer(PlayerType typeIn)
        {
            LoadProfile();
            type = typeIn;
        }

        private void LoadProfile()
        {
            clientSession = GameObject.FindObjectOfType<ClientWSBehavour>();
            type = PlayerType.P1;
            if (clientSession != null)
            {
                type = clientSession.profile.isHost ? PlayerType.P1 : PlayerType.P2;
            }
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

                    if (CheckersBoard.Instance.SelectedPiece == null || nextPoint.positionX == -1 || nextPoint.positionX == -1)
                    {
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

                    CheckersBoard.Instance.SendMovementMessage(point, nextPoint, hasMove);

                    if (hasMove)
                    {

                        CheckersBoard.Instance.CheckVictory();

                        CheckerGameManager.Instance.SwitchPlayer();

                    }
                    break;
            }
        }
    }
}