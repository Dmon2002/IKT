using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : Manager<LevelManager>
{
    [SerializeField] private Vector2Int leftDownStartingTile;
    [SerializeField] private int levelWidth;
    [SerializeField] private int levelHeight;

    private Tilemap _levelTilemap;
    private LevelHolder _levelHolder;

    public Room GetRoom(Vector2Int roomPos) => _levelHolder.GetRoom(roomPos);

    private void Awake()
    {
        _levelHolder = GetComponentInChildren<LevelHolder>();
        _levelTilemap = GetComponentInChildren<Tilemap>();
    }

    private void Start()
    {
        _levelHolder.SetUpRooms(leftDownStartingTile, levelWidth, levelHeight);
        _levelHolder.SetUpBorders(leftDownStartingTile, levelWidth, levelHeight);
    }

    public Room getNeighbour(Vector2Int roomPos, NeighbourSide side)
    {
        Vector2Int offset;
        switch (side)
        {
            case NeighbourSide.LeftUp:
                offset = new Vector2Int(0, 1);
                break;
            case NeighbourSide.RightUp:
                offset = new Vector2Int(1, 1);
                break;
            case NeighbourSide.LeftDown:
                offset = new Vector2Int(0, -1);
                break;
            case NeighbourSide.RightDown:
                offset = new Vector2Int(0, -1);
                break;
            case NeighbourSide.Left:
                offset = new Vector2Int(-1, 0);
                break;
            case NeighbourSide.Right:
                offset = new Vector2Int(1, 0);
                break;
            default:
                Debug.LogError("Incorrect switch case");
                return null;
        }
        return _levelHolder.GetRoom(roomPos + offset);
    }

    public List<Room> getAllNeighbours(Vector2Int roomPos)
    {
        var neighbours = new List<Room>();
        Room room = getNeighbour(roomPos, NeighbourSide.Left);
        if (room != null)
            neighbours.Add(room);
        room = getNeighbour(roomPos, NeighbourSide.Right);
        if (room != null)
            neighbours.Add(room);
        room = getNeighbour(roomPos, NeighbourSide.LeftUp);
        if (room != null)
            neighbours.Add(room);
        room = getNeighbour(roomPos, NeighbourSide.LeftDown);
        if (room != null)
            neighbours.Add(room);
        room = getNeighbour(roomPos, NeighbourSide.RightUp);
        if (room != null)
            neighbours.Add(room);
        room = getNeighbour(roomPos, NeighbourSide.RightDown);
        if (room != null)
            neighbours.Add(room);
        return neighbours;
    }

    private float GetRoomDistance(Room room, Vector3 position) => Vector2.Distance(room.transform.position, position);

    public Room GetNearestRoom(Vector3 position)
    {
        Room minRoom = null;
        float minValue = float.PositiveInfinity;
        foreach (var room in _levelHolder.Rooms)
        {
            float dist = GetRoomDistance(room, position);
            if (dist < minValue)
            {
                minRoom = room;
                minValue = dist;
            }
        }
        return minRoom;
    }

    public Room GetNearestHideRoom(Vector3 position)
    {
        Room minRoom = null;
        float minValue = float.PositiveInfinity;
        foreach (var room in _levelHolder.Rooms)
        {
            if (room.FogRevealed)
                continue;
            float dist = GetRoomDistance(room, position);
            if (dist < minValue)
            {
                minRoom = room;
                minValue = dist;
            }
        }
        return minRoom;
    }

    public Vector3 ConvertToPosition(Vector2Int intPos) => _levelTilemap.GetCellCenterWorld((Vector3Int)intPos);

    public void RevealFog(Vector2Int roomPos, Vector3 direction)
    {
        var room = _levelHolder.GetRoom(roomPos);
        room.RevealFog(direction);
    }
}

public enum NeighbourSide
{
    Left,
    Right,
    LeftUp,
    RightUp,
    LeftDown,
    RightDown,
}
