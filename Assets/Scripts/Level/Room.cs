using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Room : MonoBehaviour
{
    private static readonly string _canRevealFogName = "CanReveal";

    [SerializeField] private Animation _fogTile;
    [SerializeField] private float _fogRevealDuration;

    public event Action FogRevealStart;
    public event Action FogRevealEnd;

    private Vector2Int _coords;

    private bool _fogRevealed;

    private List<Entity> _inRoom = new();

    public IEnumerable<Entity> InRoom => _inRoom;

    public Vector2Int Coords => _coords;

    public bool FogRevealed => _fogRevealed;

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
            if (!entity.StatContainer.GetStatBoolValue(_canRevealFogName))
                return;
            RevealFog(transform.position - entity.transform.position);
            if (_inRoom.Contains(entity))
                return;
            _inRoom.Add(entity);
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