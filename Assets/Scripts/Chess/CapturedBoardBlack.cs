using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CapturedBoardBlack : MonoBehaviour
{
    public static CapturedBoardBlack Instance { get; set; }

    [Tooltip("start position of grid")]
    [SerializeField] private Transform startPoint;

    [SerializeField] private Transform[] capturePiecePosList;

    public Stack<GameObject> blackPieces { get; set; }

    private int spawnIndex = 0;

    private void Awake()
    {
        Instance = this;
        blackPieces = new Stack<GameObject>();
    }

    public void AddCaturePiece(GameObject captured) {

        blackPieces.Push(captured);

        if (captured.tag =="Pawn") {
            Debug.Log("Capture PawnChess");
            GameObject toSpawn = blackPieces.Peek();
            SpawnCapturePiece(toSpawn);
        }

        if (captured.tag == "Queen")
        {
            Debug.Log("Capture Queen");
            GameObject toSpawn = blackPieces.Peek();
            SpawnCapturePiece(toSpawn);
        }

        if (captured.tag == "Rook")
        {
            Debug.Log("Capture Rook");
            GameObject toSpawn = blackPieces.Peek();
            SpawnCapturePiece(toSpawn);
        }

        if (captured.tag == "Knight")
        {
            Debug.Log("Capture Knight");
            GameObject toSpawn = blackPieces.Peek();
            SpawnCapturePiece(toSpawn);
        }

        if (captured.tag == "Bishop")
        {
            Debug.Log("Capture Bishop");
            GameObject toSpawn = blackPieces.Peek();
            SpawnCapturePiece(toSpawn);
        }

        spawnIndex++;
    }

    private void Update()
    {
        DrawCapureBlackPiece();

        DebugPosition();
    }

    private void DrawCapureBlackPiece() 
    {
        

        Vector3 widthLine = Vector3.right * 2;
        Vector3 heigthLine = Vector3.back * 5;

        for (int i = 5; i >= 0; i--)
        {
            Vector3 start = startPoint.position;
            start = start + Vector3.back * i;
            Debug.DrawLine(start, start + widthLine);
        }

        for (int j = 0; j <= 2; j++)
        {
            Vector3 start = startPoint.position;
            start = start + Vector3.right * j;
            Debug.DrawLine(start, start + heigthLine);

        }

        //draw the selection

        //if (selectionX >= 0 && seletionY >= 0)
        //{

        //    Debug.DrawLine(Vector3.forward * seletionY + Vector3.right * selectionX,
        //         Vector3.forward * (seletionY + 1) + Vector3.right * (selectionX + 1));

        //    Debug.DrawLine(Vector3.forward * (seletionY + 1) + Vector3.right * selectionX,
        //        Vector3.forward * seletionY + Vector3.right * (selectionX + 1));

        //}

    }

    private void SpawnCapturePiece(GameObject obj) {

        if (spawnIndex <= 15)
        {

            Instantiate(obj, capturePiecePosList[spawnIndex].position, Quaternion.identity, transform);

        }
    }


    private void DebugPosition() {


        if (Input.GetMouseButtonDown(0)) {

            RaycastHit hit;


            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f))
            {
                int selectionX = (int) hit.point.x;
                int seletionY = (int)hit.point.z;
                int seletionZ = (int)hit.point.y;

            }

        }
    }

}
