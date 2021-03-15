using Assets.Scripts.General;
using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket;
using UnityEngine;

namespace Assets.Scripts.Checkers
{
	public class CheckerGameManager : Singleton<CheckerGameManager>
	{

		[SerializeField]
		private LayerMask clickableMask;

		private GameState gameState;

		private CheckerPlayer player;

		public CheckerPlayer Player
		{
			get { return player; }
		}
		public GameState GameState
		{
			get { return gameState; }
		}

		public LayerMask CLickableMask
		{
			get { return clickableMask; }
		}

		void Awake()
		{
			_destroyOnLoad = destroyOnLoad;
			gameState = new GameState();

		}

		public void StartGame()
		{

			var client = FindObjectOfType<ClientWSBehavour>();
			if (client != null)
			{
				StartOnlineGame();
			}
			else
			{
				StartOfflineGame();
			}
			CheckersBoard.Instance.isWhiteTurn = true;
			CoolDownUtil.StartCoolDownPlayer(PlayerType.P1);
		}

		private void StartOnlineGame()
		{

			player = new CheckerPlayer();

			if (player.Type.Equals(PlayerType.P1))
			{
				player.EnableInput();
			}

			CanvasManagerUI.Instance.ShowAlertText("Match Start");

			CanvasManagerUI.Instance.StartGameCanvasView();
		}

		private void StartOfflineGame()
		{

			Debug.Log("Entering in Start Offline Game ");

			player = new CheckerPlayer(PlayerType.P1);

			player.EnableInput();

			CanvasManagerUI.Instance.ShowAlertText("Match Start");

			CanvasManagerUI.Instance.StartGameCanvasView();
		}

		public void SwitchPlayer()
		{
			var client = FindObjectOfType<ClientWSBehavour>();
			if (client != null)
			{
				SwitchPlayerOnlineGame();
			}
			else
			{
				SwitchPlayerOfflineGame();
			}

		}

		private void SwitchPlayerOnlineGame()
		{

			Debug.Log("Entering in SwitchPlayerOnlineGame");

			if (Instance.GameState.IsGameOver)
			{
				GameOver();
				return;
			}

			CheckersBoard.Instance.isWhiteTurn = !CheckersBoard.Instance.isWhiteTurn;

			if (player.Type.Equals(PlayerType.P1) && CheckersBoard.Instance.isWhiteTurn)
			{
				CheckersBoard.Instance.ShowAlertPlayerTurn(PlayerType.P1);
				CoolDownUtil.StartCoolDownPlayer(PlayerType.P1);
				CoolDownUtil.ResetCoolDownPlayer(PlayerType.P2);
				player.EnableInput();
			}

			if (player.Type.Equals(PlayerType.P1) && !CheckersBoard.Instance.isWhiteTurn)
			{
				CheckersBoard.Instance.ShowAlertPlayerTurn(PlayerType.P2);
				CoolDownUtil.StartCoolDownPlayer(PlayerType.P2);
				CoolDownUtil.ResetCoolDownPlayer(PlayerType.P1);
				player.DisableInput();
			}

			if (player.Type.Equals(PlayerType.P2) && !CheckersBoard.Instance.isWhiteTurn)
			{
				CheckersBoard.Instance.ShowAlertPlayerTurn(PlayerType.P2);
				CoolDownUtil.StartCoolDownPlayer(PlayerType.P2);
				CoolDownUtil.ResetCoolDownPlayer(PlayerType.P1);
				player.EnableInput();
			}

			if (player.Type.Equals(PlayerType.P2) && CheckersBoard.Instance.isWhiteTurn)
			{
				player.DisableInput();
				CoolDownUtil.ResetCoolDownPlayer(PlayerType.P2);
				CoolDownUtil.StartCoolDownPlayer(PlayerType.P1);
				CheckersBoard.Instance.ShowAlertPlayerTurn(PlayerType.P1);
			}

			Instance.GameState.Release();
			//reset ate piece counter 

			Debug.Log("Current player " + player.Type);
			//reset ate piece counter 
			CheckersBoard.Instance.ResetAteMove();
		}

		private void SwitchPlayerOfflineGame()
		{
			Debug.Log("Entering in SwitchPlayerOfflineGame");

			if (player != null && player.Type == PlayerType.P1)
			{
				player = new CheckerPlayer(PlayerType.P2);
				CoolDownUtil.ResetCoolDownPlayer(PlayerType.P1);
				CoolDownUtil.StartCoolDownPlayer(PlayerType.P2);
			}
			else if (player != null && player.Type == PlayerType.P2)
			{
				player = new CheckerPlayer(PlayerType.P1);
				CoolDownUtil.ResetCoolDownPlayer(PlayerType.P2);
				CoolDownUtil.StartCoolDownPlayer(PlayerType.P1);
			}

			if (Instance.GameState.IsGameOver)
			{
				Invoke("GameOver", 2.0f);
				return;
			}

			CheckersBoard.Instance.isWhiteTurn = !CheckersBoard.Instance.isWhiteTurn;

			Instance.GameState.Release();

			CheckersBoard.Instance.ShowAlertPlayerTurn(player.Type);

			//reset ate piece counter 
			CheckersBoard.Instance.ResetAteMove();
			Debug.Log("Current player " + player.Type);
		}

		public void GameOver()
		{
			GameOverType gameoverType = Instance.GameState.GameOverType;

			CanvasManagerUI.Instance.ShowAlertText($"Player {Instance.GameState.PlayerWin} Win the game");

			switch (gameoverType)
			{
				case GameOverType.CHECKMATE:
					if (Instance.GameState.PlayerWin == PlayerType.P2)
					{
						Debug.Log("CHECKMATE: player BLACK win");

						CanvasManagerUI.Instance.ShowGameOverCanvas();
					}
					else if (Instance.GameState.PlayerWin == PlayerType.P1)
					{
						Debug.Log("CHECKMATE: player WHITE wins");
						CanvasManagerUI.Instance.ShowGameOverCanvas();
					}
					break;

				case GameOverType.OUT_OF_TIME:
					if (Instance.GameState.PlayerWin == PlayerType.P1)
					{
						Debug.Log("OUT OF TIME: WHITE wins");
						CanvasManagerUI.Instance.ShowGameOverCanvas();
					}
					else if (Instance.GameState.PlayerWin == PlayerType.P2)
					{
						Debug.Log("OUT OF TIME: BLACK wins");
						CanvasManagerUI.Instance.ShowGameOverCanvas();
					}
					break;
			}
		}

	}
}