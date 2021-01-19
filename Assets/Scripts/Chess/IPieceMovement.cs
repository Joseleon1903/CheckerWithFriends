namespace Assets.Scripts.Chess
{
	public delegate void ComputeBound();

	public interface IPieceMovement
	{

		event ComputeBound BoundComputations;
		void ComputeBound();
		void Compute();
		void Moved();
		bool IsMoved();
	}
}
