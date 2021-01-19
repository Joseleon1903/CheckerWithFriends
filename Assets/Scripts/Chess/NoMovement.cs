using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Chess
{
	public class NoMovement : Movement, IPieceMovement
	{
		public NoMovement(GCPlayer player, Piece piece) : base(player, piece)
		{
			BoundComputations += ComputeBound;
		}

		public override void ComputeBound()
		{
			//do nothing
		}

	}
}
