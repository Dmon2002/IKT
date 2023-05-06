using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBonus : MonoBehaviour
{
    [SerializeField] private float _hp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player potentialPlayer = collision.gameObject.GetComponent<Player>();
        if (potentialPlayer is Player)
        {
            potentialPlayer.Heal(_hp);
            Destroy(gameObject);
        }
    }
}
