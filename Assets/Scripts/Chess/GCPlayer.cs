using Assets.Scripts.General;
using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Chess
{
	public enum PlayerType
	{
		P1, P2
	}

	public class GCPlayer : IClicker, IInputReceiver
	{

		private PlayerType type;

		private List<Piece> pieces;
		private List<Piece> eatenPieces;

		private Piece _piece;
		private Piece _checkedBy; //Experimental

		//Experimental
		public bool IsChecked
		{
			get { return _checkedBy != null; }
		}

		public List<Piece> Pieces
		{
			get { return pieces; }
		}

		public List<Piece> EatenPieces
		{
			get { return eatenPieces; }
		}

		//Experimental
		public Piece CheckedBy
		{
			get { return _checkedBy; }
			set
			{
				_checkedBy = value;
			}
		}

		public Piece HoldingPiece
		{
			get { return _piece; }
		}

		public bool IsReady
		{
			get
			{
				for (int i = 0; i < pieces.Count; i++)
				{
					if (!pieces[i].IsReady) return false;
				}

				return true;
			}
		}

		public PlayerType Type
		{
			get { return type; }
		}

		public GCPlayer(PlayerType type)
		{
			this.type = type;
			pieces = new List<Piece>();
			eatenPieces = new List<Piece>();
		}

		public void EnableInput()
		{
			InputManager.InputEvent += OnInputEvent;
		}

		public void DisableInput()
		{
			InputManager.InputEvent -= OnInputEvent;
		}

		void OnDisable()
		{
			DisableInput();
		}

		public void OnInputEvent(InputActionType action)
		{

			if (!OnlinePlayerBehavour.Instance.isOnlineGame || OnlinePlayerBehavour.Instance.isOnlineGame && OnlinePlayerBehavour.Instance.playerType == PlayerType.P1)
			{
				OninputEventOfflineOrHostPlayer(action);
			}
			else 
			{
				OnInputEventPlayerOnline(action);
			}
			
		}

		public void OnInputEventPlayerOnline(InputActionType action) {

			switch (action)
			{
				case InputActionType.GRAB_PIECE:
					Debug.Log("Entering in player grab piece");

					Node gNode = Finder.RayHitNodeFromScreen(Input.mousePosition);
					if (gNode == null) break;
					ChessBehaviour chess =ChessBoarderManager.Instance.ChessTable[gNode.col, gNode.row];
					if (chess == null) break;
					if (chess != null)
					{
						int currentX = chess.CurrentX;
						int currentY = chess.CurrentY;
						ChessBoarderManager.Instance.SelectChess(currentX, currentY, false);
						GameManager.Instance.GameState.Grab();
					}

					break;
				case InputActionType.CANCEL_PIECE:
					Debug.Log("Entering in player cancel piece");
					if (_piece != null)
					{
						_piece.Drop();
						_piece = null;
					}
					GameManager.Instance.GameState.Cancel();
					SelectorBehavour.Instance.RemoveSelectedPiece();

					break;
				case InputActionType.PLACE_PIECE:
					Debug.Log("Entering in player place piece");

					Node tNode = Finder.RayHitNodeFromScreen(Input.mousePosition);

					Piece tPiece = (tNode != null && tNode.Piece != null) ? tNode.Piece : null;

					if (tNode == null)
					{
						GameManager.Instance.GameState.Cancel();
						break;
					}
					else if (tPiece != null && tPiece.GetComponent<ChessBehaviour>().isWhite == ChessBoarderManager.Instance.isWhiteTurn)
					{
						GameManager.Instance.GameState.Cancel();
						break;
					}

					bool isPlayerCheked = ValidatePlayerCheked();
					ChessBehaviour select = ChessBoarderManager.Instance.SelectedPiece;
					bool[,] allowedMoves = ChessBoarderManager.Instance.ChessTable[select.CurrentX, select.CurrentY].PossibleMove();

					if (allowedMoves[tNode.col, tNode.row] && !isPlayerCheked)
					{
						ChessBoarderManager.Instance.MoveChess(tNode.col, tNode.row, true);

						SelectorBehavour.Instance.RemoveSelectedPiece();

						InputManager.Instance.InvokeInputEvent(InputActionType.CANCEL_PIECE);

						CanvasManagerUI.Instance.ShowAlertText("It's player P1 turn");

					}
					else if (allowedMoves[tNode.col, tNode.row] && isPlayerCheked)
					{
						ClientWSBehavour client = GameObject.FindObjectOfType<ClientWSBehavour>();

						//if (client != null) {
						//	string lobbyCode = client.profile.lobbyCode;
						//	string playerType = OnlinePlayerBehavour.Instance.playerType.ToString().ToUpper();
						//	ChessValidateCheckMoveReq move = new ChessValidateCheckMoveReq(lobbyCode, GameType.CHESS.ToString().ToUpper(),
						//		playerType, select.CurrentX.ToString(), select.CurrentY.ToString(), tNode.col.ToString(), tNode.row.ToString());
						//	client.Send(move.GetMessageText());
						//}

						InputManager.Instance.InvokeInputEvent(InputActionType.CANCEL_PIECE);
					}
					else
					{
						UnSelected();
						GameManager.Instance.GameState.Cancel();
					}
					break;
			}

		}

        public void OninputEventOfflineOrHostPlayer(InputActionType action) {

			switch (action)
			{
				case InputActionType.GRAB_PIECE:
					Debug.Log("Entering in player grab piece");
					Node gNode = Finder.RayHitNodeFromScreen(Input.mousePosition);
					if (gNode == null) break;
					_piece = gNode.Piece;
					if (_piece == null) break;
					if (Click(gNode) && _piece && Has(_piece) && Click(_piece))
					{
						_piece.Compute();
						int currentX = _piece.GetComponent<ChessBehaviour>().CurrentX;
						int currentY = _piece.GetComponent<ChessBehaviour>().CurrentY;
						ChessBoarderManager.Instance.SelectChess(currentX, currentY, false);
						GameManager.Instance.GameState.Grab();
					}

					//check clickable for tile and piece then pass Player
					//check if player has piece - PIECE 
					//check if player has piece if not empty - NODE 
					break;
				case InputActionType.CANCEL_PIECE:
					if (_piece != null)
					{
						_piece.Drop();
						_piece = null;
						GameManager.Instance.GameState.Cancel();
					}
					break;
				case InputActionType.PLACE_PIECE:

					Node tNode = Finder.RayHitNodeFromScreen(Input.mousePosition);

					Piece tPiece = (tNode != null && tNode.Piece != null) ? tNode.Piece : null;

					if (tNode == null || _piece == null)
					{
						UnSelected();
						GameManager.Instance.GameState.Cancel();
						break;
					}
					else if (tPiece != null && tPiece.GetComponent<ChessBehaviour>().isWhite == ChessBoarderManager.Instance.isWhiteTurn)
					{
						UnSelected();
						GameManager.Instance.GameState.Cancel();
						break;
					}

					if (tPiece == null || _piece.IsPossibleMove(tNode))
					{
						if (_piece.IsPossibleMove(tNode))
						{
							if (Rules.IsCheckMove(this, _piece, tNode, true))
							{
								Debug.Log("Move checked"); // do nothing
								CanvasManagerUI.Instance.ShowAlertText("Player is checked");

							}
							else
							{
								_piece.MoveToXZ(tNode);
								GameManager.Instance.GameState.Place();
								Drop();
								ChessBoarderManager.Instance.MoveChessMethod(tNode.col, tNode.row, true);
							}
						}
					}
					else
					{
						if (_piece.IsPossibleEat(tNode))
						{
							if (Rules.IsCheckEat(this, _piece, tNode, true))
							{
								Debug.Log("Eat checked"); // do nothing
							}
							else
							{
								GCPlayer oppPlayer = GameManager.Instance.Opponent(this);
								oppPlayer.RemovePiece(tPiece);
								AddEatenPieces(tPiece);
								_piece.MoveToXZ(tNode);
								GameManager.Instance.GameState.Place();
								Drop();

								ChessBoarderManager.Instance.MoveChessMethod(tNode.col, tNode.row, true);
							}
						}
					}
					break;
			}

		}

		public void ClearPiecesPossibles()
		{
			for (int i = 0; i < pieces.Count; i++)
			{
				pieces[i].ClearPossibleEats();
				pieces[i].ClearPossibleMoves();
			}
		}

		public void ClearCheck()
		{
			if (_checkedBy == null) return;
			_checkedBy = null;
		}

		//the methods inside must be in order
		private void Drop()
		{
			_piece.Drop();
			_piece.Compute();
			GameManager.Instance.GameState.Release();
			_piece = null;
		}

		private void UnSelected() {
			if (_piece != null) {
				_piece.Drop();
				_piece.Compute();
				GameManager.Instance.GameState.Cancel();
				_piece = null;
			}
		}

		public bool Has(Piece piece)
		{
			return pieces.Contains(piece);
		}

		public bool Click(IClickable clickable)
		{
			if (clickable == null) return false;
			return clickable.Inform<GCPlayer>(this);
		}

		public void AddPieces(params Piece[] pieces)
		{
			for (int i = 0; i < pieces.Length; i++)
			{
				this.pieces.Add(pieces[i]);
			}
		}

		public void AddEatenPieces(params Piece[] pieces)
		{
			for (int i = 0; i < pieces.Length; i++)
			{
				this.eatenPieces.Add(pieces[i]);
			}
		}

		public bool RemovePiece(Piece piece)
		{
			return pieces.Remove(piece);
		}

		public void ComputePieces()
		{
			for (int i = 0; i < pieces.Count; i++)
			{
				pieces[i].Compute();
			}
		}

		private bool ValidatePlayerCheked()
		{
			GameObject[] kings = GameObject.FindGameObjectsWithTag("King");

			foreach (GameObject chess in kings)
			{
				ChessBehaviour piece = chess.GetComponent<ChessBehaviour>();

				if (!piece.isWhite && chess.GetComponent<KingChess>().IsCheck)
				{
					return true;
				}
			}
			return false;
		}
	}

}
