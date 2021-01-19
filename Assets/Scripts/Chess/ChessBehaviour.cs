﻿using UnityEngine;

public abstract class ChessBehaviour : MonoBehaviour
{
    public int CurrentX { get; set; }

    public int CurrentY { get; set; }

    public bool isWhite;

    public void SetPosition(int x, int y) {
        CurrentX = x;
        CurrentY = y;
    }

    public virtual bool[,] PossibleMove() {
        return new bool[8,8];
    }
    
    
}
