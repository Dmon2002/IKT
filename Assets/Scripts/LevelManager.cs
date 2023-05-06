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
        levelHolder.getRoom(roomCoords).RevealFog();
    }

    public void RevealFog(Room room)
    {
        RevealFog(room.Coords);
    }
}
