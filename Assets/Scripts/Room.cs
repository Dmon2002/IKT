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
    [SerializeField] private bool _fogRevealed;

    private List<AliveObject> _inRoom = new();

    public IEnumerable<AliveObject> InRoom => _inRoom;

    public Vector2Int Coords => _coords;

    public void OnFogRevealEnd()
    {
        FogRevealEnd.Invoke();
    }

    private void Awake()
    {
        _spawner = GetComponent<RoomSpawner>();
    }

    public void SetCoords(Vector2Int coords)
    {
        _coords = coords;
    }
    
    public void RevealFog(Transform target)
    {
        if (_fogRevealed)
            return;
        _fogTile.GetComponent<FogAnimationEvent>()?.FogAnimationStart(target);

    }

    public void OnFogAnimationEnd()
    {
        FogRevealEnd.Invoke();
    }

    public void AddAliveToRoom(AliveObject obj)
    {
        _inRoom.Add(obj);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            PlayerEnter.Invoke();
            RevealFog(player.transform);
        }
        if (collision.TryGetComponent<AliveObject>(out var alive))
        {
            if (_inRoom.Contains(alive))
                return;
            _inRoom.Add(alive);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<AliveObject>(out var alive))
        {
            _inRoom.Remove(alive);
        }
    }

}
