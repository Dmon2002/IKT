using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBonus : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player potentialPlayer = collision.gameObject.GetComponent<Player>();
        if (potentialPlayer is Player)
        {
            GameObject.FindObjectOfType<SwordUI>().SetCritical();
            Destroy(gameObject);
        }
    }


}
