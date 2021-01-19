using Assets.Scripts.General;
using Assets.Scripts.Utils;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Chess
{
	public class GameManager : Singleton<GameManager>
	{

		[SerializeField]
		private BoardGrid grid;
		[SerializeField]
		private Camera mainCamera;
		[SerializeField]
		private LayerMask clickableMask;

		public const string PLAYER_WHITE = "PLAYER_WHITE";
		public const string PLAYER_BLACK = "PLAYER_BLACK";
		public const string SCORE_MAX = "SCORE_MAX";
		public const string GAME_MAX = "GAME_MAX";
		public const string GAME_CURRENT = "GAME_CURRENT";
		public const string CAMERA_VIEW = "CAMERA_VIEW";

		public delegate void SwitchedPlayer();
		public static event SwitchedPlayer SwitchedEvent;

		private GCPlayer p1 = new GCPlayer(PlayerType.P1);
		private GCPlayer p2 = new GCPlayer(PlayerType.P2);

		private GCPlayer currentPlayer;

		public GameState gameState;

		private bool whiteTurn;
		//END of EXPERIMENT_TIMER

		public bool ready;

		public GCPlayer PlayerOponent
		{
			get
			{
				return Opponent(currentPlayer);
			}
		}

        public GameState GameState
		{
			get { return gameState; }
		}

		public GCPlayer CurrentPlayer
		{
			get { return currentPlayer; }
		}

		public LayerMask CLickableMask
		{
			get { return clickableMask; }
		}

		public Camera MainCamera
		{
			get { return mainCamera; }
		}

		public GCPlayer P1
		{
			get { return p1; }
		}

		public GCPlayer P2
		{
			get { return p2; }
		}

		public bool IsReady
		{
			get { return ready; }
		}

		public BoardGrid Grid
		{
			get { return grid; }
		}

        void Awake()
		{
			_destroyOnLoad = destroyOnLoad;
			gameState = new GameState();
		}

		// Use this for initialization
		void Start()
		{
			StartCoroutine(init());
		}

		IEnumerator init()
		{
			//IMPORTANT
			p1.ComputePieces();
			p2.ComputePieces();

			
			SwitchPlayer(); //if null current player = p1
			

			//all objects are now ready
			ready = true;

			yield return null;
		}

		// Update is called once per frame
		void Update()
		{

#if UNITY_EDITOR
			if (Input.GetKeyDown(KeyCode.Z))
			{
				GameManager.Instance.GameState.Checkmate();
				GameOver(p1, GameOverType.CHECKMATE); //TODO delete
			}
			else if (Input.GetKeyDown(KeyCode.X))
			{
				GameManager.Instance.GameState.Checkmate();
				GameOver(p2, GameOverType.CHECKMATE); //TODO delete
			}
			else if (Input.GetKeyDown(KeyCode.C))
			{
				GameManager.Instance.GameState.Stalemate();
				GameOver(p2, GameOverType.STALEMATE);
			}
#endif

			if (!ready) return;
			if (gameState.IsGameOver) return;
		}


		public void SwitchPlayer()
		{
			if (currentPlayer != null)
			{
				currentPlayer.DisableInput();
			}

			if (currentPlayer == p2)
			{
				currentPlayer = p1;
				whiteTurn = true;
				//mainCamera.GetComponent<SwitchAngle>().SwitchCamera(PlayerType.P1);
				Debug.Log("Change campera point of view for opponent player 1");

			}
			else if (currentPlayer == p1)
			{
				currentPlayer = p2;
				whiteTurn = false;
				//mainCamera.GetComponent<SwitchAngle>().SwitchCamera(PlayerType.P2);
				Debug.Log("Change campera point of view for opponent player 2");
			}
			else
			{
				currentPlayer = p1;
				whiteTurn = true;
			}

			//IF checkmate
			if (Rules.HasNoMove())
			{
				if (currentPlayer.IsChecked)
				{
					Instance.GameState.Checkmate();
					GameOver(PlayerOponent, GameOverType.CHECKMATE);
					Debug.Log("CHECKMATE");
				}
				else
				{
					Instance.GameState.Stalemate();
					print("STALEMATE");
					GameOver(currentPlayer, GameOverType.STALEMATE);
				}
			}

			currentPlayer.EnableInput();
			if (SwitchedEvent != null)
			{
				SwitchedEvent(); //EXPERIMENTAL
			}

			print("Turn of: " + currentPlayer.Type);

			//if  player is cheked 
			if (currentPlayer.IsChecked == true) {
				ChessBoarderManager.Instance.PlayerCheckedHightLight(currentPlayer);
			}
			CanvasManagerUI.Instance.ShowAlertText("It's player " + currentPlayer.Type + " Turn");
		}

		public GCPlayer Opponent(GCPlayer player)
		{
			if (player == null) return null;
			if (player == p1) return p2;
			else return p1;
		}

		public void GameOver(GCPlayer winner, GameOverType gameOverType)
		{
			switch (gameOverType)
			{
				case GameOverType.CHECKMATE:
					if (winner == p2)
					{
						Debug.Log("CHECKMATE: BLACK wins");

						var NewState =new ChessboardState { isGameOver = true, gameOverType = GameOverType.CHECKMATE, playerWin = winner.Type };
						ChessBoarderManager.Instance.ChessboardState = NewState;

						//CanvasManagerUI.Instance.ShowGameOverCanvas();
					}
					else if (winner == p1)
					{
						Debug.Log("CHECKMATE: WHITE wins");

						var NewState = new ChessboardState { isGameOver = true, gameOverType = GameOverType.CHECKMATE, playerWin = winner.Type };
						ChessBoarderManager.Instance.ChessboardState = NewState;
						//CanvasManagerUI.Instance.ShowGameOverCanvas();
					}
					break;
				case GameOverType.STALEMATE:
					Debug.Log("STALEMATE: It's a tie");
					break;
				case GameOverType.OUT_OF_TIME:
					if (winner == p1)
					{
						Debug.Log("OUT OF TIME: WHITE wins");
						AddScore(PLAYER_WHITE);
					}
					else if (winner == p2)
					{
						Debug.Log("OUT OF TIME: BLACK wins");
						AddScore(PLAYER_BLACK);
					}
					break;
			}
		}

		public void End()
		{
			Debug.Log("End Game();");
		}

		public void AddScore(string playerString)
		{
			int newScore = PlayerPrefs.GetInt(playerString, 0) + 1;
			PlayerPrefs.SetInt(playerString, newScore);

			int maxScore = PlayerPrefs.GetInt(SCORE_MAX, 0);
			if (newScore >= maxScore)
			{
				End();
			}
		}
	}
}
