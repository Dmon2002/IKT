using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _roomPrefabs;

    public Dictionary<Vector2Int, GameObject> Generate(Vector2Int leftUpStartingTile, int width, int height)
    {
        var rooms = new Dictionary<Vector2Int, GameObject>();
        for (int y = leftUpStartingTile.y; y < leftUpStartingTile.y + height; y++)
        {
            for (int x = leftUpStartingTile.x; x < leftUpStartingTile.x + width - y % 2; x++)
            {
                rooms[new Vector2Int(x, y)] = getRandomRoom();
            }
        }
        
        return rooms;
    }

    public GameObject getRandomRoom() => _roomPrefabs[Random.Range(0, _roomPrefabs.Count)];
}
