using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class LevelSector : ILevelGrid
{
    [SerializeField] private int _height;
    [SerializeField] private List<LevelClusterCoord> _clusterCoords;
    [SerializeField] private List<LevelTileCoord> _tileCoords;
    [SerializeReference] private LevelTile _defaultTile;

    public int Height => _height;

    public bool ContainsCoord(Vector2Int coord)
    {
        return coord.y < _height && coord.y >= 0;
    }

    public GameObject GetPrefab(Vector2Int coord)
    {
        if (!ContainsCoord(coord))
            return null;
        foreach (var tileCoord in _tileCoords)
        {
            if (_tileCoords.Contains(tileCoord))
            {
                return tileCoord.LevelTile.GetPrefab();
            }
        }
        foreach (var clusterCoord in _clusterCoords)
        {
            var relativeCoord = coord - clusterCoord.Coord;
            if (clusterCoord.LevelCluster.ContainsCoord(relativeCoord))
            {
                return clusterCoord.LevelCluster.GetPrefab(relativeCoord);
            }
        }
        return _defaultTile?.GetPrefab();
    }

}

public interface ILevelGrid
{
    bool ContainsCoord(Vector2Int coord);

    GameObject GetPrefab(Vector2Int coord);

    
}

[Serializable]
public class LevelCluster : ILevelGrid
{
    [SerializeField] private Vector2Int _size;
    [SerializeReference] private LevelTile _defaultTile;
    [SerializeField] private List<LevelTileCoord> _tileCoords;

    public Vector2Int Size => _size;

    public GameObject GetPrefab(Vector2Int coord)
    {
        if (!ContainsCoord(coord))
            return null;
        for (int i = 0; i < _tileCoords.Count; i++)
        {
            if (_tileCoords[i].Coord == coord)
            {
                return _tileCoords[i].LevelTile.GetPrefab();
            }
        }
        return _defaultTile.GetPrefab();
    }

    public bool ContainsCoord(Vector2Int coord)
    {
        return coord.x < _size.x && coord.y < _size.y && coord.x >= 0 && coord.y >= 0;
    } 
}

[Serializable]
public struct LevelTileCoord
{
    [SerializeField] private Vector2Int _coord;
    [SerializeReference] public LevelTile _levelTile;

    public Vector2Int Coord => _coord;

    public LevelTile LevelTile => _levelTile;
}

[Serializable]
public struct LevelClusterCoord
{
    [SerializeField] private Vector2Int _coord;
    [SerializeField] public LevelCluster _levelCluster;

    public Vector2Int Coord => _coord;

    public LevelCluster LevelCluster => _levelCluster;
}

[Serializable]
public abstract class LevelTile : ILevelGrid
{ 
    public bool ContainsCoord(Vector2Int coord)
    {
        return coord == Vector2Int.zero;
    }

    public GameObject GetPrefab(Vector2Int coord)
    {
        return coord == Vector2Int.zero ? null : GetPrefab();
    }

    public abstract GameObject GetPrefab();
}

[Serializable]
public class TileConfigConst : LevelTile
{
    [AssetsOnly]
    [SerializeField] private GameObject _tilePrefab;

    public override GameObject GetPrefab()
    {
        return _tilePrefab;
    }
}

[Serializable]
public class TileConfigRandomFromList : LevelTile
{
    [AssetsOnly]
    [SerializeField] private List<GameObject> _tilePrefabs;

    public override GameObject GetPrefab()
    {
        return _tilePrefabs.Count != 0 ? _tilePrefabs[Random.Range(0, _tilePrefabs.Count)] : null;
    }
}
