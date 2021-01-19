using Assets.Scripts.Checkers;
using Assets.Scripts.Chess;
using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket;
using Assets.Scripts.WebSocket.Message;
using System.Collections.Generic;
using UnityEngine;

class CheckersBoard : MonoBehaviour
{
    public static CheckersBoard Instance;

    public CheckerPiece[,] pieces = new CheckerPiece[8, 8];
    public GameObject whitePiecePrefab;
    public GameObject blackPiecePrefab;

    public Vector3 pieceOffSet = new Vector3(0.5f, 0.1f, 0.5f);

    private Vector2 mouseover;
    private Vector2 startDrag;
    private Vector2 endDrag;

    private bool isWhiteTurn;
    public bool isWhite;

    private CheckerPiece selectedPiece;
    private List<CheckerPiece> forcedPieces;
    private bool haskilled;

    private ClientWSBehavour client;

    private void Start()
    {
        Instance = this;

        client = FindObjectOfType<ClientWSBehavour>();
        isWhite = (client != null) ? client.profile.isHost : true;

        isWhiteTurn = true;
        forcedPieces = new List<CheckerPiece>();
        GeneratedBoard();

    }

    private void Update()
    {
        DrawDebugBoard();

        UpdateMouseOver();

        // if it is my turn 
        if((isWhite) ? isWhiteTurn : !isWhiteTurn)
        {
            int x = (int)mouseover.x;
            int y = (int)mouseover.y;

            if (selectedPiece != null) 
            {
                UpdatePieceDrag(selectedPiece);
            }


            if (Input.GetMouseButtonDown(0)) 
            {
                // Debug.Log($"Down x:{x} y: {y}");

                SelectPiece(x, y);
            }

            if (Input.GetMouseButtonUp(0)) 
            {
                // Debug.Log($"Up x:{x} y: {y}");

                TryMove((int)startDrag.x, (int)startDrag.y, x, y);
            }

            if (selectedPiece !=  null) {
                // highLight CheckerPiece
                selectedPiece.EnableHightLight();
            }
        }
    }

    private void SelectPiece(int x, int y)
    {
        //out of bound
        if (x < 0 || x >= pieces.Length || y < 0 || y >= pieces.Length)
        {
            return;
        }

        CheckerPiece p = pieces[x, y];

        if (p != null && p.isWhite == isWhite)
        {
            if (forcedPieces.Count == 0)
            {
                selectedPiece = p;
               
                startDrag = mouseover;

                //hightlight piece posible move 
                bool[,] lightTiles = selectedPiece.PossibleMove(pieces, x, y);

                HightLightTiled.Instance.HightLight(lightTiles);

            }
            else {
                //look for the piece under forces pieces list

                if (forcedPieces.Find(fp=> fp == p ) ==  null)
                    return;

                selectedPiece = p;
                startDrag = mouseover;
            }              
        }
    }

    public void TryMove(int x1, int y1, int x2, int y2)
    {
        forcedPieces = ScanForPossibleMove();

        //multiplayer support
        startDrag = new Vector2(x1, y1);
        endDrag = new Vector2(x2, y2);
        selectedPiece = pieces[x1, y1];

        if (x2< 0 || x2>= pieces.Length || y2 < 0 || y2 >= pieces.Length ) 
        {
            if (selectedPiece != null) 
            {
                MovePiece(selectedPiece.gameObject, x1, y2);
                
            }
            startDrag = Vector2.zero;
            selectedPiece = null;
            HightLightTiled.Instance.HideHightLight();
            return;
        }

        if (selectedPiece != null) 
        {
            //if is has not moved
            if (endDrag == startDrag) {
                MovePiece(selectedPiece.gameObject, x1, y1);
                startDrag = Vector2.zero;
                selectedPiece = null;
                HightLightTiled.Instance.HideHightLight();
                return;
            }

            //check if its a valid move
            if (selectedPiece.ValidMove(pieces, x1, y1, x2, y2))
            {
                //did we kill anithing

                //if this is a jump
                if (Mathf.Abs(x2 - x1) == 2)
                {
                    CheckerPiece p = pieces[(x1 + x2) / 2, (y1 + y2) / 2];
                    if (p != null)
                    {
                        pieces[(x1 + x2) / 2, (y1 + y2) / 2] = null;
                        DestroyImmediate(p.gameObject);
                        haskilled = true;
                    }

                }

                // were we supused to kill anything?
                if (forcedPieces.Count != 0 && !haskilled) {

                    MovePiece(selectedPiece.gameObject, x1, y1);
                    startDrag = Vector2.zero;
                    selectedPiece = null;
                    HightLightTiled.Instance.HideHightLight();
                    return;
                }


                pieces[x2, y2] = selectedPiece;
                pieces[x1, y1] = null;
                MovePiece(selectedPiece.gameObject, x2, y2);
                HightLightTiled.Instance.HideHightLight();
                EndTurn();
            }
            else 
            {
                MovePiece(selectedPiece.gameObject, x1, y1);
                startDrag = Vector2.zero;
                selectedPiece = null;
                HightLightTiled.Instance.HideHightLight();
                return;
            }
        }
    }

    private void EndTurn()
    {
        int x = (int)endDrag.x;
        int y = (int)endDrag.y;

        //promotion piece 
        if (selectedPiece != null) {

            if (selectedPiece.isWhite && !selectedPiece.isKing && y == 7) {
                selectedPiece.isKing = true;
                selectedPiece.transform.Rotate(Vector3.right * 180);
            } else if (!selectedPiece.isWhite && !selectedPiece.isKing && y == 0) 
            {
                selectedPiece.isKing = true;
                selectedPiece.transform.Rotate(Vector3.right * 180);
            }
        }

        //send movment to the server
        if (client != null) 
        {
            string lobbyCode = client.profile.lobbyCode;
            string boolean = EnumHelper.FALSE;
            DataMessageReq dataReq = new DataMessageReq(lobbyCode, GameType.CHECKER.ToString().ToUpper(), boolean, 
                startDrag.x.ToString(), startDrag.y.ToString(), endDrag.x.ToString(),endDrag.y.ToString());

            client.Send(dataReq.GetMessageText());
        }

        HightLightTiled.Instance.HideCaptureHightLight();

        if (ScanForPossibleMove(x, y).Count != 0 && haskilled)
        {
            HightLightTiled.Instance.CaptureHightLight(selectedPiece.ValidCapturePiece(pieces, (int)endDrag.x, (int)endDrag.y));
            selectedPiece = null;
            return;
        }

        selectedPiece = null;
        startDrag = Vector2.zero;

        isWhiteTurn = !isWhiteTurn;
        
        //only for offline mode
      //  isWhite = !isWhite;

        haskilled = false;
        //check for teh victory of player
        bool v = CheckVictory();

        if (!v)
        {
            //for hight light piece
            ScanForPossibleMove();

            //show alert for change player turn information
            ShowAlertPlayerTurn(isWhite);
        }
    }

    private bool CheckVictory()
    {
        bool hasVictory = false;
        var ps = FindObjectsOfType<CheckerPiece>();
        bool hasWhite = false, hasBlack = false;
        for (int i =0; i < ps.Length; i++) {
            if (ps[i].isWhite)
                hasWhite = true;
            else
                hasBlack = true;
        }

        //validate all opponent piece can make a move

        bool whiteWin = true;
        bool blackWin = true;
        //black piece cant make a move
        foreach (CheckerPiece p in ps) {

            if (!p.isWhite && p.HasValidMove(pieces) ) {
                whiteWin = false;
            }
        }

        //white piece cant make a move
        foreach (CheckerPiece p in ps)
        {

            if (p.isWhite && p.HasValidMove(pieces))
            {
                blackWin = false;
            }
        }

        if (!hasWhite || blackWin)
        {
            hasVictory = false;
            Victory(false);
        }

        if (!hasBlack || whiteWin) 
        {
            hasVictory = true;
            Victory(true);
        }
        return hasVictory;
    }

    private void Victory(bool white) {

        if (white) { 
            Debug.Log("White team has wont");
            CanvasManagerUI.Instance.ShowGameOverCanvas();
        }
        else 
        { 
            Debug.Log("black team has wont");
            CanvasManagerUI.Instance.ShowGameOverCanvas();
        }

    }

    private List<CheckerPiece> ScanForPossibleMove(int x, int y) {
        forcedPieces = new List<CheckerPiece>();

        //check all pieces   
        if (pieces[x, y].IsForceToMove(pieces, x, y)) {
            forcedPieces.Add(pieces[x, y]);
        }
        return forcedPieces;
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

    private void UpdateMouseOver() {

        //if its my turn 

        if (!Camera.main) 
        {
            Debug.Log("unable to find main camera");
        }

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f, LayerMask.GetMask("CheesPlane")))
        {
            mouseover.x = (int)hit.point.x;
            mouseover.y = (int)hit.point.z;
        }
        else
        {
            mouseover.x = -1;
            mouseover.y = -1;
        }

    }

    private void UpdatePieceDrag(CheckerPiece piece) {

        //if its my turn 

        if (!Camera.main)
        {
            Debug.Log("unable to find main camera");
        }

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50.0f, LayerMask.GetMask("CheesPlane")))
        {
            piece.transform.position = hit.point + Vector3.up;
        }

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

    private void ShowAlertPlayerTurn(bool turn) {

        if (turn)
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

