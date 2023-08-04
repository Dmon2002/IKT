using System.Collections.Generic;
using UnityEngine;

public class LevelHolder : MonoBehaviour
{
    [SerializeField] private Transform _roomContainer;
    [SerializeField] private Transform _borderContainer;
    [SerializeField] private GameObject _borderTilePrefab;
    [SerializeField] private LevelConfig _levelConfig;

    private readonly Dictionary<Vector2Int, Room> _rooms = new();

    private Grid _grid;

    public IEnumerable<Room> Rooms => _rooms.Values;

    private void Awake()
    {
        _grid = GetComponentInChildren<Grid>();
    }

    public Vector3 ConvertToWorldPosition(Vector2Int intPos)
    {
        if (_grid == null)
            _grid = GetComponentInChildren<Grid>();
        return _grid.GetCellCenterWorld((Vector3Int)intPos);
    }

    public Room GetRoom(Vector2Int coord) => _rooms.ContainsKey(coord) ? _rooms[coord] : null;

    public void SetUpRooms()
    {
        for (int y = 0; y < _levelConfig.Height; y++)
        {
            for (int x = 0; x < _levelConfig.Width - y % 2; x++)
            {
                Vector2Int tileIntPos = new (x, y);
                var roomPrefab = _levelConfig.GetPrefab(tileIntPos);
                if (roomPrefab == null)
                {
                    Debug.LogWarning("Room prefab is null");
                    continue;
                }
                var room = Instantiate(roomPrefab, ConvertToWorldPosition(tileIntPos), Quaternion.identity, _roomContainer).GetComponent<Room>();
                if (room == null) continue;
                room.SetCoords(tileIntPos);
                _rooms[tileIntPos] = room;
            }
        }
    }

    // Некоторые бордеры накладываются друг на друга, исправить в будущем
    public void SetUpBorders()
    {
        Vector2Int _startingTileCoord = Vector2Int.zero;
        int xStart = _startingTileCoord.x - 1;
        int xEnd = _startingTileCoord.x + _levelConfig.Width;
        int yStart = _startingTileCoord.y - 1;
        int yEnd = _startingTileCoord.y + _levelConfig.Height;
        for (int x = xStart; x < xEnd; x++)
        {
            GenerateBorder(new Vector2Int(x, yStart));
        }
        for (int x = xStart; x < xEnd; x++)
        {
            GenerateBorder(new Vector2Int(x, yEnd));
        }
        for (int y = yStart; y < yEnd; y++)
        {
            GenerateBorder(new Vector2Int(xStart, y));
        }
        for (int y = yStart; y < yEnd; y++)
        {
            GenerateBorder(new Vector2Int(xEnd - y % 2, y));
        }
    }

    public GameObject GenerateBorder(Vector2Int borderPos)
    {
        return Instantiate(_borderTilePrefab, ConvertToWorldPosition(borderPos), Quaternion.identity, _borderContainer);
    }
}
