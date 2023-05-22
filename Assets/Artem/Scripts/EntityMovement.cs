using UnityEngine;

public abstract class EntityMovement : MonoBehaviour
{
    public abstract void Move(Vector2 moveVector2);

    public void Move(Vector2 direction, float speed)
    {
        Move(direction.normalized * speed);
    }
}
