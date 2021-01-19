using Assets.Scripts.General;
using UnityEngine;

namespace Assets.Scripts.Chess
{
	public struct OnlinePlayerMovement
	{
		public OnlinePlayerMovement(int nodePosXIn, int nodePosYIn, int nodeNextPosXIn, int nodeNextPosYIn)
		{
			nodePosX = nodePosXIn;
			nodePosY = nodePosYIn;
			nodeNextPosX = nodeNextPosXIn;
			nodeNextPosY = nodeNextPosYIn;
		}

		public int nodePosX { get; }
		public int nodePosY { get; }

		public int nodeNextPosX { get; }
		public int nodeNextPosY { get; }

		public override string ToString() => $"(X: {nodePosX} , Y: {nodePosY}, nX: {nodeNextPosX}, nY: {nodeNextPosY})";
	}

	public class OnlinePlayerBehavour : Singleton<OnlinePlayerBehavour>
    {
		public bool isOnlineGame;

		public PlayerType playerType = PlayerType.P1;

        private OnlinePlayerMovement _onlinePlayer;

        public OnlinePlayerMovement PlayerMovement { get { return _onlinePlayer; } set { _onlinePlayer = value; } }      

    }
}