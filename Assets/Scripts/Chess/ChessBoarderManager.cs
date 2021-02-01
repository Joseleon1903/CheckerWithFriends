using Assets.Scripts;
using Assets.Scripts.Chess;
using Assets.Scripts.Utils;
using Assets.Scripts.WebSocket;
using Assets.Scripts.WebSocket.Message;
using System;
using System.Collections.Generic;
using UnityEngine;

public struct ChessboardState {

    public bool isGameOver;
    public GameOverType gameOverType;
    public PlayerType playerWin;

}

public class ChessBoarderManager : MonoBehaviour
{
    public static ChessBoarderManager Instance { get; set; }

    [SerializeField] private Material selectedMat;

    private bool[,] allowedMoves { get; set; }

    public ChessBehaviour[,] ChessTable { get; set; }

    private ChessBehaviour selectedChess;

    public List<GameObject> chessPrefabs;

    public List<GameObject> activeChees;

    public bool isWhiteTurn = true;

    public ChessBehaviour SelectedPiece { get { return selectedChess; } set { selectedChess = value; } }

    public ChessboardState ChessboardState { get; set; }

    private ClientWSBehavour client;

    private void Awake()
    {
        Instance = this;
        activeChees = new List<GameObject>();
        ChessTable = new ChessBehaviour[8, 8];
        ChessboardState = new ChessboardState { isGameOver = false, gameOverType = GameOverType.OUT_OF_TIME, playerWin = PlayerType.P1 };

        //for online match setting
        client = FindObjectOfType<ClientWSBehavour>();

        if (client != null)
        {
            if (client.profile.isHost) { OnlinePlayerBehavour.Instance.playerType = PlayerType.P1; } else { OnlinePlayerBehavour.Instance.playerType = PlayerType.P2; }
            OnlinePlayerBehavour.Instance.isOnlineGame = true;
        }
    }

    //public void MoveChess(int x, int y, bool onlineMovement)
    //{
    //    int previonsX = selectedChess.CurrentX;
    //    int previonsY = selectedChess.CurrentY;

    //    if (allowedMoves[x,y])
    //    {
    //        ChessBehaviour c = ChessTable[x, y];
    //        if (c != null && c.isWhite  != isWhiteTurn ) 
    //        {
    //            //Capture a piece
    //            Debug.Log("Capture a piece");

    //            if (!isWhiteTurn)
    //                CapturedBoardBlack.Instance.AddCaturePiece(c.gameObject);
    //            else
    //                CapturedBoardWhite.Instance.AddCaturePiece(c.gameObject);

    //            //destroy chess 
    //            activeChees.Remove(c.gameObject);
    //            Destroy(c.gameObject);
    //        }
          

         
    //        if (selectedChess.GetType() == typeof(PawnChess)) {

    //            // crown a pawn Logic
    //            SpawnPosition pos = new SpawnPosition { PosX = x, PosY = y };

    //            if (isWhiteTurn && y == 7) {

    //                Debug.Log("White Pawn crow new piece");
    //                CanvasManagerUI.Instance.ShowUI(true, pos, isWhiteTurn);
    //            }
    //            else if (!isWhiteTurn && y == 0)
    //            {

    //                Debug.Log("Black Pawn crow new piece");
    //                CanvasManagerUI.Instance.ShowUI(true, pos, isWhiteTurn);

    //            }

    //        }

    //        //validate enpassant move
    //        int moveDiff = Math.Abs(selectedChess.CurrentY - y);
    //        if (selectedChess.GetType() == typeof(PawnChess)) {
    //            PawnChess pa = (PawnChess)selectedChess;

    //            //eat piece passant move white
    //            if (pa.EnPassantMove[0] != -1 && pa.EnPassantMove[1] != -1 && selectedChess.isWhite && x < 7 && x >=0)
    //            {
    //                //capture pawn 
    //                ChessBehaviour capPawn = ChessTable[x, y - 1];
    //                if (capPawn.isWhite != selectedChess.isWhite) {
    //                    CapturedBoardWhite.Instance.AddCaturePiece(capPawn.gameObject);
    //                    activeChees.Remove(capPawn.gameObject);
    //                    Destroy(capPawn.gameObject);
    //                }
    //            }

    //            //eat piece passant move black
    //            if (pa.EnPassantMove[0] != -1 && pa.EnPassantMove[1] != -1 && !selectedChess.isWhite && x < 7 && x >= 0)
    //            {
    //                //capture pawn 
    //                ChessBehaviour capPawn = ChessTable[x, y + 1];
    //                if (capPawn.isWhite != selectedChess.isWhite)
    //                {
    //                    CapturedBoardBlack.Instance.AddCaturePiece(capPawn.gameObject);
    //                    activeChees.Remove(capPawn.gameObject);
    //                    Destroy(capPawn.gameObject);
    //                }
    //            }

    //            if (moveDiff == 2 && (x - 1) >= 0 && (x + 1) <= 7) {
    //                Debug.Log("moveDiff: " + moveDiff);

    //                PawnChess cs = null;

                    
    //                cs = (PawnChess)ChessTable[x - 1, y];
                    
    //                if (cs != null && cs.isWhite && cs.isWhite != selectedChess.isWhite)
    //                {

    //                    cs.EnPassantMove[0] = x;
    //                    cs.EnPassantMove[1] = y + 1;
    //                }

    //                //passant left white
    //                cs = (PawnChess)ChessTable[x + 1, y];
    //                if (cs != null && cs.isWhite && cs.isWhite != selectedChess.isWhite)
    //                {
    //                    cs.EnPassantMove[0] = x;
    //                    cs.EnPassantMove[1] = y + 1;
    //                }
                    
    //                //passant black
    //                cs = (PawnChess)ChessTable[x -1, y];
    //                if (cs != null && !cs.isWhite && cs.isWhite != selectedChess.isWhite)
    //                {
    //                    cs.EnPassantMove[0] = x;
    //                    cs.EnPassantMove[1] = y - 1;
    //                }

    //                cs = (PawnChess)ChessTable[x + 1, y];
    //                if (cs != null && !cs.isWhite && cs.isWhite != selectedChess.isWhite)
    //                {
    //                    cs.EnPassantMove[0] = x;
    //                    cs.EnPassantMove[1] = y - 1;
    //                }


    //            }
    //            pa.EnPassantMove[0] = -1;
    //            pa.EnPassantMove[1] = -1;
    //        }

    //        //castiling move logic 
    //        if (selectedChess.GetType() == typeof(RookChess)) {
    //            RookChess rock = (RookChess) selectedChess;
    //            rock.IsHasMove = true;
    //        }

    //        if (selectedChess.GetType() == typeof(KingChess))
    //        {
    //            KingChess king = (KingChess)selectedChess;
    //            king.IsFirstMove = false;

    //            if (king.isCastlingMove[0] == x && king.isCastlingMove[1] == y && selectedChess.isWhite) {

    //                ChessBehaviour rookChess;
    //                int NextRookPosX, NextRookPosY, NextKingPosX, NextKingPosY;

    //                if (x < selectedChess.CurrentX) 
    //                {
    //                    rookChess = ChessTable[0, 0];
    //                    NextRookPosX = 2;
    //                    NextRookPosY = 0;
    //                    NextKingPosX = 1;
    //                    NextKingPosY = 0;
    //                }
    //                else
    //                {
    //                    rookChess = ChessTable[7, 0];
    //                    NextRookPosX = 5;
    //                    NextRookPosY = 0;
    //                    NextKingPosX = 6;
    //                    NextKingPosY = 0; 
    //                }

    //                ChessTable[rookChess.CurrentX, rookChess.CurrentY] = null;
    //                ChessTable[NextRookPosX, NextRookPosY] = rookChess;
    //                rookChess.transform.position = TilesUtils.GetTileCenter(NextRookPosX, NextRookPosY);
    //                rookChess.SetPosition(NextRookPosX, NextRookPosY);

    //                ChessTable[selectedChess.CurrentX, selectedChess.CurrentY] = null;
    //                selectedChess.transform.position = TilesUtils.GetTileCenter(NextKingPosX, NextKingPosY);
    //                selectedChess.SetPosition(NextKingPosX, NextKingPosY);
    //                ChessTable[NextKingPosX, NextKingPosY] = selectedChess;

    //                isWhiteTurn = !isWhiteTurn;

    //                isSpecialMove = true;
    //            }

    //            if (king.isCastlingMove[0] == x && king.isCastlingMove[1] == y && !selectedChess.isWhite)
    //            {

    //                ChessBehaviour rookChess;
    //                int NextRookPosX, NextRookPosY, NextKingPosX, NextKingPosY;

    //                if (x < selectedChess.CurrentX)
    //                {
    //                    rookChess = ChessTable[0, 7];
    //                    NextRookPosX = 2;
    //                    NextRookPosY = 7;
    //                    NextKingPosX = 1;
    //                    NextKingPosY = 7;
    //                }
    //                else
    //                {
    //                    rookChess = ChessTable[7, 7];
    //                    NextRookPosX = 5;
    //                    NextRookPosY = 7;
    //                    NextKingPosX = 6;
    //                    NextKingPosY = 7;
    //                }

    //                ChessTable[rookChess.CurrentX, rookChess.CurrentY] = null;
    //                ChessTable[NextRookPosX, NextRookPosY] = rookChess;
    //                rookChess.transform.position = TilesUtils.GetTileCenter(NextRookPosX, NextRookPosY);
    //                rookChess.SetPosition(NextRookPosX, NextRookPosY);

    //                ChessTable[selectedChess.CurrentX, selectedChess.CurrentY] = null;
    //                selectedChess.transform.position = TilesUtils.GetTileCenter(NextKingPosX, NextKingPosY);
    //                selectedChess.SetPosition(NextKingPosX, NextKingPosY);
    //                ChessTable[NextKingPosX, NextKingPosY] = selectedChess;

    //                isWhiteTurn = !isWhiteTurn;

    //                isSpecialMove = true;
    //            }

    //        }

    //        if (!isSpecialMove) {
    //            ChessTable[selectedChess.CurrentX, selectedChess.CurrentY] = null;
    //            selectedChess.transform.position = TilesUtils.GetTileCenter(x, y);
    //            selectedChess.SetPosition(x, y);
    //            ChessTable[x, y] = selectedChess;

    //            isWhiteTurn = !isWhiteTurn; 
    //        }

    //    }

    //    //Send Movement to the server
    //    if (client != null && OnlinePlayerBehavour.Instance.isOnlineGame && onlineMovement)
    //    {

    //        //string lobbyCode = client.profile.lobbyCode;
    //        //string boolean = (GameManager.Instance.CurrentPlayer.IsChecked) ? EnumHelper.TRUE : EnumHelper.FALSE;

    //        //DataMessageReq dataReq = new DataMessageReq(lobbyCode, GameType.CHESS.ToString().ToUpper(), boolean,
    //        //    previonsX.ToString(), previonsY.ToString(), x.ToString(), y.ToString());

    //        //client.Send(dataReq.GetMessageText());

            
    //    }

    //    isSpecialMove = false;

    //    //remove piece selector
    //    SelectorBehavour.Instance.RemoveSelectedPiece();

    //    //validate victory 
    //    ValidatePlayerVictory();

    //}

    //public void ValidateUncheckPlayerMove(ChessValidateCheckMoveResp vCheck)
    //{
    //    Debug.Log("Entering in method ValidateUncheckPlayerMove");

    //    ClientWSBehavour client = FindObjectOfType<ClientWSBehavour>();

    //    Node tNode = BoardGrid.Instance.GetNodeAt(vCheck.endPosY, vCheck.endPosX);

    //    Piece cPiece = BoardGrid.Instance.GetNodeAt(vCheck.startPosY, vCheck.startPosX).Piece;

    //    Piece tPiece = (tNode != null && tNode.Piece != null) ? tNode.Piece : null;

    //    GCPlayer player = GameManager.Instance.CurrentPlayer;


    //    if (tPiece == null || cPiece.IsPossibleMove(tNode))
    //    {
    //        if (cPiece.IsPossibleMove(tNode))
    //        {
    //            if (Rules.IsCheckMove(player, cPiece, tNode, true))
    //            {
    //                Debug.Log("Move checked"); // do nothing
    //                Debug.Log("Response Invalid Player Move");
    //                // send P2 is invalid move notification player is check
    //                ChessEndValidateCheckMoveReq message = MessageUtils.ConvertFromMessage(vCheck, false);
    //                client.Send(message.GetMessageText());
    //            }
    //            else 
    //            {
    //                Debug.Log("Response valid player Move");
    //                // send P2 is valid move en proced to done
    //                ChessEndValidateCheckMoveReq message = MessageUtils.ConvertFromMessage(vCheck, true);
    //                client.Send(message.GetMessageText());

    //                BoardHightLight.Intance.HideHightLightKingChecked();

    //                cPiece.MoveToXZ(tNode);
    //                cPiece.Drop();
    //                cPiece.Compute();
    //                OnlinePlayerMovement movement = new OnlinePlayerMovement(vCheck.startPosX, vCheck.startPosY, vCheck.endPosX, vCheck.endPosY);
    //                InputManager.Instance.ChessBoardMovementeApply(movement, true);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (cPiece.IsPossibleEat(tNode))
    //        {
    //            if (Rules.IsCheckEat(player, cPiece, tNode, true))
    //            {
    //                Debug.Log("Eat checked"); // do nothing
    //                Debug.Log("Move checked"); // do nothing
    //                Debug.Log("Response Invalid Player Move");
    //                // send P2 is invalid move notification player is check
    //                ChessEndValidateCheckMoveReq message = MessageUtils.ConvertFromMessage(vCheck, false);
    //                client.Send(message.GetMessageText());
    //            }
    //            else
    //            {
    //                Debug.Log("Response valid player Move");
    //                // send P2 is valid move en proced to done
    //                //apply move to current player
    //                ChessEndValidateCheckMoveReq message = MessageUtils.ConvertFromMessage(vCheck, true);
    //                client.Send(message.GetMessageText());

    //                BoardHightLight.Intance.HideHightLightKingChecked();

    //                cPiece.MoveToXZ(tNode);
    //                cPiece.Drop();
    //                cPiece.Compute();
    //                OnlinePlayerMovement movement = new OnlinePlayerMovement(vCheck.startPosX, vCheck.startPosY, vCheck.endPosX, vCheck.endPosY);
    //                InputManager.Instance.ChessBoardMovementeApply(movement, true);
    //            }
    //        }
    //    }

    //}

    public void ValidatePlayerVictory()
    {
        Debug.Log("Entering validate player victory");

        if (ChessboardState.isGameOver && ChessboardState.gameOverType == GameOverType.CHECKMATE) {

            CanvasManagerUI.Instance.ShowGameOverCanvas();

            if (client != null && OnlinePlayerBehavour.Instance.isOnlineGame && ChessboardState.playerWin == OnlinePlayerBehavour.Instance.playerType) {

                string lobbyCode = client.profile.lobbyCode;
                VictoryGameMessageReq dataReq = new VictoryGameMessageReq(lobbyCode, GameType.CHESS.ToString().ToUpper(),
                    OnlinePlayerBehavour.Instance.playerType.ToString()); 
                client.Send(dataReq.GetMessageText());
            }

        }

    }

    //public void PlayerCheckedHightLight(GCPlayer currentPlayer)
    //{
    //    KingChess[] kings = FindObjectsOfType<KingChess>();

    //    foreach (KingChess ki in kings) {

    //        if (currentPlayer.Type == PlayerType.P1 && ki.isWhite == true)
    //        {
    //            BoardHightLight.Intance.HightLightKingChecked(ki.CurrentX, ki.CurrentY);
    //        }

    //        if (currentPlayer.Type == PlayerType.P2 && ki.isWhite == false)
    //        {
    //            BoardHightLight.Intance.HightLightKingChecked(ki.CurrentX, ki.CurrentY);
    //        }

    //    }
    //}

    //public void MoveChessMethod(int row, int col, bool onlineMove)
    //{
    //    MoveChess(row, col, onlineMove);
    //}

    //public void TryOnlinePlayerMove(DataMessageResp message) {
    //    int startPosX = int.Parse(message.startPosX);
    //    int startPosY = int.Parse(message.startPosY);
    //    int endPosX = int.Parse(message.endPosX);
    //    int endPosY = int.Parse(message.endPosY);
    //    OnlinePlayerMovement onlinePlayer = new OnlinePlayerMovement(startPosX, startPosY, endPosX, endPosY);
    //    OnlinePlayerBehavour.Instance.PlayerMovement = onlinePlayer;
    //    //InputManager.Instance.InvokeInputEvent();

    //    if (string.Equals(message.checketdMove, EnumHelper.TRUE))
    //    {
    //        PlayerType typeP = (OnlinePlayerBehavour.Instance.playerType == PlayerType.P1) ? PlayerType.P2 : PlayerType.P1;
    //        GameObject[] kings = GameObject.FindGameObjectsWithTag("King");

    //        foreach (GameObject chess in kings) {

    //            ChessBehaviour piece = chess.GetComponent<ChessBehaviour>();

    //            if (typeP == PlayerType.P1 && !piece.isWhite)
    //            {
    //                Node nPiece = chess.GetComponent<Piece>().Node;
    //                chess.GetComponent<KingChess>().IsCheck = true;
    //                BoardHightLight.Intance.HightLightKingChecked(nPiece.col, nPiece.row);
    //            }
    //            if (typeP == PlayerType.P2 && piece.isWhite)
    //            {
    //                Node nPiece = chess.GetComponent<Piece>().Node;
    //                chess.GetComponent<KingChess>().IsCheck = true;
    //                BoardHightLight.Intance.HightLightKingChecked(nPiece.col, nPiece.row);
    //            }
    //        }
    //    }

    //}

    public void SelectChess(int x, int y , bool isOnlinePlayer)
    {
        if (ChessTable[x, y] == null)
            return;

        if (ChessTable[x, y].isWhite != isWhiteTurn)
            return;

        bool hasAtLeastOneMove = false;

        allowedMoves = ChessTable[x, y].PossibleMove();

        for (int i =0; i < 8; i++) {

            for (int j = 0; j < 8; j++)
            {
                if (allowedMoves [i,j]) {
                    hasAtLeastOneMove = true;
                }
            }
        }

        if (!hasAtLeastOneMove) {
            return;
        }

        selectedChess = ChessTable[x, y];
    }

    public void MoveChessSendMessage(string startX, string startY, string endX, string endY, string boolCheck) {

        string lobbyCode = client.profile.lobbyCode;
        DataMessageReq dataReq = new DataMessageReq(lobbyCode, GameType.CHESS.ToString().ToUpper(), boolCheck,
            startX, startY, endX, endY);

        client.Send(dataReq.GetMessageText());
    }

    public GameObject SpwanChess(int index,int x, int y, Quaternion quaterion) {
        Vector3 position = TilesUtils.GetTileCenter(x,y);
        GameObject chess = Instantiate(chessPrefabs[index],  position, quaterion);
        chess.transform.SetParent(transform);
        ChessTable[x, y] = chess.GetComponent<ChessBehaviour>();
        ChessTable[x, y].SetPosition(x, y);
        activeChees.Add(chess);
        return chess;
    }

    public void ChessBoardPromotedPiece(PlayerType pType, int prefabIndex, SpawnPosition spawnPos) {

        Quaternion quaterionWhite = Quaternion.Euler(0, 0, 0);
        Quaternion quaterionBlack = Quaternion.Euler(0, 180, 0);

        Quaternion orientation = (pType == PlayerType.P1) ? quaterionWhite : quaterionBlack;

        Debug.Log($"Selected prefab piece : {prefabIndex}");

        //delete current piece 
        ChessBehaviour piece = Instance.ChessTable[spawnPos.PosX, spawnPos.PosY];
        Instance.ChessTable[spawnPos.PosX, spawnPos.PosY] = null;
        Piece pawn = piece.GetComponent<Piece>();

        //GCPlayer currentPlayer = GameManager.Instance.Opponent(GameManager.Instance.CurrentPlayer);
        //currentPlayer.RemovePiece(pawn);

        Destroy(piece.gameObject);

        //spawn selected piece 

        //GridCoords grid = new GridCoords(spawnPos.PosY, spawnPos.PosX);
        
        //BoardGrid.Instance.SpawnPiece(grid,
        //    Instance.SpwanChess(prefabIndex, spawnPos.PosX, spawnPos.PosY, orientation),
        //    currentPlayer.Type);

        //if (OnlinePlayerBehavour.Instance.isOnlineGame && pType == OnlinePlayerBehavour.Instance.playerType)
        //{
        //    Debug.Log("Send promote Piece message to P2 player");
        //    ClientWSBehavour client = FindObjectOfType<ClientWSBehavour>();

        //    string lobbyCode = client.profile.lobbyCode;

        //    ChessPromotedPieceReq dataReq = new ChessPromotedPieceReq(lobbyCode, GameType.CHESS.ToString().ToUpper(),
        //        OnlinePlayerBehavour.Instance.playerType.ToString().ToUpper(),
        //        prefabIndex, spawnPos.PosX.ToString(), spawnPos.PosX.ToString());

        //    client.Send(dataReq.GetMessageText());
        //}
    }

}
