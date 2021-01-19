namespace Assets.Scripts
{
    public class QueenChess : ChessBehaviour
    {
        public override bool[,] PossibleMove()
        {

            bool[,] r = new bool[8, 8];

            ChessBehaviour c;
            int i, j;

            //-------  Same of Rook
            //right
            i = CurrentX;
            while (true)
            {
                i++;
                if (i >= 8)
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


            //-------  Same of Bishop
            //top left 
            i = CurrentX;
            j = CurrentY;
            while (true)
            {
                i--;
                j++;
                if (i < 0 || j >= 8) { break; }

                c = ChessBoarderManager.Instance.ChessTable[i, j];

                if (c == null)
                {
                    r[i, j] = true;
                }
                else
                {

                    if (isWhite != c.isWhite)
                    {
                        r[i, j] = true;
                    }
                    break;
                }
            }

            //top right 
            i = CurrentX;
            j = CurrentY;
            while (true)
            {
                i++;
                j++;
                if (i >= 8 || j >= 8) { break; }

                c = ChessBoarderManager.Instance.ChessTable[i, j];

                if (c == null)
                {
                    r[i, j] = true;
                }
                else
                {

                    if (isWhite != c.isWhite)
                    {
                        r[i, j] = true;
                    }
                    break;
                }
            }

            //down left 
            i = CurrentX;
            j = CurrentY;
            while (true)
            {
                i--;
                j--;
                if (i < 0 || j < 0) { break; }

                c = ChessBoarderManager.Instance.ChessTable[i, j];

                if (c == null)
                {
                    r[i, j] = true;
                }
                else
                {

                    if (isWhite != c.isWhite)
                    {
                        r[i, j] = true;
                    }
                    break;
                }
            }

            //down right 
            i = CurrentX;
            j = CurrentY;
            while (true)
            {
                i++;
                j--;
                if (i >= 8 || j < 0) { break; }

                c = ChessBoarderManager.Instance.ChessTable[i, j];

                if (c == null)
                {
                    r[i, j] = true;
                }
                else
                {

                    if (isWhite != c.isWhite)
                    {
                        r[i, j] = true;
                    }
                    break;
                }
            }

            return r;

        }

      
    }
}