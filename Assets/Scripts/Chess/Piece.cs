using Assets.Scripts.General;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Chess
{

	public enum PieceType
	{
		NONE,
		CIRCLE,
		TRIANGLE,
		SQUARE,
		HEXAGON,
		CROSS,
		RECTANGLE,
	}

	public class Piece : Movable, IClickable
	{

		[SerializeField]
		private PieceType pieceType;
		[SerializeField]
		private Node node;


		private IPieceMovement pieceMovement;
		private bool dropping;

		private List<Node> possibleMoves;
		private List<Node> possibleEats;

		public IPieceMovement PieceMovement
		{
			get { return pieceMovement; }
			set
			{
				pieceMovement = value;
			}
		}

		public bool IsMoved
		{
			get
			{
				if (pieceMovement != null)
				{
					return pieceMovement.IsMoved();
				}

				return false;
			}
		}

		public List<Node> PossibleMoves
		{
			get { return possibleMoves; }
		}

		public List<Node> PossibleEats
		{
			get { return possibleEats; }
		}

		public PieceType PieceType
		{
			get { return pieceType; }
		}

		void Awake()
		{
			possibleEats = new List<Node>();
			possibleMoves = new List<Node>();
		}


		protected override void Start()
		{
			base.Start();
		}
		
		public bool IsPossibleMove(Node node)
		{
			return this.possibleMoves.Contains(node);
		}

		public bool IsPossibleEat(Node node)
		{
			return this.possibleEats.Contains(node);
		}

		public void AddPossibleMoves(params Node[] nodes)
		{
			for (int i = 0; i < nodes.Length; i++)
			{
				this.possibleMoves.Add(nodes[i]);
			}
		}

		public void AddPossibleEats(params Node[] nodes)
		{
			for (int i = 0; i < nodes.Length; i++)
			{
				this.possibleEats.Add(nodes[i]);
			}
		}

		public void ClearPossibleMoves()
		{
			while (possibleMoves.Count > 0)
			{
				Node node = possibleMoves[0];
				possibleMoves.Remove(node);
			}
		}

		public void ClearPossibleEats()
		{
			while (possibleEats.Count > 0)
			{
				Node node = possibleEats[0];
				possibleEats.Remove(node);
			}
		}

		public void Compute()
		{
			pieceMovement.Compute();
		}

		public override void MoveToXZ(Node node)
		{
			pieceMovement.Moved();
		}

		public string ChessCoords
		{
			get
			{
				if (node == null) return null;

				return node.ChessCoords;
			}
		}

		public Node Node
		{
			get { return node; }
		}

		public void UpdateNode(Node n)
		{
			if (node != null)
			{
				node.Clear();
			}
			node = n;
			if (node != null)
			{
				node.Piece = this;
			}
		}

		public bool Inform<T>(T arg)
		{
			//TODO
			return true;
		}

		public void Drop()
		{
			dropping = true;
			ready = false;
		}

		//EXPERIMENT
		void OnCollisionEnter(Collision collision)
		{
			if (dropping && collision.collider.gameObject)
			{
				ready = true;
				dropping = false;
			}
		}

	}
}
