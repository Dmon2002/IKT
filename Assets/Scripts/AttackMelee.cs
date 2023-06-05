using UnityEngine;

public class AttackMelee : Attack
{
    protected override void Shoot()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Utilities.ToAngle(Direction)));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Entity>(out var entity))
        {
            CollideEntity(entity);
        }
    }
}
