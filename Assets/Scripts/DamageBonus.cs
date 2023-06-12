using UnityEngine;

public class DamageBonus : MonoBehaviour
{
    [SerializeField] private float _multiplication;
    [SerializeField] private float _duration;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Player potentialPlayer = collision.gameObject.GetComponent<Player>();
    //    if (potentialPlayer is Player)
    //    {
    //        potentialPlayer.BuffWeapon(_multiplication,_duration);
    //        Destroy(gameObject);
    //    }
    //}
}
