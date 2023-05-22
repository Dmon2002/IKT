using UnityEngine;

public class AttackMelee : Attack
{
    public override void Strike(Vector2 direction)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Utilities.ToAngle(direction)));
    }
}
