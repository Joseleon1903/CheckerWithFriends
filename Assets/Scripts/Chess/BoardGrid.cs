using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Chess
{
	public class BoardGrid : MonoBehaviour
	{
		public static BoardGrid Instance { get; set; }

		[SerializeField]
		private int rows;
		[SerializeField]
		private int cols;

		[SerializeField]
		private bool checkDiagonals = true;

		private Node[,] grid;
		private Vector3 tileSize;
		private Vector3 size;

		public int Rows
		{
			get { return rows; }
		}

		public int Cols
		{
			get { return cols; }
		}

		public bool IsReady
		{
			get
			{
				for (int row = 0; row < rows; row++)
				{
					for (int col = 0; col < cols; col++)
					{
						Node node = grid[row, col];
						if (!node.IsReady) return false;
					}
				}
				return true;
			}
		}

		public int Size
		{
			get
			{
				return rows * cols;
			}
		}

		public Node GetNodeAt(int row, int col)
		{
			if (row < 0 || row >= rows || col < 0 || col >= cols) return null;
			return grid[row, col];
		}

		public Node GetNodeAt(Vector3 pos)
		{
			pos -= transform.position;

			float percentRow = (pos.z / size.z) + 0.5f;
			float percentCol = (pos.x / size.x) + 0.5f;
			percentRow = Mathf.Clamp01(percentRow);
			percentCol = Mathf.Clamp01(percentCol);
			int row = Mathf.RoundToInt((rows - 1) * percentRow);
			int col = Mathf.RoundToInt((cols - 1) * percentCol);

			return grid[row, col];
		}

        private void Start()
        {
			StartCoroutine(SpawnPieces());
		}

        void Awake()
		{
			Instance = this;
			grid = new Node[rows, cols];
			size = new Vector3(tileSize.x * cols, tileSize.y, tileSize.z * rows);
			CreateGrid();
		}

		void CreateGrid()
		{
			Vector3 bottomLeft = new Vector3(
				transform.position.x - size.x / 2 + tileSize.x / 2,
				transform.position.y,
				transform.position.z - size.z / 2 + tileSize.z / 2);
			Vector3 startPosition = bottomLeft;

			GameObject tile = new GameObject();

			for (int row = 0; row < rows; row++)
			{
				for (int col = 0; col < cols; col++)
				{
					startPosition.z = bottomLeft.z + tileSize.z * row;
					startPosition.x = bottomLeft.x + tileSize.x * col;
					GameObject go = Instantiate(tile, startPosition, tile.transform.rotation) as GameObject;
					Node dn = go.AddComponent<Node>();
					dn.row = row;
					dn.col = col;
					dn.rowChess = Converter.ToChessRow(row);
					dn.colChess = Converter.ToChessCol(col);
					grid[row, col] = dn;
					go.transform.parent = transform;
					go.transform.localScale = Vector3.zero;
					dn.ScaleIn(Random.Range(0f, 1f), Random.Range(1f, 2f), tile.transform.localScale);
				}
			}
		}

		IEnumerator SpawnPieces()
		{

			PlayerType p1T = PlayerType.P1;
			PlayerType p2T = PlayerType.P2;

			///*----------------------CHESS sample
			Quaternion quaterionWhite = Quaternion.Euler(0, 0, 0);
		    Quaternion quaterionBlack = Quaternion.Euler(0, 180, 0);

			//spawn circles
			for (int i = 0; i <= 7; i++)
			{
                SpawnPiece(new GridCoords(1, i), ChessBoarderManager.Instance.SpwanChess(5, i, 1, quaterionWhite), p1T); // p1 circ
                SpawnPiece(new GridCoords(6, i), ChessBoarderManager.Instance.SpwanChess(11, i, 6, quaterionBlack), p2T); // p2 circ
			}

			//spawn boxes
			SpawnPiece(new GridCoords(0, 0), ChessBoarderManager.Instance.SpwanChess(4, 0, 0, quaterionWhite), p1T); //p1 box
			SpawnPiece(new GridCoords(0, 7), ChessBoarderManager.Instance.SpwanChess(4, 7, 0, quaterionWhite), p1T); //p1 box
			SpawnPiece(new GridCoords(7, 0), ChessBoarderManager.Instance.SpwanChess(10, 0, 7, quaterionBlack), p2T); //p2 box
			SpawnPiece(new GridCoords(7, 7), ChessBoarderManager.Instance.SpwanChess(10, 7, 7, quaterionBlack), p2T); //p2 box

			////spawn triangles
			SpawnPiece(new GridCoords(0, 2), ChessBoarderManager.Instance.SpwanChess(2, 2, 0, quaterionWhite), p1T); //p1 tri
			SpawnPiece(new GridCoords(0, 5), ChessBoarderManager.Instance.SpwanChess(2, 5, 0, quaterionWhite), p1T); //p1 tri
			SpawnPiece(new GridCoords(7, 2), ChessBoarderManager.Instance.SpwanChess(8, 2, 7, quaterionBlack), p2T); //p2 tri
			SpawnPiece(new GridCoords(7, 5), ChessBoarderManager.Instance.SpwanChess(8, 5, 7, quaterionBlack), p2T); //p2 tri

			////spawn crosses
			SpawnPiece(new GridCoords(0, 4), ChessBoarderManager.Instance.SpwanChess(0, 4, 0, quaterionWhite), p1T); //p1 cross
			SpawnPiece(new GridCoords(7, 4), ChessBoarderManager.Instance.SpwanChess(6, 4, 7, quaterionBlack), p2T); //p2 cross

			////spawn hexagons
			SpawnPiece(new GridCoords(0, 3), ChessBoarderManager.Instance.SpwanChess(1, 3, 0, quaterionWhite), p1T); //p1 hex
			SpawnPiece(new GridCoords(7, 3), ChessBoarderManager.Instance.SpwanChess(7, 3, 7, quaterionBlack), p2T); //p2 hex

			////spawn rectangles - knights for testing
			SpawnPiece(new GridCoords(0, 1), ChessBoarderManager.Instance.SpwanChess(3, 1, 0, quaterionWhite), p1T); //p1 rect
			SpawnPiece(new GridCoords(0, 6), ChessBoarderManager.Instance.SpwanChess(3, 6, 0, quaterionWhite), p1T); //p1 rect
			SpawnPiece(new GridCoords(7, 1), ChessBoarderManager.Instance.SpwanChess(9, 1, 7, quaterionBlack), p2T); //p2 rect
			SpawnPiece(new GridCoords(7, 6), ChessBoarderManager.Instance.SpwanChess(9, 6, 7, quaterionBlack), p2T); //p2 rect

			yield return null;
		}

		public void SpawnPiece(GridCoords coords, GameObject piece, PlayerType playerType)
		{
			Node pieceNode = GetNodeAt(coords.row, coords.col);

			//player type
			GCPlayer player = null;
			switch (playerType)
			{
				case PlayerType.P1:
					player = GameManager.Instance.P1;
					break;
				case PlayerType.P2:
					player = GameManager.Instance.P2;
					break;
			}
			player.AddPieces(piece.GetComponent<Piece>());

			//piece.GetComponent<Piece>().PieceMovement = Creator.CreatePieceMovement(piece.GetComponent<Piece>().MovementType, player, piece.GetComponent<Piece>());

			piece.GetComponent<Piece>().UpdateNode(pieceNode);


		}

		public List<Node> GetNeighbours(Node node)
		{
			List<Node> neighbours = new List<Node>();
			for (int row = -1; row <= 1; row++)
			{
				for (int col = -1; col <= 1; col++)
				{

					//skip these ones
					if (row == 0 && col == 0)
						continue;
					if (!checkDiagonals && (row * row) == 1 && (col * col) == 1)
						continue;

					int checkRow = node.row + row;
					int checkCol = node.col + col;

					if (checkRow >= 0 && checkRow < rows && checkCol >= 0 && checkCol < cols)
					{
						neighbours.Add(grid[checkRow, checkCol]);
					}
				}
			}

			return neighbours;
		}

		public bool IsNearby(Node nodeA, Node nodeB)
		{
			for (int row = -1; row <= 1; row++)
			{
				for (int col = -1; col <= 1; col++)
				{

					//skip these ones
					if (row == 0 && col == 0)
						continue;
					if (!checkDiagonals && (row * row) == 1 && (col * col) == 1)
						continue;

					int checkRow = nodeA.row + row;
					int checkCol = nodeA.col + col;

					if (checkRow >= 0 && checkRow < rows && checkCol >= 0 && checkCol < cols)
					{
						if (grid[checkRow, checkCol] == nodeB) return true;
					}
				}
			}

			return false;
		}

		void OnDrawGizmos()
		{
			Gizmos.DrawWireCube(transform.position, new Vector3(2, 2, 2));
		}
	}
}
