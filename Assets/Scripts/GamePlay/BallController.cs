using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class BallController : MonoBehaviour, IOnBoundsCalculation
{
    [SerializeField] private Rigidbody2D _rigidbody2D = null;
    [SerializeField] private float _maxVelocity = 2f;
    [SerializeField] private BallControllerStatus _ballControllerStatus = null;
    [SerializeField] private AnimationCurve _speedToLevelCurve = new AnimationCurve();
    public BallControllerStatus BallControllerStatus { get { return _ballControllerStatus; } }

    public float MaxVelocity { get { return BallControllerStatus != null ? _speedToLevelCurve.Evaluate(BallControllerStatus.CurrentLevel) : _maxVelocity; } }


    private Vector3 _startPosition = Vector3.zero;

    public void StartBallMovement()
    {
        _rigidbody2D.velocity = transform.up * MaxVelocity;
    }

    private void Reset()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(_rigidbody2D.velocity.magnitude > MaxVelocity)
            _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * MaxVelocity;

        if(BallControllerStatus != null)
        {
            BallControllerStatus.CurrentVelocity.Serialize(_rigidbody2D.velocity);
            BallControllerStatus.CurrentPosition.Serialize(_rigidbody2D.position);
        }
    }

    public void UpdateBounds(Vector3 position, Vector2 size)
    {
        Vector3 newPosition = position;
        newPosition.z = 0;
        _startPosition = transform.position = newPosition;
    }

    public void MoveToStartPosition()
    {
        transform.position = _startPosition;
        _rigidbody2D.velocity = Vector2.zero;
    }

    public void ResetStatus()
    {
        if (BallControllerStatus != null)
        {
            BallControllerStatus.ResetStatus();
        }
    }

    public void RestoreFormStatus()
    {
        if(BallControllerStatus != null)
        {
            _rigidbody2D.velocity = BallControllerStatus.CurrentVelocity;
            _rigidbody2D.position = BallControllerStatus.CurrentPosition;
        }
    }

    public void GoToNextLevel()
    {
        if (BallControllerStatus != null)
            BallControllerStatus.GoToNextLevel();
    }
}
