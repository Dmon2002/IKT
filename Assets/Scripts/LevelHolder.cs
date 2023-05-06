using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class LevelHolder : MonoBehaviour
{
    [SerializeField] private Tilemap _floorTilemap;
    [SerializeField] private Transform _roomContainer;
    [SerializeField] private Transform _borderContainer;
    [SerializeField] private GameObject _tileBorder;

    private Dictionary<Vector2Int, Room> _rooms = new();

    public Room GetRoom(Vector2Int coord) => _rooms.ContainsKey(coord) ? _rooms[coord] : null;

    public UnityEvent<Room> OnPlayerEnterRoom;

    public IEnumerable<Room> Rooms => _rooms.Values;

    public void SetUpRooms(Vector2Int leftUpStartingTile, int width, int height, Dictionary<Vector2Int, GameObject> rooms)
    {
        
        for (int y = leftUpStartingTile.y; y < leftUpStartingTile.y + height; y++)
        {
            for (int x = leftUpStartingTile.x; x < leftUpStartingTile.x + width - y % 2; x++)
            {
                Vector3Int tileIntPos = new Vector3Int(x, y, 0);
                Vector3 tilePosition = _floorTilemap.GetCellCenterWorld(tileIntPos);
                var room = Instantiate(rooms[new Vector2Int(x, y)], tilePosition, Quaternion.identity, _roomContainer)
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
            Instantiate(_tileBorder, tilePosition, Quaternion.identity, _borderContainer);
        }
        for (int x = xStart; x < xEnd; x++)
        {
            Vector3Int tileIntPos = new Vector3Int(x, yEnd, 0);
            Vector3 tilePosition = _floorTilemap.GetCellCenterWorld(tileIntPos);
            Instantiate(_tileBorder, tilePosition, Quaternion.identity, _borderContainer);
        }
        for (int y = yStart; y < yEnd; y++)
        {
            Vector3Int tileIntPos = new Vector3Int(xStart, y, 0);
            Vector3 tilePosition = _floorTilemap.GetCellCenterWorld(tileIntPos);
            Instantiate(_tileBorder, tilePosition, Quaternion.identity, _borderContainer);
        }
        for (int y = yStart; y < yEnd; y++)
        {
            Vector3Int tileIntPos = new Vector3Int(xEnd - y % 2, y, 0);
            Vector3 tilePosition = _floorTilemap.GetCellCenterWorld(tileIntPos);
            Instantiate(_tileBorder, tilePosition, Quaternion.identity, _borderContainer);
        }
    }
}
