using UnityEngine;

[RequireComponent (typeof(Collider2D))]
public class BreakingFloor : MonoBehaviour
{
    [SerializeField] private Animation breakingAnimation;

    private bool _isBroke;

    public void StartBreaking()
    {
        breakingAnimation.Play();
    }

    public void Break() 
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerEntity>(out var player))
        {
            StartBreaking();
        }
    }
}
