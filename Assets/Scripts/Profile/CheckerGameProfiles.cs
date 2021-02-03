using Assets.Scripts.WebSocket.Message;
using UnityEngine;

public class CheckerGameProfiles : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private PlayerInfo _playerOne;

    private PlayerInfo _playerTwo;

    public PlayerInfo PlayerOne { get { return _playerOne; } }

    public PlayerInfo PlayerTwo { get { return _playerTwo; } }

    public void SetProfileOne(PlayerInfo playerOne) {
        _playerOne = playerOne;
    }

    public void SetProfileTwo(PlayerInfo playerTwo)
    {
        _playerTwo = playerTwo;
    }
}
