using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class LevelHolder : MonoBehaviour
{
    [SerializeField] private Tilemap _floorTilemap;
    [SerializeField] private Transform _roomContainer;
    [SerializeField] private Transform _borderContainer;
    [SerializeField] private GameObject _tileBorderPrefab;
    [SerializeField] private int _startingTiles;
    [SerializeField] private GameObject _tileCleanPrefab;

    private Dictionary<Vector2Int, Room> _rooms = new();
    private LevelGenerator _levelGenerator;

    public Room GetRoom(Vector2Int coord) => _rooms.ContainsKey(coord) ? _rooms[coord] : null;

    public UnityEvent<Room> OnPlayerEnterRoom;

    public IEnumerable<Room> Rooms => _rooms.Values;

    private void Awake()
    {
        _levelGenerator = GetComponentInChildren<LevelGenerator>();
    }

    public void SetUpRooms(Vector2Int leftUpStartingTile, int width, int height)
    {
        
        for (int y = leftUpStartingTile.y; y < leftUpStartingTile.y + height; y++)
        {
            for (int x = leftUpStartingTile.x; x < leftUpStartingTile.x + width - y % 2; x++)
            {
                var tile = _tileCleanPrefab;
                if (y - leftUpStartingTile.y >= _startingTiles)
                {
                    tile = _levelGenerator.getRandomRoom();
                }
                Vector3Int tileIntPos = new Vector3Int(x, y, 0);
                Vector3 tilePosition = _floorTilemap.GetCellCenterWorld(tileIntPos);
                var room = Instantiate(tile, tilePosition, Quaternion.identity, _roomContainer)
                    .GetComponent<Room>();
                room.SetCoords((Vector2Int)tileIntPos);
                room.PlayerEnter.AddListener(() => OnPlayerEnterRoom.Invoke(room));
                _rooms[(Vector2Int)tileIntPos] = room;
            }
        }
    }

    public void SetUpBorders(Vector2Int leftUpStartingTile, int width, int height)
    {
        int xStart = leftUpStartingTile.x - 1;
        int xEnd = leftUpStartingTile.x + width;
        int yStart = leftUpStartingTile.y - 1;
        int yEnd = leftUpStartingTile.y + height;
        for (int x = xStart; x < xEnd; x++)
        {
            Vector3Int tileIntPos = new Vector3Int(x, yStart, 0);
            Vector3 tilePosition = _floorTilemap.GetCellCenterWorld(tileIntPos);
            Instantiate(_tileBorderPrefab, tilePosition, Quaternion.identity, _borderContainer);
        }
        for (int x = xStart; x < xEnd; x++)
        {
            Vector3Int tileIntPos = new Vector3Int(x, yEnd, 0);
            Vector3 tilePosition = _floorTilemap.GetCellCenterWorld(tileIntPos);
            Instantiate(_tileBorderPrefab, tilePosition, Quaternion.identity, _borderContainer);
        }
        for (int y = yStart; y < yEnd; y++)
        {
            Vector3Int tileIntPos = new Vector3Int(xStart, y, 0);
            Vector3 tilePosition = _floorTilemap.GetCellCenterWorld(tileIntPos);
            Instantiate(_tileBorderPrefab, tilePosition, Quaternion.identity, _borderContainer);
        }
        for (int y = yStart; y < yEnd; y++)
        {
            Vector3Int tileIntPos = new Vector3Int(xEnd - y % 2, y, 0);
            Vector3 tilePosition = _floorTilemap.GetCellCenterWorld(tileIntPos);
            Instantiate(_tileBorderPrefab, tilePosition, Quaternion.identity, _borderContainer);
        }
    }
}
