using UnityEngine;

public class AttackProjectile : Attack
{
    private float _currentSpeed = 0f;

    protected override void Shoot()
    {
        _currentSpeed = StatContainer.GetStatFloatValue(Stat.MovementSpeedStatName);
    }

    private void Update()
    {
        Move(Time.deltaTime);
    }

    private void Move(float time)
    {
        if (Direction == Vector2.zero)
            return;
        transform.Translate(_currentSpeed * time * Direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Entity>(out var entity))
        {
            CollideEntity(entity);
            //Destroy(gameObject);
        }
    }
}