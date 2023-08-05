using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Manager<LevelManager>
{
    [SerializeField] private float _roomDetectionRadius;
    [SerializeField] private Vector2Int _playerStartingCoord;
    [SerializeField] private GameObject _playerPrefab;

    private LevelHolder _levelHolder;
    private GameObject _player;

    private bool _spawned = false;

    public GameObject Player
    {
        get
        {
            if (_player == null)
                _player = SpawnPlayer();
            return _player;
        }
    }

    private void Awake()
    {
        _levelHolder = GetComponentInChildren<LevelHolder>();
        if (_player == null) 
            _player = SpawnPlayer();
    }

    private void Start()
    {
        _levelHolder.SetUpRooms();
        _levelHolder.SetUpBorders();
    }

    public Vector3 ConvertToWorldPosition(Vector2Int intPos) => _levelHolder.ConvertToWorldPosition(intPos);

    public Room GetNeighbour(Vector2Int roomPos, NeighbourSide side)
    {
        switch (side)
        {
            case NeighbourSide.Left:
                return _levelHolder.GetRoom(roomPos + new Vector2Int(-1, 0));
            case NeighbourSide.Right:
                return _levelHolder.GetRoom(roomPos + new Vector2Int(1, 0));
            case NeighbourSide.LeftDown:
                return _levelHolder.GetRoom(roomPos + new Vector2Int((roomPos.y % 2 == 0) ? -1 : 0, -1));
            case NeighbourSide.RightDown:
                return _levelHolder.GetRoom(roomPos + new Vector2Int((roomPos.y % 2 != 0) ? 1 : 0, -1));
            case NeighbourSide.LeftUp:
                return _levelHolder.GetRoom(roomPos + new Vector2Int((roomPos.y % 2 == 0) ? -1 : 0, 1));
            case NeighbourSide.RightUp:
                return _levelHolder.GetRoom(roomPos + new Vector2Int((roomPos.y % 2 != 0) ? 1 : 0, 1));
            default:
                Debug.LogError("Incorrect switch case");
                return null;
        }
    }

    public List<Room> GetAllNeighbours(Vector2Int roomPos)
    {
        var neighbours = new List<Room>();
        Room room = GetNeighbour(roomPos, NeighbourSide.Left);
        if (room != null)
            neighbours.Add(room);
        room = GetNeighbour(roomPos, NeighbourSide.Right);
        if (room != null)
            neighbours.Add(room);
        room = GetNeighbour(roomPos, NeighbourSide.LeftUp);
        if (room != null)
            neighbours.Add(room);
        room = GetNeighbour(roomPos, NeighbourSide.LeftDown);
        if (room != null)
            neighbours.Add(room);
        room = GetNeighbour(roomPos, NeighbourSide.RightUp);
        if (room != null)
            neighbours.Add(room);
        room = GetNeighbour(roomPos, NeighbourSide.RightDown);
        if (room != null)
            neighbours.Add(room);
        return neighbours;
    }

    public Room GetNearestRoom(Vector3 position)
    {
        Collider2D[] availableRooms = GetAllRoomsInArea(position);
        if (availableRooms.Length == 0)
        {
            throw new Exception("No rooms near by (maybe increasing _roomDetectionRadius will help)");
        }
        Collider2D closestRoom = null;
        float minDistance = float.PositiveInfinity;
        foreach(var col in availableRooms)
        {
            float dist = Vector2.Distance(col.transform.position, position);
            if (minDistance > dist)
            {
                closestRoom = col;
                minDistance = dist;
            }
        }
        return closestRoom.GetComponent<Room>();
    }


    public Room GetNearestRoom(Vector3 position, Predicate<Room> filter)
    {
        Room minRoom = null;
        float minValue = float.PositiveInfinity;
        foreach (var room in _levelHolder.Rooms)
        {
            if (!filter(room))
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

    public void RevealFog(Vector2Int roomPos, Vector3 direction)
    {
        var room = _levelHolder.GetRoom(roomPos);
        room.RevealFog(direction);
    }

    private Collider2D[] GetAllRoomsInArea(Vector3 position)
    {
        return Physics2D.OverlapCircleAll(position, _roomDetectionRadius);
    }

    private float GetRoomDistance(Room room, Vector3 position) => Vector2.Distance(room.transform.position, position);

    private GameObject SpawnPlayer()
    {
        if (_levelHolder == null)
            _levelHolder = GetComponentInChildren<LevelHolder>();
        if (_spawned)
            return null;
        _spawned = true;
        return Instantiate(_playerPrefab, _levelHolder.ConvertToWorldPosition(_playerStartingCoord), Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _roomDetectionRadius);
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
