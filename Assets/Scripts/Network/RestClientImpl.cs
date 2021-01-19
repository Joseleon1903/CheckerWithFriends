using Assets.Script.WebSocket;
using UnityEngine;

public class RestClientImpl
{

	public static string HostApi()
	{
		if (Application.platform ==RuntimePlatform.Android) {

			return "https://router-game-server.herokuapp.com";
		}

		if (Application.platform == RuntimePlatform.WindowsEditor) {

			return "http://localhost:8085";
		}

		if (Application.platform == RuntimePlatform.WebGLPlayer)
		{

			return "http://localhost:8085";
		}

		return "http://localhost:8085";
	}


	public static string GetPublicMatchRestPath(MultiplayerLobbyType type, int count)
	{
		if (Application.platform == RuntimePlatform.Android && type.Equals(MultiplayerLobbyType.Checker))
		{
			return "https://router-game-server.herokuapp.com/checker/lobby?count="+count;
		}

		if (Application.platform == RuntimePlatform.Android && type.Equals(MultiplayerLobbyType.Chess))
		{
			return "https://router-game-server.herokuapp.com/chess/lobby?count=" + count;
		}

		if (type.Equals(MultiplayerLobbyType.Checker))
		{
			return "http://localhost:8085/checker/lobby?count=" + count;
		}

		if (type.Equals(MultiplayerLobbyType.Chess))
		{
			return "http://localhost:8085/chess/lobby?count=" + count;
		}

		return "http://localhost:8085";
	}

}