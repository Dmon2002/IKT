using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class LevelHolder : MonoBehaviour
{
    [SerializeField] private GameObject _roomColliderPrefab;
    [SerializeField] private TileBase _fogTile;
    [SerializeField] private Tilemap _floorTilemap;
    [SerializeField] private Transform _roomContainer;

    private Dictionary<Vector2Int, Room> _rooms = new();

    public Room getRoom(Vector2Int coord) => _rooms[coord];


    public UnityEvent<Room> OnPlayerEnterRoom;

    public IEnumerable<Room> Rooms => _rooms.Values;

    private void Start()
    {
        SetUpRooms();
    }

    private void SetUpRooms()
    {
        Tile tile = _floorTilemap.GetTile(Vector3Int.one) as Tile;
        BoundsInt bounds = _floorTilemap.cellBounds;
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tileIntPos = new Vector3Int(x, y, 0);
                if (_floorTilemap.GetTile(tileIntPos) == null)
                    continue;
                Vector3 tilePosition = _floorTilemap.GetCellCenterWorld(tileIntPos);
                var room = Instantiate(_roomColliderPrefab, tilePosition, Quaternion.identity, _roomContainer)
                    .GetComponent<Room>();
                room.SetCoords((Vector2Int)tileIntPos);
                room.OnPlayerEnter.AddListener(() => OnPlayerEnterRoom.Invoke(room));
                _rooms[(Vector2Int)tileIntPos] = room;
            }
        }
    }
}
