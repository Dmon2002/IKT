using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Room : MonoBehaviour
{
    public UnityEvent FogRevealStart;
    public UnityEvent FogRevealEnd;
    private Vector2Int _coords;

    private RoomSpawner _spawner;

    //[SerializeField] private Animation _fogRevealAnimation;
    [SerializeField] private GameObject _fogTile;
    [SerializeField] private bool _fogRevealed;
    [SerializeField] private float _fogDelay;

    private List<AliveObject> _inRoom = new();

    public IEnumerable<AliveObject> InRoom => _inRoom;

    public Vector2Int Coords => _coords;

    public bool FogRevealed => _fogRevealed;

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
    
    public void RevealFog(Vector2 direction)
    {
        if (_fogRevealed)
            return;
        _fogRevealed = true;
        FogRevealStart.Invoke();
        _fogTile.GetComponent<FogAnimationEvent>()?.FogAnimationStart(direction);
        StartCoroutine(FogRevealDelayCoroutine());

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
        if (collision.TryGetComponent<AliveObject>(out var alive))
        {
            if (!alive.CanReveal)
                return;
            RevealFog(transform.position - alive.transform.position);
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

    private IEnumerator FogRevealDelayCoroutine()
    {
        yield return new WaitForSeconds(_fogDelay);
        FogRevealEnd.Invoke();
    }
}
