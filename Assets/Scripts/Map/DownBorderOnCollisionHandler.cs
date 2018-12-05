using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownBorderOnCollisionHandler : MonoBehaviour
{
    public OnCollisionEnter2DEvennt OnCollisionEnter2DEvennt = new OnCollisionEnter2DEvennt();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnter2DEvennt.Invoke(this.gameObject, collision);
    }
}
