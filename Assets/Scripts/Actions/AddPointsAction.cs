using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPointsAction : BaseAction
{
    [SerializeField] private int _points = 20;

    public override void Perform(GameObject gameObject, Collision2D collision)
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.UpdateScore(_points);
    }
}
