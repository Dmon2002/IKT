using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[RequireComponent (typeof(Collider2D))]
public class BreakingFloor : MonoBehaviour
{
    [SerializeField] private Animation breakingAnimation;
    [SerializeField] private float fallDistance;
    [SerializeField] private float breakDistance;
    [SerializeField] private float deadlyTime;
    [SerializeField] private Room room;
    [SerializeField] private GameObject col;

    private bool _revealed;
    private BreakState _breakState;

    private void Update()
    {
        if (!_revealed)
            return;
        foreach (var alive in room.InRoom)
        {
            if (_breakState == BreakState.Broke && Vector2.Distance(alive.transform.position, transform.position) <= fallDistance)
            {
                alive.Die();
            }
            if (_breakState == BreakState.Not && Vector2.Distance(alive.transform.position, transform.position) <= breakDistance)
            {
                StartBreaking();
            }
        }
    }

    public void StartBreaking()
    {
        if (_breakState != BreakState.Not || !_revealed)
            return;
        _breakState = BreakState.Breaking;
        breakingAnimation.Play();
    }

    public void Break()
    {
        if (_breakState == BreakState.Broke || !_revealed)
            return;
        _breakState = BreakState.Broke;
        StartCoroutine(DeadlyTimer());
    }

    private IEnumerator DeadlyTimer()
    {
        yield return new WaitForSeconds(deadlyTime);
        _breakState = BreakState.CompletelyBroke;
        col.SetActive(true);
    }

    public void Reveal()
    {
        _revealed = true;
    }

    private enum BreakState
    {
        Not,
        Breaking,
        Broke,
        CompletelyBroke
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, breakDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, fallDistance);
    }
}
