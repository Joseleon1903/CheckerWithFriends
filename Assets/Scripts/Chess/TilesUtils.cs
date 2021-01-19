using UnityEngine;

namespace Assets.Scripts
{
    public static class TilesUtils 
    {
        private const float TILE_SIZE = 1.0f;

        private const float TILE_OFFSET = 0.5f;

        public static Vector3 GetTileCenter(int x , int y) {
            Vector3 origin = Vector3.zero;
            origin.x += (TILE_SIZE * x) + TILE_OFFSET;
            origin.z += (TILE_SIZE * y) + TILE_OFFSET;
            return origin;
        }

        public static Vector3 GetCaptureTileCenter(float x, float y,  float z)
        {
            Vector3 origin = Vector3.zero;
            origin.x = TILE_OFFSET+x;
            origin.y = y;
            origin.z = TILE_OFFSET +z;
            return origin;
        }


        public static string TrasnlatePieceGameName(string name) {

            string firstletter = name[0].ToString().ToUpper();

            string nameOut = "";
            switch (firstletter)
            {
                case "P":
                    nameOut = "Pawn";
                    break;

                case "R":
                    nameOut = "Rook";
                    break;

                case "Q":
                    nameOut = "Queen";
                    break;

                case "K":
                    nameOut = "Knight";
                    break;

                case "B":
                    nameOut = "Bishop";
                    break;
            }
            return nameOut;
        }

    }
}