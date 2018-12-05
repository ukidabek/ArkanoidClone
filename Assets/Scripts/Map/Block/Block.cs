using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Block : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer = null;
    public Vector2 Size { get { return _spriteRenderer.sprite.rect.size / _spriteRenderer.sprite.pixelsPerUnit; } }

    public OnCollisionEnter2DEvennt OnCollisionEnter2DCallback = new OnCollisionEnter2DEvennt();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnter2DCallback.Invoke(this.gameObject, collision);
    }

    private void Reset()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
}