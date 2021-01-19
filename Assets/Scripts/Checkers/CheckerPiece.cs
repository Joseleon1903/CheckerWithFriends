using HighlightingSystem;
using System;
using UnityEngine;

namespace Assets.Scripts.Checkers
{
    public struct Position {
        public int x;
        public int y;
    }

    public class CheckerPiece : MonoBehaviour
    {
        private Highlighter highlighter;

        public bool isWhite;
        public bool isKing;

        private void Awake()
        {
            highlighter = GetComponent<Highlighter>();
        }

        private void Update()
        {
            highlighter.ConstantOff();
        }

        public void EnableHightLight() {

            highlighter.On(Color.blue);
        }

        public bool IsForceToMove(CheckerPiece[,] board, int x , int y ) {

            //white team
            if (isWhite || isKing)
            {

                //Top left
                if (x >= 2 && y <= 5)
                {

                    CheckerPiece p = board[x - 1, y + 1];

                    if (p != null && p.isWhite != isWhite)
                    {

                        //check if its possible to land after the jump
                        if (board[x - 2, y + 2] == null)
                            return true;
                    }
                }

                //Top right
                if (x <= 5 && y <= 5)
                {

                    CheckerPiece p = board[x + 1, y + 1];

                    if (p != null && p.isWhite != isWhite)
                    {

                        //check if its possible to land after the jump
                        if (board[x + 2, y + 2] == null)
                            return true;
                    }
                }
            }
            
            //black team
            if(!isWhite || isKing)
            {
                //Botton left
                if (x >= 2 && y >= 2)
                {

                    CheckerPiece p = board[x - 1, y - 1];

                    if (p != null && p.isWhite != isWhite)
                    {

                        //check if its possible to land after the jump
                        if (board[x - 2, y - 2] == null)
                            return true;
                    }
                }

                //Botton right
                if (x <= 5 && y >= 2)
                {

                    CheckerPiece p = board[x + 1, y - 1];

                    if (p != null && p.isWhite != isWhite)
                    {

                        //check if its possible to land after the jump
                        if (board[x + 2, y - 2] == null)
                            return true;
                    }
                }
            }

            return false;
        }

        public bool[,] PossibleMove(CheckerPiece[,] board, int x, int y)
        {
            bool[,] tiles = new bool[8, 8];

            Debug.Log("Curretn piece X: "+ x);
            Debug.Log("Curretn piece Y: " + y);

            Position posNextL;
            Position posNextR;

            //posible move for white piece
            if (isWhite || isKing) {

                posNextL.x = x + 1;
                posNextL.y = y + 1;

                posNextR.x = x - 1;
                posNextR.y = y + 1;

                if (posNextL.x < 8 && posNextL.x > -1 && posNextL.y < 8 && posNextL.y > -1)
                {
                    bool r = (board[posNextL.x, posNextL.y] == null) ? true : false;
                    tiles[posNextL.x, posNextL.y] = r;
                }

                if (posNextR.x < 8 && posNextR.x > -1 && posNextR.y < 8 && posNextR.y > -1)
                {
                    bool r = (board[posNextR.x, posNextR.y] == null) ? true : false;
                    tiles[posNextR.x, posNextR.y] = r;
                }
            }

            //posible move for black piece
            if (!isWhite || isKing) {

                posNextL.x = x + 1;
                posNextL.y = y - 1;

                posNextR.x = x - 1;
                posNextR.y = y - 1;

                if (posNextL.x < 8 && posNextL.x > -1 && posNextL.y < 8 && posNextL.y > -1)
                {
                    bool r = (board[posNextL.x, posNextL.y] == null) ? true : false;
                    tiles[posNextL.x, posNextL.y] = r;
                }

                if (posNextR.x < 8 && posNextR.x > -1 && posNextR.y < 8 && posNextR.y > -1)
                {
                    bool r = (board[posNextR.x, posNextR.y] == null) ? true : false;
                    tiles[posNextR.x, posNextR.y] = r;
                }

            }
            return tiles;
        }

        public bool ValidMove(CheckerPiece[,] board, int x1, int y1, int x2, int y2)
        {

            //if you are on top oaf another piece

            if (board[x2, y2] != null)
            {
                return false;
            }

            // for white piece 
            if (isWhite)
            {

                if (y2 == (y1 + 1) && (x2 == (x1 - 1) || x2 == (x1 + 1)))
                {
                    return true;
                }

                int deltaMoveY = y2 - y1;
                if (deltaMoveY == 2)
                {
                    CheckerPiece p = board[(x1 + x2) / 2, (y1 + y2) / 2];
                    if (p != null && p.isWhite != isWhite)
                    {
                        return true;
                    }
                }
            }


            // for black piece 
            if (!isWhite)
            {
                if (y2 == (y1 - 1) && (x2 == (x1 - 1) || x2 == (x1 + 1)))
                {
                    return true;
                }

                int deltaMoveY = Math.Abs(y2 - y1);
                if (deltaMoveY == 2)
                {
                    CheckerPiece p = board[(x1 + x2) / 2, (y1 + y2) / 2];
                    if (p != null && p.isWhite != isWhite)
                    {
                        return true;
                    }
                }
            }

            //king move
            if (isKing)
            {

                if (y2 == (y1 + 1) || y2 == (y1 - 1) && x2 == (x1 + 1) || x2 == (x1 - 1))
                {
                    return true;
                }

                int deltaMoveY = Math.Abs(y2 - y1);
                if (deltaMoveY == 2)
                {
                    CheckerPiece p = board[(x1 + x2) / 2, (y1 + y2) / 2];
                    if (p != null && p.isWhite != isWhite)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool[,] ValidCapturePiece(CheckerPiece[,] board, int x, int y)
        {
            bool[,] tiles = new bool[8, 8];

            Debug.Log("Curretn piece X: " + x);
            Debug.Log("Curretn piece Y: " + y);

            Position posNextL;
            Position posNextR;

            //posible capture move for white piece
            if (isWhite || isKing)
            {

                posNextL.x = x + 1;
                posNextL.y = y + 1;

                posNextR.x = x - 1;
                posNextR.y = y + 1;

                if (posNextL.x < 8 && posNextL.x > -1 && posNextL.y < 8 && posNextL.y > -1)
                {
                    bool r = (board[posNextL.x, posNextL.y] != null && board[posNextL.x, posNextL.y].isWhite != isWhite) ? true : false;

                    int captureX, captureY;
                    captureX = posNextL.x + 1;
                    captureY = posNextL.y + 1;

                    if (r && captureX < 8 && captureX > -1 && captureY < 8 && captureY > -1 && board[captureX, captureY] == null)
                    {
                        tiles[posNextL.x, posNextL.y] = r;
                    }
                }

                if (posNextR.x < 8 && posNextR.x > -1 && posNextR.y < 8 && posNextR.y > -1)
                {
                    bool r = (board[posNextR.x, posNextR.y] != null && board[posNextR.x, posNextR.y].isWhite != isWhite) ? true : false;

                    int captureX, captureY;
                    captureX = posNextR.x - 1;
                    captureY = posNextR.y + 1;

                    if (r && captureX < 8 && captureX > -1 && captureY < 8 && captureY > -1 && board[captureX, captureY] == null)
                    {
                        tiles[posNextR.x, posNextR.y] = r;
                    }
                }
            }

            //posible capture move for black piece
            if (!isWhite || isKing)
            {

                posNextL.x = x + 1;
                posNextL.y = y - 1;

                posNextR.x = x - 1;
                posNextR.y = y - 1;

                if (posNextL.x < 8 && posNextL.x > -1 && posNextL.y < 8 && posNextL.y > -1)
                {
                    bool r = (board[posNextL.x, posNextL.y] != null  && board[posNextL.x, posNextL.y].isWhite != isWhite) ? true : false;

                    int captureX, captureY;
                    captureX = posNextL.x + 1;
                    captureY = posNextL.y -1;

                    if (r && captureX < 8 && captureX > -1 && captureY < 8 && captureY > -1 && board[captureX, captureY] == null)
                    {
                        tiles[posNextL.x, posNextL.y] = r;
                    }
                }

                if (posNextR.x < 8 && posNextR.x > -1 && posNextR.y < 8 && posNextR.y > -1)
                {
                    bool r = (board[posNextR.x, posNextR.y] != null && board[posNextR.x, posNextR.y].isWhite != isWhite) ? true : false;

                    int captureX, captureY;

                    captureX = posNextR.x - 1;
                    captureY = posNextL.y - 1;
                    if (r && captureX < 8 && captureX > -1 && captureY < 8 && captureY > -1 && board[captureX, captureY] == null) {
                        tiles[posNextR.x, posNextR.y] = r;
                    }
                }
            }

            return tiles;
        }

        public bool HasValidMove(CheckerPiece[,] board) {

            int currentX = (int)(transform.position.x - 0.5f);
            int currentY = (int)(transform.position.z - 0.5f);

            bool[,] possibleMove = PossibleMove(board, currentX, currentY);

            int moveCount = 0;

            foreach (bool b in possibleMove) {

                if (b) {
                    moveCount++;
                }
            }

            return (moveCount > 0);
        }

    }
}