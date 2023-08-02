using Sirenix.OdinInspector;
using StatSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Room : MonoBehaviour
{
    [SerializeField] private bool _initiallyRevealed;
    [ShowIf("@!_initiallyRevealed")]
    [SerializeField] private Animation _fogTile;
    [ShowIf("@!_initiallyRevealed")]
    [SerializeField] private float _fogRevealDuration;

    public event System.Action FogRevealStart;
    public event System.Action FogRevealEnd;

    private Vector2Int _coords;

    private bool _fogRevealed;

    private List<Entity> _inRoom = new();

    public IEnumerable<Entity> InRoom => _inRoom;

    public Vector2Int Coords => _coords;

    public bool FogRevealed => _fogRevealed;

    private void Awake()
    {
        if (_initiallyRevealed)
        {
            _fogRevealed = true;
        }
    }

    public void SetCoords(Vector2Int coords)
    {
        _coords = coords;
    }
    
    public void RevealFog(Vector2 direction)
    {
        if (_fogRevealed)
            return;
        _fogRevealed = true;
        FogRevealStart?.Invoke();
        _fogTile.Play();
        StartCoroutine(FogRevealDurationCoroutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Entity>(out var entity))
        {
            if (!_inRoom.Contains(entity))
            {
                _inRoom.Add(entity);
            }
            if (entity.StatContainer.GetStat<bool>(StatNames.CanReveal))
            {
                RevealFog(transform.position - entity.transform.position);
            }


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Entity>(out var entity))
        {
            _inRoom.Remove(entity);
        }
    }

    private IEnumerator FogRevealDurationCoroutine()
    {
        yield return new WaitForSeconds(_fogRevealDuration);
        FogRevealEnd?.Invoke();
    }
}
