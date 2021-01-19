using UnityEngine;

namespace Assets.Scripts
{
    public class PawnChess : ChessBehaviour
    {
        private int[] _enPassantMove;

        public int[] EnPassantMove { get { return _enPassantMove; } }

        private void Start()
        {
            _enPassantMove = new int[]{ -1,-1,-1,-1};
        }

        public override bool[,] PossibleMove()
        {
            bool[,] r = new bool[8, 8];
            ChessBehaviour c, c2;

            Debug.Log("CurrentPos : " + CurrentX + " - " + CurrentY);

            if (_enPassantMove[0] != -1 && _enPassantMove[1] != -1)
            {
                r[_enPassantMove[0], _enPassantMove[1]] = true;
            }

            //white tema move
            if (isWhite)
            {
                //diagonal left
                if (CurrentX != 0 && CurrentY != 7)
                {
                    c = ChessBoarderManager.Instance.ChessTable[CurrentX - 1, CurrentY + 1];
                    if (c != null && !c.isWhite)
                        r[CurrentX - 1, CurrentY + 1] = true;
                }

                ////diagonal right
                if (CurrentX != 7 && CurrentY != 0)
                {
                    c = ChessBoarderManager.Instance.ChessTable[CurrentX + 1, CurrentY + 1];
                    if (c != null && !c.isWhite)
                        r[CurrentX + 1, CurrentY + 1] = true;
                }

                ////middle

                if (CurrentY != 7)
                {
                    c = ChessBoarderManager.Instance.ChessTable[CurrentX, CurrentY +1];
                    if (c == null)
                        r[CurrentX, CurrentY +1] = true;
                }

                //middle on first move
                if (CurrentY == 1)
                {

                    c = ChessBoarderManager.Instance.ChessTable[CurrentX , CurrentY+1];

                    c2 = ChessBoarderManager.Instance.ChessTable[CurrentX, CurrentY + 2];

                    if (c == null && c2 == null)
                    {
                        r[CurrentX , CurrentY + 2] = true;
                    }
                }
            }
            else {

                ////diagonal left
                if (CurrentX != 7 && CurrentY != 0)
                {
                    
                    c = ChessBoarderManager.Instance.ChessTable[CurrentX + 1, CurrentY - 1];
                    if (c != null && c.isWhite)
                        r[CurrentX + 1, CurrentY - 1] = true;
                }

                ////diagonal right
                if (CurrentX != 0 && CurrentY != 7)
                {

                    c = ChessBoarderManager.Instance.ChessTable[CurrentX - 1, CurrentY - 1];
                    if (c != null && c.isWhite)
                        r[CurrentX - 1, CurrentY - 1] = true;
                }

                ////middle
                if (CurrentY != 0)
                {
                    c = ChessBoarderManager.Instance.ChessTable[CurrentX  , CurrentY - 1];
                    if (c == null)
                        r[CurrentX, CurrentY-1] = true;
                }

                ////middle on first move
                if (CurrentY == 6)
                {
                    c = ChessBoarderManager.Instance.ChessTable[CurrentX, CurrentY-1];

                    c2 = ChessBoarderManager.Instance.ChessTable[CurrentX, CurrentY -2];

                    if (c == null && c2 == null)
                    {
                        r[CurrentX , CurrentY -2 ] = true;
                    }
                }
            }

            return r;
        }


    }
}