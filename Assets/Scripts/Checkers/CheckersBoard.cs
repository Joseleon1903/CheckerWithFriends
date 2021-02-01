using Assets.Scripts.Checkers;
using Assets.Scripts.General;
using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket;
using System.Collections.Generic;
using UnityEngine;

class CheckersBoard : Singleton<CheckersBoard>
{

    public CheckerPiece[,] pieces = new CheckerPiece[8, 8];

    [Tooltip("Player White Piece")]
    [SerializeField] private GameObject whitePiecePrefab;

    [Tooltip("Player Black Piece")]
    [SerializeField] private GameObject blackPiecePrefab;

    [Tooltip("Player White Queen Piece")]
    [SerializeField] private GameObject whiteQueenPiecePrefab;

    [Tooltip("Player Black Queen Piece")]
    [SerializeField] private GameObject blackQueenPiecePrefab;

    public Vector3 pieceOffSet = new Vector3(0.5f, 0.1f, 0.5f);

    public bool isWhiteTurn;
    public bool isWhite;

    public CheckerPiece SelectedPiece { get; set; }

    private List<CheckerPiece> forcedPieces;

    private ClientWSBehavour client;

    private void Start()
    {
        client = FindObjectOfType<ClientWSBehavour>();
        isWhite = (client != null) ? client.profile.isHost : true;

        isWhiteTurn = true;
        forcedPieces = new List<CheckerPiece>();
        GeneratedBoard();
    }

    private void Update()
    {
        //DrawDebugBoard();

        //UpdateMouseOver();

        //// if it is my turn 
        //if((isWhite) ? isWhiteTurn : !isWhiteTurn)
        //{
        //    int x = (int)mouseover.x;
        //    int y = (int)mouseover.y;

        //    if (selectedPiece != null) 
        //    {
        //        UpdatePieceDrag(selectedPiece);
        //    }


        //    if (Input.GetMouseButtonDown(0)) 
        //    {
        //        // Debug.Log($"Down x:{x} y: {y}");

        //        SelectPiece(x, y);
        //    }

        //    if (Input.GetMouseButtonUp(0)) 
        //    {
        //        // Debug.Log($"Up x:{x} y: {y}");

        //        TryMove((int)startDrag.x, (int)startDrag.y, x, y);
        //    }

        //    if (selectedPiece !=  null) {
        //        // highLight CheckerPiece
        //        selectedPiece.EnableHightLight();
        //    }
        //}
    }

    public void SelectPiece(int x, int y)
    {
        CheckerPiece p = pieces[x, y];

        if (p != null && p.isWhite == isWhite)
        {
            forcedPieces = ScanForPossibleMove(x,y);

            if (forcedPieces.Count == 0)
            {
                SelectedPiece = p;

                //hightlight piece posible move 
                bool[,] lightTiles = SelectedPiece.PossibleMove(pieces, x, y);

                HightLightTiled.Instance.HightLight(lightTiles);

            }
            else {
                //look for the piece under forces pieces list

                if (forcedPieces.Find(fp=> fp == p ) ==  null)
                    return;

                SelectedPiece = p;
            }              
        }
    }

    public bool TryMove(int x1, int y1, int x2, int y2)
    {
        //multiplayer support
        SelectedPiece = pieces[x1, y1];

        if (SelectedPiece == null) 
        {
            return false;
        }

        MovePiece(SelectedPiece.gameObject, x2, y2);
        pieces[x1, y1] = null;
        pieces[x2, y2] = SelectedPiece;
        HightLightTiled.Instance.HideHightLight();

        //if this is a jump
        if (Mathf.Abs(x2 - x1) == 2)
        {
            CheckerPiece p = pieces[(x1 + x2) / 2, (y1 + y2) / 2];
            if (p != null)
            {
                pieces[(x1 + x2) / 2, (y1 + y2) / 2] = null;
                DestroyImmediate(p.gameObject);
            }
            HightLightTiled.Instance.HideCaptureHightLight();
            // validate if player can stil eat
            forcedPieces = ScanForPossibleMove();
        }

        //promotion piece 
        if (SelectedPiece != null)
        {
            if (SelectedPiece.isWhite && !SelectedPiece.isKing && y2 == 7)
            {
                DestroyImmediate(SelectedPiece.gameObject);
                GeneratePiece(whiteQueenPiecePrefab, x2, y2);
            }
            else if (!SelectedPiece.isWhite && !SelectedPiece.isKing && y2 == 0)
            {
                DestroyImmediate(SelectedPiece.gameObject);
                GeneratePiece(blackQueenPiecePrefab, x2, y2);
            }
        }
       
        Debug.Log("forcedPieces Count " + forcedPieces.Count);
        if (forcedPieces.Count > 0)
        {
            return false;
        }

        SelectedPiece = null;

        return true;
    }

    private void EndTurn()
    { 
        //send movment to the server
        if (client != null) 
        {
            string lobbyCode = client.profile.lobbyCode;
            string boolean = EnumHelper.FALSE;
            //DataMessageReq dataReq = new DataMessageReq(lobbyCode, GameType.CHECKER.ToString().ToUpper(), boolean, 
            //    startDrag.x.ToString(), startDrag.y.ToString(), endDrag.x.ToString(),endDrag.y.ToString());

            //client.Send(dataReq.GetMessageText());
        }

        HightLightTiled.Instance.HideCaptureHightLight();

        //if (ScanForPossibleMove(x, y).Count != 0 && haskilled)
        //{
        //    HightLightTiled.Instance.CaptureHightLight(selectedPiece.ValidCapturePiece(pieces, (int)endDrag.x, (int)endDrag.y));
        //    selectedPiece = null;
        //    return;
        //}

        SelectedPiece = null;

        isWhiteTurn = !isWhiteTurn;
    }

    public void CheckVictory()
    {
        bool hasWhite = true, hasBlack = true;

        //check black player 2 winner
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (pieces[i, j] != null && pieces[i, j].isWhite && (pieces[i, j].HasValidMove(pieces) 
                    || pieces[i, j].IsForceToMove(pieces, i, j)))
                {
                    hasWhite = false;
                }
            }
        }

        if (hasWhite)
        {
            CheckerGameManager.Instance.GameState.Checkmate(PlayerType.P2);
        }

        //check White player 1 winner
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (pieces[i, j] != null && !pieces[i, j].isWhite && (pieces[i, j].HasValidMove(pieces)
                    || pieces[i, j].IsForceToMove(pieces, i, j)))
                {
                    hasBlack = false;
                }
            }
        }

        if (hasBlack)
        {
            CheckerGameManager.Instance.GameState.Checkmate(PlayerType.P1);
        }

    }

    private List<CheckerPiece> ScanForPossibleMove(int x, int y) {
        bool[,]  tiles = pieces[x, y].PossibleEatMove(pieces, x, y);
        HightLightTiled.Instance.HightLight(tiles);
        return ScanForPossibleMove();
    }

    private List<CheckerPiece> ScanForPossibleMove() {

        forcedPieces = new List<CheckerPiece>();
        //check all pieces
        for (int i =0; i < 8; i ++) 
        {
            for (int j =0; j < 8; j++) 
            {
                if (pieces[i,j] != null && pieces[i, j].isWhite == isWhiteTurn) 
                {
                    if (pieces[i, j].IsForceToMove(pieces, i, j)) {

                        HightLightTiled.Instance.CaptureHightLight(pieces[i, j].ValidCapturePiece(pieces, i,j));
                        forcedPieces.Add(pieces[i, j]);
                    }
                }
            }
        }
        return forcedPieces;
    }

    private void GeneratedBoard() {

        //Generate white team
        for (int y = 0; y < 3; y++)
        {
            bool addRow = (y % 2 == 0);
            // x < 8
            for (int x = 0; x <8 ; x+=2)
            {
                GeneratePiece((addRow)? x: x+1, y);
            }
        }

        //Generate black team
        for (int y = 7; y > 4; y--)
        {
            bool addRow = (y % 2 == 0);
            // x < 8
            for (int x = 0; x < 8; x += 2)
            {
                GeneratePiece((addRow) ? x : x + 1, y);
            }
        }
    }

    private void MovePiece(GameObject p, int x, int y) 
    {
        p.transform.position = (Vector3.right * x) + (Vector3.forward * y) + pieceOffSet;
    }

    private void GeneratePiece(int x, int y)
    {
        bool isWhitePiece = (y > 3) ? false : true;
        GameObject go = Instantiate(isWhitePiece ? whitePiecePrefab : blackPiecePrefab) as GameObject;
        go.transform.SetParent(transform);
        CheckerPiece p = go.GetComponent<CheckerPiece>();
        pieces[x, y] = p;
        MovePiece(go, x, y);
    }

    private void GeneratePiece(GameObject prefab, int x, int y)
    {
        GameObject go = Instantiate(prefab);
        go.transform.SetParent(transform);
        CheckerPiece p = go.GetComponent<CheckerPiece>();
        pieces[x, y] = p;
        MovePiece(go, x, y);
    }

    public void ShowAlertPlayerTurn(PlayerType type) {

        if (type.Equals(PlayerType.P1))
        {
           CanvasManagerUI.Instance.ShowAlertText("It's White player turn..");
        }
        else {
            CanvasManagerUI.Instance.ShowAlertText("It's Black player turn..");
        }
    
    }

    private void DrawDebugBoard()
    {
        Vector3 startPoint = Vector3.zero;
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heigthLine = Vector3.forward * 8;

        for (int i = 0; i <= 8; i++)
        {
            Vector3 start = startPoint;
            start = start + Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
        }

        for (int j = 0; j <= 8; j++)
        {
            Vector3 start = startPoint;
            start = start + Vector3.right * j;
            Debug.DrawLine(start, start + heigthLine);
        }
    }

}

