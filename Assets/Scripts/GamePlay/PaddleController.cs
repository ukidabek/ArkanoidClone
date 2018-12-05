using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class PaddleController : MonoBehaviour, IOnBoundsCalculation
{
    [SerializeField] private SpriteRenderer _spriteRenderer = null;
    [SerializeField] private Rigidbody2D _rigidbody2D = null;

    [SerializeField] private float _baseSpeed = 2f;
    public float CurrentSpeed { get { return _baseSpeed; } }

    [SerializeField] private Vector3 positionOffset = new Vector3(0, -2.4f, 0);

    [SerializeField] private Vector3 _currentPosition = Vector3.zero;
    [SerializeField] private PaddleControllerStatus _paddleControllerStatus = null;
    public PaddleControllerStatus PaddleControllerStatus { get { return _paddleControllerStatus; }
    }
    public Vector2 MovementVector { get; set; }

    [SerializeField] private float _maxX = 0;
    [SerializeField] private float _minX = 0;

    [SerializeField] private float _forceOnCollision = 10f;

    private Vector3 _startPosition = Vector3.zero;

    private void Awake()
    {
        _currentPosition = transform.position;
    }

    private void FixedUpdate()
    {
        _currentPosition.x += MovementVector.x * CurrentSpeed * Time.fixedDeltaTime;
        _currentPosition.x = Mathf.Clamp(_currentPosition.x, _minX, _maxX);
        _rigidbody2D.position = _currentPosition;
        if (_paddleControllerStatus != null)
            _paddleControllerStatus.CurrentPosition.Serialize(_currentPosition);
    }

    public void UpdateBounds(Vector3 position, Vector2 size)
    {
        _startPosition = transform.position = position + positionOffset;
        _currentPosition = transform.position;
        Vector2 spriteSize = _spriteRenderer.sprite.rect.size / _spriteRenderer.sprite.pixelsPerUnit;
        float xOffset = spriteSize.x / 2;
        _maxX = (transform.position.x + size.x) - xOffset;
        _minX = (transform.position.x - size.x) + xOffset;
    }

    private void Reset()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody != null)
            collision.rigidbody.AddForce(MovementVector * _forceOnCollision);
    }

    public void MoveToStartPosition()
    {
        SetPosition(_startPosition);
    }

    private void SetPosition(Vector3 position)
    {
        transform.position = _currentPosition = position;
    }

    public void RestoreFormStatus()
    {
        if (_paddleControllerStatus != null)
            SetPosition(_paddleControllerStatus.CurrentPosition);
    }
}
