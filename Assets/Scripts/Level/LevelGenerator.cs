using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform _roomContainer;
    [SerializeField] private Transform _borderContainer;
    [SerializeField] private LevelHolder _levelHolder;
    [SerializeField] private GameObject _borderTilePrefab;
    [SerializeField] private int _startingTilesCount;
    [SerializeField] private GameObject _startingTilePrefab;
    [SerializeField] private LevelConfig _levelConfig;

    private Vector2Int _startingTileCoord;

    public void SetLevelConfiguration(Vector2Int startingTileCoord)
    {
        _startingTileCoord = startingTileCoord;
    }

    public GameObject GenerateRoom(Vector2Int roomPos)
    {
        var roomPrefab = _startingTilePrefab;
        if (roomPos.y - _startingTileCoord.y >= _startingTilesCount)
        {
            roomPrefab = _levelConfig.GetPrefab(roomPos);
        }
        return Instantiate(roomPrefab, _levelHolder.ConvertToWorldPosition(roomPos), Quaternion.identity, _roomContainer);
    }

    public GameObject GenerateBorder(Vector2Int borderPos)
    {
        return Instantiate(_borderTilePrefab, _levelHolder.ConvertToWorldPosition(borderPos), Quaternion.identity, _borderContainer);
    }

    
}
