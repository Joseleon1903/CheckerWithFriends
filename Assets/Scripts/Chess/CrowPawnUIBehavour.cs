using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket;
using Assets.Scripts.WebSocket.Message;
using UnityEngine;

namespace Assets.Scripts.Chess
{
    public struct SpawnPosition {
        public int PosX;
        public int PosY;
    }

    enum PieceTypeNumber { 
    
        Rook = 1,
        Knight = 2,
        Bishop= 3,
        Queen = 4
    }

    class CrowPawnUIBehavour : MonoBehaviour
    {

        public GCPlayer Player { get; set; }
        public SpawnPosition SpawnPositionPiece {get;set;}

        public bool IsWhitePlayer { get; set; }

        public void SpawnSelectedPiece(int selectedType) {

            PlayerType type = (ChessBoarderManager.Instance.isWhiteTurn) ? PlayerType.P1 : PlayerType.P2;

            if (OnlinePlayerBehavour.Instance.isOnlineGame) {
                type = OnlinePlayerBehavour.Instance.playerType;
            }

            int prefabsindex = GetPrefacabPosition(selectedType);

            ChessBoarderManager.Instance.ChessBoardPromotedPiece(type, prefabsindex, SpawnPositionPiece);

        }

        private int GetPrefacabPosition(int selectedType)
        {
            int index = 0;

            switch (selectedType) {

                case 1:
                    if (IsWhitePlayer)
                    {
                        index = 4;
                    }
                    else
                    {
                        index = 10;
                    }
                    break;

                case 2:
                    if (IsWhitePlayer)
                    {
                        index = 3;
                    }
                    else
                    {
                        index = 9;
                    }
                    break;
                case 3:
                    if (IsWhitePlayer)
                    {
                        index = 2;
                    }
                    else
                    {
                        index = 8;
                    }
                    break;

                case 4:
                    if (IsWhitePlayer)
                    {
                        index = 1;
                    }
                    else
                    {
                        index = 7;
                    }
                    break;
            }
            return index;
        }
    }
}
