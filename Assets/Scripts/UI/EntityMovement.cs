using StatSystem;
using System;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class EntityMovement : MonoBehaviour
{
    [SerializeField] private MovementType _movementType;

    protected StatContainer EntityStatContainer;

    private Rigidbody2D _rb;

    private float _defaultSpeed;

    public float Speed => _defaultSpeed;

    private Vector2 _currentSpeedDirection;

    public event Action<Vector2> MovedPoint;

    public Vector2 LastMoveDirectional => _currentSpeedDirection;

    private void FixedUpdate()
    {
        Move(_currentSpeedDirection);
    }

    public void SetStatConatiner(StatContainer statContainer)
    {
        EntityStatContainer = statContainer;
    }

    private void Move(Vector2 speedDirection)
    {
        _rb.velocity = speedDirection;
    }

    public void MoveInDirection(Vector2 direction)
    {
        if (_movementType == MovementType.Point) return;

        _currentSpeedDirection = Speed * direction;
    }

    public void MoveToPoint(Vector2 point)
    {
        if (_movementType == MovementType.Direction) return;

        transform.position = point;
        MovedPoint?.Invoke(point);
    }

    public void StopMovement()
    {
        //_currentSpeedDirection = 0f;
    }
}

public enum MovementType
{
    Point,
    Direction,
    Both
} 