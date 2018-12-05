using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class ScoreInfo
{
    public string PlayerName = string.Empty;

    public int CurrentScore = 0;

    public void ClearScore()
    {
        CurrentScore = 0;
    }
}
