using System;

namespace Assets.Scripts.Chess
{
	[Serializable]
	public class PieceData
	{
		public PieceType pieceType;
		public PlayerType playerType;

		public override string ToString()
		{
			return "Piece: \n"
			 + "\n\tPieceType: " + pieceType
			 + "\n\tPlayerType: " + playerType.ToString();
		}
	}
}
