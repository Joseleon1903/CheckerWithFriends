﻿using Assets.Scripts.General;
using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket;
using UnityEngine;

public class CheckerGameManager : Singleton<CheckerGameManager>
{

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

	private GameState gameState;

	private CheckerPlayer player;

	public GameState GameState
	{
		get { return gameState; }
	}

	public LayerMask CLickableMask
	{
		get { return clickableMask; }
	}

	public Camera MainCamera
	{
		get { return mainCamera; }
	}

	void Awake()
	{
		_destroyOnLoad = destroyOnLoad;
		gameState = new GameState();
	}

	public void StartGame() {

		Debug.Log("Entering in start game ");

		player = new CheckerPlayer();

		if (player.Type.Equals(PlayerType.P2) || true ) {

			player.EnableInput();
		}

		CanvasManagerUI.Instance.ShowAlertText("Match Start");
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

	private void SwitchPlayerOnlineGame() {
		Debug.Log("Entering in SwitchPlayerOnlineGame");
	
	}

	private void SwitchPlayerOfflineGame()
	{
		Debug.Log("Entering in SwitchPlayerOfflineGame");

		if (player != null && player.Type == PlayerType.P1)
		{
			player = new CheckerPlayer(PlayerType.P2);
		}
		else if (player != null && player.Type == PlayerType.P2) {

			player = new CheckerPlayer(PlayerType.P1);

		}

		if (Instance.GameState.IsGameOver) {
			GameOver();
			return;
		}

		CheckersBoard.Instance.isWhiteTurn = !CheckersBoard.Instance.isWhiteTurn;
		//only for offline mode
		CheckersBoard.Instance.isWhite = !CheckersBoard.Instance.isWhite;

		Instance.GameState.Release();

		CheckersBoard.Instance.ShowAlertPlayerTurn(player.Type);
		Debug.Log("Current player "+ player.Type);
	}

    public void GameOver()
    {
		GameOverType gameoverType = Instance.GameState.GameOverType;

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
                }
                else if (Instance.GameState.PlayerWin == PlayerType.P1)
                {
                    Debug.Log("OUT OF TIME: BLACK wins");
                }
                break;
        }
    }

    //public void End()
    //{
    //	Debug.Log("End Game();");
    //}

    //public void AddScore(string playerString)
    //{
    //	int newScore = PlayerPrefs.GetInt(playerString, 0) + 1;
    //	PlayerPrefs.SetInt(playerString, newScore);

    //	int maxScore = PlayerPrefs.GetInt(SCORE_MAX, 0);
    //	if (newScore >= maxScore)
    //	{
    //		End();
    //	}
    //}
}