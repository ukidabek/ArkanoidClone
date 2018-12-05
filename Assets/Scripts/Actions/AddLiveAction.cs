using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddLiveAction : BaseAction
{
    public override void Perform(GameObject gameObject, Collision2D collision)
    {
        if (LiveManager.Instance != null)
            LiveManager.Instance.AddLives();

    }
}
