namespace Assets.Scripts
{
    public class BishopChess : ChessBehaviour
    {

        public override bool[,] PossibleMove()
        {
            bool[,] r = new bool[8, 8];

            ChessBehaviour c;
            int i, j;

            //top left 
            i = CurrentX;
            j = CurrentY;
            while (true) {
                i--;
                j++;
                if (i < 0 || j >= 8) { break; }

                c = ChessBoarderManager.Instance.ChessTable[i, j];

                if (c == null)
                {
                    r[i, j] = true;
                }
                else {

                    if (isWhite != c.isWhite) {
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
                if (i >= 8 || j <0 ) { break; }

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