 namespace Assets.Scripts
{
    public class RookChess : ChessBehaviour
    {

        private bool _isHasMove = false;

        public bool IsHasMove { get; set; }

        public RookChess() {
            IsHasMove = _isHasMove;
        }

        public override bool[,] PossibleMove()
        {
            bool[,] r = new bool[8, 8];

            ChessBehaviour c;
            int i;

            //right
            i = CurrentX;
            while (true) {
                i++;
                if (i >= 8) {
                    break;
                }

                c = ChessBoarderManager.Instance.ChessTable[i, CurrentY];

                if (c == null)
                {
                    r[i, CurrentY] = true;
                }
                else 
                {
                    if (c.isWhite != isWhite) {
                        r[i, CurrentY] = true;
                    }
                    break;
                
                }
            }

            //left 

            i = CurrentX;
            while (true)
            {
                i--;
                if (i < 0)
                {
                    break;
                }

                c = ChessBoarderManager.Instance.ChessTable[i, CurrentY];

                if (c == null)
                {
                    r[i, CurrentY] = true;
                }
                else
                {
                    if (c.isWhite != isWhite)
                    {
                        r[i, CurrentY] = true;
                    }
                    break;

                }
            }

            //Up 
            i = CurrentY;
            while (true)
            {
                i++;
                if (i >= 8)
                {
                    break;
                }

                c = ChessBoarderManager.Instance.ChessTable[CurrentX,i];

                if (c == null)
                {
                    r[CurrentX, i] = true;
                }
                else
                {
                    if (c.isWhite != isWhite)
                    {
                        r[CurrentX, i] = true;
                    }
                    break;

                }
            }

            //down 
            i = CurrentY;
            while (true)
            {
                i--;
                if (i < 0)
                {
                    break;
                }

                c = ChessBoarderManager.Instance.ChessTable[CurrentX, i];

                if (c == null)
                {
                    r[CurrentX, i] = true;
                }
                else
                {
                    if (c.isWhite != isWhite)
                    {
                        r[CurrentX, i] = true;
                    }
                    break;

                }
            }
            return r; 
        }
    }
}