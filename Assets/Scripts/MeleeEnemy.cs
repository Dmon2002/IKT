using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private float _bodyDamage;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.TryGetComponent<Player>(out var player))
        {
            player.ApplyDamage(_bodyDamage);
        }
    }
}
