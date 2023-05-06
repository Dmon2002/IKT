using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TerrainUtils;
using UnityEngine.Tilemaps;

public class LevelHolder : MonoBehaviour
{
    [SerializeField] private GameObject _roomPrefab;
    [SerializeField] private Tilemap _floorTilemap;
    [SerializeField] private Transform _roomContainer;

    private Dictionary<Vector2Int, Room> _rooms = new();

    public Room GetRoom(Vector2Int coord) => _rooms.ContainsKey(coord) ? _rooms[coord] : null;

    public UnityEvent<Room> OnPlayerEnterRoom;

    public IEnumerable<Room> Rooms => _rooms.Values;

    public void SetUpRooms(Vector2Int leftUpStartingTile, int width, int height)
    {
        for (int x = leftUpStartingTile.x; x < leftUpStartingTile.x + width; x++)
        {
            for (int y = leftUpStartingTile.y; y < leftUpStartingTile.y + height; y++)
            {
                Vector3Int tileIntPos = new Vector3Int(x, y, 0);
                Vector3 tilePosition = _floorTilemap.GetCellCenterWorld(tileIntPos);
                var room = Instantiate(_roomPrefab, tilePosition, Quaternion.identity, _roomContainer)
                    .GetComponent<Room>();
                room.SetCoords((Vector2Int)tileIntPos);
                room.PlayerEnter.AddListener(() => OnPlayerEnterRoom.Invoke(room));
                _rooms[(Vector2Int)tileIntPos] = room;
            }
        }
    }
}
