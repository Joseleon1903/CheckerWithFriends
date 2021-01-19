namespace Assets.Scripts
{
    public class KingChess : ChessBehaviour
    {

        private bool _isFirstMove =true;

        private bool _isCheck = false;

        public bool IsFirstMove { get; set; }

        public bool IsCheck { get; set;}

        private int[] _isCastlingMove;

        public int[] isCastlingMove { get { return _isCastlingMove; } }

        public KingChess() {
            IsFirstMove = _isFirstMove;
            IsCheck = _isCheck;
            _isCastlingMove = new int[] { -1, -1 , -1 ,-1 };
        }

        public override bool[,] PossibleMove()
        {
            bool[,] r = new bool[8, 8];

            ChessBehaviour c;
            int i, j;

            //top 
            i = CurrentX - 1;
            j = CurrentY + 1;
            if (CurrentY != 7)
            {
                for (int k =0; k< 3; k++) {

                    if (i>=0 && i<8) {

                        c = ChessBoarderManager.Instance.ChessTable[i, j];
                        if (c == null) {
                            r[i, j] = true;

                        } else if (isWhite != c.isWhite) {
                            r[i, j] = true;
                        }
                    }
                    i++;
                }
            }

            // down 
            i = CurrentX - 1;
            j = CurrentY - 1;
            if (CurrentY != 0)
            {
                for (int k = 0; k < 3; k++)
                {

                    if (i >= 0 && i < 8)
                    {

                        c = ChessBoarderManager.Instance.ChessTable[i, j];
                        if (c == null)
                        {
                            r[i, j] = true;


                        }
                        else if (isWhite != c.isWhite)
                        {
                            r[i, j] = true;
                        }
                    }
                    i++;
                }
            }

            // middle left 
            if (CurrentX != 0)
            {
                c = ChessBoarderManager.Instance.ChessTable[CurrentX -1, CurrentY];
                if (c == null)
                {
                    r[CurrentX -1, CurrentY] = true;
                }
                else if (isWhite != c.isWhite)
                {
                    r[CurrentX - 1, CurrentY] = true;
                }
            }

            // middle left 
            if (CurrentX != 7)
            {
                c = ChessBoarderManager.Instance.ChessTable[CurrentX + 1, CurrentY];
                if (c == null)
                {
                    r[CurrentX + 1, CurrentY] = true;
                }
                else if (isWhite != c.isWhite)
                {
                    r[CurrentX + 1, CurrentY] = true;
                }
            }

            // checking for casting movement

            if (isWhite)
            {
                //white  left side 
                RookChess rookLeft = (RookChess)ChessBoarderManager.Instance.ChessTable[7, 0];

                //rule 1: validate first movement
                if (IsFirstMove && rookLeft != null && !rookLeft.IsHasMove)
                {

                    //rule 2: There are no pieces between the king and the rook.
                    ChessBehaviour p1 = ChessBoarderManager.Instance.ChessTable[6, 0];
                    ChessBehaviour p2 = ChessBoarderManager.Instance.ChessTable[5, 0];
                    if (p1 == null && p2 == null)
                    {
                        r[CurrentX +2, CurrentY] = true;
                        _isCastlingMove[0] = CurrentX + 2;
                        _isCastlingMove[1] = CurrentY;
                    }
                }

                rookLeft = (RookChess)ChessBoarderManager.Instance.ChessTable[0, 0];

                //white right side 
                if (IsFirstMove && rookLeft != null && !rookLeft.IsHasMove)
                {
                    //rule 2: There are no pieces between the king and the rook.
                    ChessBehaviour p1 = ChessBoarderManager.Instance.ChessTable[1, 0];
                    ChessBehaviour p2 = ChessBoarderManager.Instance.ChessTable[2, 0];
                    ChessBehaviour p3 = ChessBoarderManager.Instance.ChessTable[3, 0];

                    if (p1 == null && p2 == null && p3 == null)
                    {
                        r[CurrentX-2, CurrentY] = true;
                        _isCastlingMove[0] = CurrentX - 2;
                        _isCastlingMove[1] = CurrentY;
                    }
                }
            }
            else
            {

                //black left side 
                RookChess rookLeft = (RookChess)ChessBoarderManager.Instance.ChessTable[7, 7];

                //rule 1: validate first movement
                if (IsFirstMove && rookLeft != null && !rookLeft.IsHasMove)
                {

                    //rule 2: There are no pieces between the king and the rook.
                    ChessBehaviour p1 = ChessBoarderManager.Instance.ChessTable[6, 7];
                    ChessBehaviour p2 = ChessBoarderManager.Instance.ChessTable[5, 7];
                    if (p1 == null && p2 == null)
                    {
                        r[CurrentX+2, CurrentY] = true;
                        _isCastlingMove[0] = CurrentX+2;
                        _isCastlingMove[1] = CurrentY;
                    }
                }

                //black right side 
                rookLeft = (RookChess)ChessBoarderManager.Instance.ChessTable[0, 7];

                //rule 1: validate first movement
                if (IsFirstMove && rookLeft != null && !rookLeft.IsHasMove)
                {

                    //rule 2: There are no pieces between the king and the rook.
                    ChessBehaviour p1 = ChessBoarderManager.Instance.ChessTable[1, 7];
                    ChessBehaviour p2 = ChessBoarderManager.Instance.ChessTable[2, 7];
                    ChessBehaviour p3 = ChessBoarderManager.Instance.ChessTable[3, 7];

                    if (p1 == null && p2 == null && p3 == null)
                    {
                        r[CurrentX -2, CurrentY] = true;
                        _isCastlingMove[0] = CurrentX - 2;
                        _isCastlingMove[1] = CurrentY;
                    }
                }

            }
            return r;
        }
    }
}