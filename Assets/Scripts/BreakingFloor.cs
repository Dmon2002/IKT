using UnityEngine;
using static UnityEditor.PlayerSettings;

[RequireComponent (typeof(Collider2D))]
public class BreakingFloor : MonoBehaviour
{
    [SerializeField] private Animation breakingAnimation;
    [SerializeField] private float fallDistance;

    private bool _revealed;
    private BreakState _breakState;

    public void StartBreaking()
    {
        if (_breakState != BreakState.Not || !_revealed)
            return;
        Debug.Log("StartBreaking");
        _breakState = BreakState.Breaking;
        breakingAnimation.Play();
    }

    public void Break()
    {
        if (_breakState == BreakState.Broke || !_revealed)
            return;
        Debug.Log("Break");
        _breakState = BreakState.Broke;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        if (!_revealed)
            return;
        if (collision.TryGetComponent<AliveObject>(out var alive))
        {
            if (_breakState == BreakState.Not)
            {
                StartBreaking();
                return;
            }
            if (_breakState == BreakState.Broke && Vector2.Distance(transform.position, alive.transform.position) <= fallDistance)
            {
                alive.Die();
            }
        }
    }

    public void Reveal()
    {
        Debug.Log("revealed");
        _revealed = true;
    }

    private enum BreakState
    {
        Not,
        Breaking,
        Broke
    }
}
