
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private Vector2Int _startingPosition;
    [SerializeField] private int width;
    [SerializeField] private int startingHeight;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileBase _backgroundTile;
    [SerializeField] private float _cameraOffset;

    private int _lastY;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        for (int y = _startingPosition.y; y < _startingPosition.y + startingHeight; y++)
        {
            for (int x = _startingPosition.x; x < _startingPosition.x + width; x++)
            {
                _tilemap.SetTile(new Vector3Int(x, y, 0), _backgroundTile);
            }
        }
        _lastY = _startingPosition.y + startingHeight;
    }

    private void Update()
    {
        if (_mainCamera.transform.position.y >= _tilemap.GetCellCenterWorld(new Vector3Int(0, _lastY, 0)).y - _cameraOffset)
        {
            SpawnRow();
        }
    }

    private void SpawnRow()
    {
        for (int x = _startingPosition.x; x < _startingPosition.x + width; x++)
        {
            _tilemap.SetTile(new Vector3Int(x, _lastY, 0), _backgroundTile);
        }
        _lastY++;

    }
}
