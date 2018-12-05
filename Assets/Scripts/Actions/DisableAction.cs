using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAction : BaseAction
{
    public override void Perform(GameObject gameObject, Collision2D collision)
    {
        if (gameObject != null && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            if (MapGenerator.Instance != null)
                MapGenerator.Instance.BlockDisabled(gameObject);
        }
    }
}
