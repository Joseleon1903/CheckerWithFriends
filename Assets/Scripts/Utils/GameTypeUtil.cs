using Assets.Script.WebSocket;
using System;

namespace Assets.Scripts.Utils
{
    public class GameTypeUtil
    {

        public static MultiplayerLobbyType GetGameConfigType()
        {

            SocketConfig config = UnityEngine.Object.FindObjectOfType<SocketConfig>();

            if (config == null) {
                throw new Exception("Not found Socket Configuration");
            }

            return config.LobbyType;
        }


    }
}
