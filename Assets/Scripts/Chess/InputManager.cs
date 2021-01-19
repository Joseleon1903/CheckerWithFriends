using Assets.Scripts.General;
using UnityEngine;

namespace Assets.Scripts.Chess
{
	public enum InputActionType
	{
		GRAB_PIECE = 0,
		PLACE_PIECE = 1,
		CANCEL_PIECE = 2,
		ZOOM_IN = 3,
		ZOOM_OUT = 4,
		ROTATE = 5,
		STOP_ROTATE = 6,
		ONLINE_OPPONENT_PLACE = 7
	}

	public class InputManager : Singleton<InputManager>
	{

		public delegate void InputEventHandler(InputActionType actionType);
		public static event InputEventHandler InputEvent;

		private bool clicked;
		private Node currentNode;
		private GCPlayer currentPlayer;

		public Vector2 mouseAxis;

		public Vector2 MouseAxis
		{
			get { return mouseAxis; }
		}

		void Awake()
		{
			_destroyOnLoad = destroyOnLoad;
			mouseAxis = new Vector2(0, 0);
		}

		void OnDisable()
		{
			InputEvent = null;
		}

		public void InvokeInputEvent(InputActionType actionType)
		{
			InputEvent(actionType);
		}

		public void InvokeInputEvent()
		{

			Debug.Log("Entering method InvokeInputEvent do ONLINE_OPPONENT_PLACE ");

			OnlinePlayerMovement playerM = OnlinePlayerBehavour.Instance.PlayerMovement;
			Debug.Log("Online player movement: " + playerM.ToString());

			// code for node movement game manager
			if (OnlinePlayerBehavour.Instance.playerType == PlayerType.P1) {

				Node cNode = BoardGrid.Instance.GetNodeAt(playerM.nodePosY, playerM.nodePosX);

				Node nNode = BoardGrid.Instance.GetNodeAt(playerM.nodeNextPosY, playerM.nodeNextPosX);

				Piece cPiece = cNode.Piece;

				cPiece.Compute();

				GCPlayer player = GameManager.Instance.CurrentPlayer;

				if (cPiece.IsPossibleMove(nNode))
				{
					if (Rules.IsCheckMove(player, cPiece, nNode, true))
					{
						Debug.Log("Move checked"); // do nothing


					}
					else
					{
						cPiece.MoveToXZ(nNode);
						cPiece.Drop();
						cPiece.Compute();
					}
				}
				else
				{
					if (cPiece.IsPossibleEat(nNode))
					{
						if (Rules.IsCheckEat(player, cPiece, nNode, true))
						{
							Debug.Log("Eat checked"); // do nothing
						}
						else
						{
							GCPlayer oppPlayer = GameManager.Instance.Opponent(player);
							oppPlayer.RemovePiece(cPiece);
							player.AddEatenPieces(cPiece);
							cPiece.MoveToXZ(nNode);
							cPiece.Drop();
							cPiece.Compute();
						}
					}
				}

				GameManager.Instance.CurrentPlayer.ComputePieces();
				GameManager.Instance.PlayerOponent.ComputePieces();

				GameManager.Instance.SwitchPlayer();
			}

			ChessBoardMovementeApply(playerM, false);

			if (OnlinePlayerBehavour.Instance.playerType == PlayerType.P2) {
				CanvasManagerUI.Instance.ShowAlertText("It's player P2 turn");
			}

			Debug.Log("Exiting method InvokeInputEvent done ONLINE_OPPONENT_PLACE ");

		}

		public void ChessBoardMovementeApply(OnlinePlayerMovement playerM,bool switchPlayer) {

			//code for chess board movement game manager  
			ChessBehaviour selectedChess = ChessBoarderManager.Instance.ChessTable[playerM.nodePosX, playerM.nodePosY];

			//validate if next movement position is ocupated by opponent piece
			if (ChessBoarderManager.Instance.ChessTable[playerM.nodeNextPosX, playerM.nodeNextPosY] != null)
			{
				//Capture a piece
				Debug.Log("Capture a piece");

				ChessBehaviour capturedPiece = ChessBoarderManager.Instance.ChessTable[playerM.nodeNextPosX, playerM.nodeNextPosY];

				if (OnlinePlayerBehavour.Instance.playerType.Equals(PlayerType.P2))
					CapturedBoardWhite.Instance.AddCaturePiece(capturedPiece.gameObject);
				else
					CapturedBoardBlack.Instance.AddCaturePiece(capturedPiece.gameObject);

				//destroy chess 
				ChessBoarderManager.Instance.activeChees.Remove(capturedPiece.gameObject);
				Destroy(capturedPiece.gameObject);
			}

			ChessBoarderManager.Instance.ChessTable[playerM.nodePosX, playerM.nodePosY] = null;
			selectedChess.transform.position = TilesUtils.GetTileCenter(playerM.nodeNextPosX, playerM.nodeNextPosY);
			selectedChess.SetPosition(playerM.nodeNextPosX, playerM.nodeNextPosY);
			ChessBoarderManager.Instance.ChessTable[playerM.nodeNextPosX, playerM.nodeNextPosY] = selectedChess;

			if (switchPlayer) {
				GameManager.Instance.SwitchPlayer();
			}

		}

		void Update()
		{

			if (!OnlinePlayerBehavour.Instance.isOnlineGame ||
		   (OnlinePlayerBehavour.Instance.isOnlineGame && ChessBoarderManager.Instance.isWhiteTurn && OnlinePlayerBehavour.Instance.playerType == PlayerType.P1) ||
		   (OnlinePlayerBehavour.Instance.isOnlineGame && !ChessBoarderManager.Instance.isWhiteTurn && OnlinePlayerBehavour.Instance.playerType == PlayerType.P2))
			{
				mouseAxis.x = Input.GetAxis("Mouse X");
				mouseAxis.y = Input.GetAxis("Mouse Y");

				if (InputEvent == null) return;

				if (Input.GetMouseButtonUp(0))
				{
					Debug.Log(GameManager.Instance.GameState.State.ToString());

					if (GameManager.Instance.GameState.IsWaiting)
					{
						InputEvent(InputActionType.GRAB_PIECE);
					}
					else if (GameManager.Instance.GameState.IsHolding)
					{
						InputEvent(InputActionType.PLACE_PIECE);
					}
				}

				if (Input.GetMouseButtonUp(1))
				{
					if (GameManager.Instance.GameState.IsHolding)
					{
						InputEvent(InputActionType.CANCEL_PIECE);
					}
				}


				//camera controll 
				if (Input.GetAxis("Mouse ScrollWheel") > 0)
				{
					InputEvent(InputActionType.ZOOM_IN);
				}

				if (Input.GetAxis("Mouse ScrollWheel") < 0)
				{
					InputEvent(InputActionType.ZOOM_OUT);
				}

				if (Input.GetMouseButtonDown(2))
				{
					InputEvent(InputActionType.ROTATE);
				}
				else if (Input.GetMouseButtonUp(2))
				{
					InputEvent(InputActionType.STOP_ROTATE);
				}

			}

		}

	}
}