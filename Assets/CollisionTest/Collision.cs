using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class Collision : MonoBehaviour
{
    private Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        Gizmos.color = Color.red;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D - " + Time.time);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("OnTriggerExit2D");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("OnTriggerStay2D - " + Time.time);
        Gizmos.color = Color.green;
    }

    private void OnDrawGizmos()
    {
        col = GetComponent<Collider2D>();
        Gizmos.DrawCube(col.bounds.center, col.bounds.size);
    }
}
