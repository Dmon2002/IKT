using System.Collections.Generic;
using UnityEngine;

public class LevelHolder : MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private int _levelWidth;
    [SerializeField] private int _levelHeight;
    [SerializeField] private Vector2Int _startingTileCoord;

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
        for (int y = _startingTileCoord.y; y < _startingTileCoord.y + _levelHeight; y++)
        {
            for (int x = _startingTileCoord.x; x < _startingTileCoord.x + _levelWidth - y % 2; x++)
            {
                Vector2Int tileIntPos = new (x, y);
                return;
                var room = _levelGenerator.GenerateRoom(tileIntPos).GetComponent<Room>();
                if (room == null) continue;
                room.SetCoords(tileIntPos);
                _rooms[tileIntPos] = room;
            }
        }
    }

    // Некоторые бордеры накладываются друг на друга, исправить в будущем
    public void SetUpBorders()
    {
        int xStart = _startingTileCoord.x - 1;
        int xEnd = _startingTileCoord.x + _levelWidth;
        int yStart = _startingTileCoord.y - 1;
        int yEnd = _startingTileCoord.y + _levelHeight;
        for (int x = xStart; x < xEnd; x++)
        {
            _levelGenerator.GenerateBorder(new Vector2Int(x, yStart));
        }
        for (int x = xStart; x < xEnd; x++)
        {
            _levelGenerator.GenerateBorder(new Vector2Int(x, yEnd));
        }
        for (int y = yStart; y < yEnd; y++)
        {
            _levelGenerator.GenerateBorder(new Vector2Int(xStart, y));
        }
        for (int y = yStart; y < yEnd; y++)
        {
            _levelGenerator.GenerateBorder(new Vector2Int(xEnd - y % 2, y));
        }
    }
}
