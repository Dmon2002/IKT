using StatSystem;
using UnityEngine;

public class AttackProjectile : Attack
{
    private float _currentSpeed = 0f;

    protected override void Shoot()
    {
        _currentSpeed = StatContainer.GetStat<float>(StatNames.MoveSpeed);
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
        if (collision.TryGetComponent<Player>(out var player))
        {
            CollideEntity(player);
            Destroy(gameObject);
        }
    }
}