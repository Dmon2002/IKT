using UnityEngine;
using static UnityEditor.PlayerSettings;

[RequireComponent (typeof(Collider2D))]
public class BreakingFloor : MonoBehaviour
{
    [SerializeField] private Animation breakingAnimation;
    [SerializeField] private float fallDistance;

    private bool _isBroke;

    public void StartBreaking()
    {
        breakingAnimation.Play();
    }

    public void Break() 
    {
        _isBroke = true;
        gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<AliveObject>(out var _))
        {
            StartBreaking();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<AliveObject>(out var alive))
        {
            if (Vector2.Distance(transform.position, alive.transform.position) <= fallDistance) {
                alive.Die();
            }
        }
    }
}
