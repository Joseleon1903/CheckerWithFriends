﻿using Assets.Scripts.General;

namespace Assets.Scripts.Chess
{

	public class Node : Scalable, IHeapItem<Node>, IClickable
	{
		public int row;
		public int col;

		public char rowChess;
		public char colChess;

		//public bool walkable = true;

		public int gCost;
		public int hCost;

		private int heapIndex;

		private Piece piece;

		protected override void Start()
		{
			base.Start();
		}

		public string ChessCoords
		{
			get { return "" + colChess + rowChess; }
		}

		public Piece Piece
		{
			get { return piece; }
			set
			{
				piece = value;
			}
		}

		public bool EmptySpace
		{
			get
			{
				return piece == null;
			}
		}

		public void Clear()
		{
			piece = null;
		}

		public int fCost
		{
			get
			{
				return gCost + hCost;
			}
		}

		public int HeapIndex
		{
			get
			{
				return heapIndex;
			}
			set
			{
				heapIndex = value;
			}
		}

		public bool Inform<T>(T arg)
		{
			//TODO
			return true;
		}

		public int CompareTo(Node nodeToCompare)
		{
			int compare = fCost.CompareTo(nodeToCompare.fCost);
			if (compare == 0)
			{
				compare = hCost.CompareTo(nodeToCompare.hCost);
			}

			return compare;
		}

		public override string ToString()
		{
			return "" + row + "x" + col;
		}
    }
}
