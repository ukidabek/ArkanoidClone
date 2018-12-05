using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class OnCollisionEnter2DEvennt:UnityEvent<GameObject, Collision2D> {}