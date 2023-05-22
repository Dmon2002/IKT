using UnityEngine;

public class ProjectileAttack : Attack
{
    private float _currentSpeed = 0f;
    private Vector2 _currentDirection = Vector2.zero;

    public override void Strike(Vector2 direction)
    {
        _currentSpeed = StatContainer.GetStatFloatValue(Stat.MovementSpeedStatName);
        _currentDirection = direction;
    }

    private void Update()
    {
        Move(Time.deltaTime);
    }

    private void Move(float time)
    {
        if (_currentDirection == Vector2.zero)
            return;
        transform.Translate(_currentSpeed * time * _currentDirection);
    }
}