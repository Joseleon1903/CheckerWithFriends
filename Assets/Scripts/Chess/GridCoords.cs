using System;

namespace Assets.Scripts.Chess
{
	[Serializable]
	public struct GridCoords
	{
		public int row, col;

		public GridCoords(int row, int col)
		{
			this.row = row;
			this.col = col;
		}

		public override string ToString()
		{
			return "coords: " + row + ", " + col;
		}
	}
}
