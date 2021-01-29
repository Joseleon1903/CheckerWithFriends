namespace Assets.Scripts.Utils
{

	public enum GameStateType
	{
		RAIL_CAMERA,
		WAITING,
		HOLDING,
		PLACING,
		GAME_OVER
	}

	public enum GameOverType
	{
		CHECKMATE,
		SURRENDER,
		OUT_OF_TIME,
	}

	public class GameState
	{

		private GameStateType state;
		private GameOverType gameOverType;
		private PlayerType playerWin;

		public GameState()
		{
			state = GameStateType.RAIL_CAMERA;
		}

		public GameStateType State
		{
			get { return state; }
			set
			{
				state = value;
			}
		}

		public PlayerType PlayerWin
		{
			get { return playerWin; }
			set
			{
				playerWin = value;
			}
		}

		public GameOverType GameOverType
		{
			get { return gameOverType; }
			set
			{
				gameOverType = value;
			}
		}

		public bool IsWaiting
		{
			get { return state == GameStateType.WAITING; }
		}

		public bool IsRail
		{
			get { return state == GameStateType.RAIL_CAMERA; }
		}

		public bool IsPlacing
		{
			get { return state == GameStateType.PLACING; }
		}

		public bool IsHolding
		{
			get { return state == GameStateType.HOLDING; }
		}

		public void Grab()
		{
			state = GameStateType.HOLDING;
		}

		public void Place()
		{
			state = GameStateType.PLACING;
		}

		public void Release()
		{
			state = GameStateType.WAITING;
		}

		public void Cancel()
		{
			state = GameStateType.WAITING;
		}

		public void Start()
		{
			state = GameStateType.WAITING;
		}
		 
		public void Checkmate(PlayerType type)
		{
			state = GameStateType.GAME_OVER;
			gameOverType = GameOverType.CHECKMATE;
			playerWin = type;
		}

		public void OutOfTime()
		{
			state = GameStateType.GAME_OVER;
			gameOverType = GameOverType.OUT_OF_TIME;
		}

		public bool IsGameOver
		{
			get
			{
				if (state == GameStateType.GAME_OVER) return true;
				return false;
			}
		}
	}

}