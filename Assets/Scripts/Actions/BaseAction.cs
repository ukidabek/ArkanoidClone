using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    public abstract void Perform(GameObject gameObject, Collision2D collision);
}
