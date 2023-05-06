using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Room : MonoBehaviour
{
    public UnityEvent PlayerEnter;
    public UnityEvent FogRevealEnd;
    private Vector2Int _coords;

    private RoomSpawner _spawner;

    [SerializeField] private Animation _fogRevealAnimation;
    [SerializeField] private GameObject _fogTile;

    private List<AliveObject> _aliveObjects = new ();

    public IEnumerable<AliveObject> AliveObjects => _aliveObjects;

    private bool _fogRevealed;

    public bool FogRevealed => _fogRevealed;

    public Vector2Int Coords => _coords;

    private void Awake()
    {
        _spawner = GetComponent<RoomSpawner>();
    }

    public void SetCoords(Vector2Int coords)
    {
        _coords = coords;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            PlayerEnter.Invoke();
        }
    }

    public void RevealFog()
    {
        if (_fogRevealed)
            return;
        _fogTile.GetComponent<Animation>()?.Play();
        _fogRevealed = true;
    }

    public void OnFogAnimationEnd()
    {
        FogRevealEnd.Invoke();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<AliveObject>(out var alive))
        {
            _aliveObjects.Add(alive);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<AliveObject>(out var alive))
        {
            _aliveObjects.Remove(alive);
        }
    }

}
