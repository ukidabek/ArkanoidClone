using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class LiveInfo
{
    [SerializeField] private int _defaultLivesCount = 3;
    public int Lives = 3;

    public void ResetLives()
    {
        Lives = _defaultLivesCount;
    }
}
