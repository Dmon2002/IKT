using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Room : MonoBehaviour
{
    public UnityEvent OnPlayerEnter;
    private Vector2Int _coords;

    [SerializeField] private GameObject _fog;

    private bool _fogRevealed;

    public bool FogRevealed => _fogRevealed;

    public Vector2Int Coords => _coords;

    public void SetCoords(Vector2Int coords)
    {
        _coords = coords;
    }

    private void Start()
    {
        OnPlayerEnter.AddListener(RevealFog);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerEntity>(out var player))
        {
            OnPlayerEnter.Invoke();
        }
    }

    public void RevealFog()
    {
        _fog.SetActive(false);
    }
}
