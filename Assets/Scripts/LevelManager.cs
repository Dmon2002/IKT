using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Vector2Int leftDownStartingTile;
    [SerializeField] private int levelWidth;
    [SerializeField] private int levelHeight;

    private Tilemap levelTilemap;
    private LevelHolder levelHolder;

    private static LevelManager _instance;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LevelManager>();
            }
            return _instance;
        }
    }

    public Room GetRoom(Vector2Int roomPos) => levelHolder.GetRoom(roomPos);

    private void Awake()
    {
        levelHolder = GetComponentInChildren<LevelHolder>();
        levelTilemap = GetComponentInChildren<Tilemap>();
    }

    private void Start()
    {
        levelHolder.SetUpRooms(leftDownStartingTile, levelWidth, levelHeight);
    }

    public void RevealFog(Vector2Int roomCoords)
    {
        levelHolder.GetRoom(roomCoords).RevealFog();
    }

    public void RevealFog(Room room)
    {
        RevealFog(room.Coords);
    }

    public Room getNeighbour(Vector2Int roomPos, NeighbourSide side)
    {
        Vector2Int offset;
        switch (side)
        {
            case NeighbourSide.LeftUp:
                offset = new Vector2Int(-1, 1);
                break;
            case NeighbourSide.RightUp:
                offset = new Vector2Int(0, 1);
                break;
            case NeighbourSide.LeftDown:
                offset = new Vector2Int(-1, -1);
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
        return levelHolder.GetRoom(roomPos + offset);
    }

    public List<Room> getAllNeighbours(Vector2Int roomPos)
    {
        return new List<Room>() {
            getNeighbour(roomPos, NeighbourSide.Left),
            getNeighbour(roomPos, NeighbourSide.Right),
            getNeighbour(roomPos, NeighbourSide.LeftUp),
            getNeighbour(roomPos, NeighbourSide.LeftDown),
            getNeighbour(roomPos, NeighbourSide.RightUp),
            getNeighbour(roomPos, NeighbourSide.RightDown)
        };
    }

    private float GetRoomDistance(Room room, Vector3 position) => Vector2.Distance(room.transform.position, position);

    public Room GetNearestRoom(Vector3 position)
    {
        Room minRoom = null;
        float minValue = float.PositiveInfinity;
        foreach (var room in levelHolder.Rooms)
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
