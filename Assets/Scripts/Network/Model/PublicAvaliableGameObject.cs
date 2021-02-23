using System;

[Serializable]
public class PublicAvaliableGameObject : IComparable<PublicAvaliableGameObject>
{
    public int id;

    public string type;

    public string lobbyCode;

    public int capacity;

    public int playerCount;

    public string sessionIdentifier;

    public string status;

    public string lobbyMap;

    public string lobbyTime;

    public string lobbyBet;

    public string gameLobby;

    public PublicAvaliableGameObject(int id, string type, string lobbyCode, int capacity, int playerCount, string sessionIdentifier, string status, string lobbyMap, string lobbyTime, string lobbyBet, string gameLobby)
    {
        this.id = id;
        this.type = type;
        this.lobbyCode = lobbyCode;
        this.capacity = capacity;
        this.playerCount = playerCount;
        this.sessionIdentifier = sessionIdentifier;
        this.status = status;
        this.lobbyMap = lobbyMap;
        this.lobbyTime = lobbyTime;
        this.lobbyBet = lobbyBet;
        this.gameLobby = gameLobby;
    }

    public int CompareTo(PublicAvaliableGameObject other)
    {
        int sort = -1;

        if (other == null) sort= 1;

        if (this.id > other.id) {
            sort = 1;
        }

        return sort;
    }
}
