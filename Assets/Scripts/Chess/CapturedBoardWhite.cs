using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [ExecuteAlways]
    public class CapturedBoardWhite : MonoBehaviour
    {
        public static CapturedBoardWhite Instance { get; set; }

        [SerializeField] private Transform startPointAnchor;

        [SerializeField] private Transform[] capturePiecePosList;

        [SerializeField] private Stack<GameObject> whitePieces { get; set; }

        private int spawnIndex = 0;

        private void Awake()
        {
            Instance = this;
            whitePieces = new Stack<GameObject>();
        }

        public void AddCaturePiece(GameObject captured)
        {
            whitePieces.Push(captured);

            if (captured.tag == "Pawn")
            {
                Debug.Log("Capture PawnChess");
                GameObject toSpawn = whitePieces.Peek();
                SpawnCapturePiece(toSpawn);
            }

            if (captured.tag == "Queen")
            {
                Debug.Log("Capture Queen");
                GameObject toSpawn = whitePieces.Peek();
                SpawnCapturePiece(toSpawn);
            }

            if (captured.tag == "Rook")
            {
                Debug.Log("Capture Rook");
                GameObject toSpawn = whitePieces.Peek();
                SpawnCapturePiece(toSpawn);
            }

            if (captured.tag == "Knight")
            {
                Debug.Log("Capture Knight");
                GameObject toSpawn = whitePieces.Peek();
                SpawnCapturePiece(toSpawn);
            }

            if (captured.tag == "Bishop")
            {
                Debug.Log("Capture Bishop");
                GameObject toSpawn = whitePieces.Peek();
                SpawnCapturePiece(toSpawn);
            }

            spawnIndex++;
        }

        private void SpawnCapturePiece(GameObject obj)
        {
            if (spawnIndex <=15) {

                Instantiate(obj, capturePiecePosList[spawnIndex].position, Quaternion.identity, transform);

            }
        }

        private void Update()
        {
            DrawCapureBlackPiece();

            DebugPosition();
        }

        private void DebugPosition()
        {

            if (Input.GetMouseButtonDown(0))
            {

                RaycastHit hit;


                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f))
                {
                    int selectionX = (int)hit.point.x;
                    int seletionY = (int)hit.point.z;
                    int seletionZ = (int)hit.point.y;

                    Debug.Log("Selection x-y-z" + selectionX + " : " + seletionY + " : " + seletionZ);
                }

            }
        }

        private void DrawCapureBlackPiece()
        {

            Vector3 widthLine = Vector3.forward * 5;
            Vector3 heigthLine = Vector3.left * 2;

            for (int i = 0; i <= 5; i++)
            {
                Vector3 start = startPointAnchor.position;
                start = start + Vector3.forward * i;
                Debug.DrawLine(start, start + heigthLine);
            }

            for (int j = 0; j <= 2; j++)
            {
                Vector3 start = startPointAnchor.position;
                start = start + Vector3.left * j;
                Debug.DrawLine(start, start + widthLine);
            }

        }

    }
}