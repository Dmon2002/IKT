using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Room : MonoBehaviour
{
    public UnityEvent PlayerEnter;
    public UnityEvent FogRevealEnd;
    private Vector2Int _coords;

    [SerializeField] private Animation _fogRevealAnimation;
    [SerializeField] private GameObject _fogTile;

    private bool _fogRevealed;

    public bool FogRevealed => _fogRevealed;

    public Vector2Int Coords => _coords;

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

    public void Test()
    {
        Debug.Log("test sdfasdf");
    }
}
