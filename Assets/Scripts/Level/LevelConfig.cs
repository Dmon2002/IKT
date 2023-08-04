using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

//TODO width, height, complex tiles (2x2, 2x3...)
[CreateAssetMenu(fileName = "New LevelConfig", menuName = "LevelConfig", order = 3)]
public class LevelConfig : ScriptableObject, ILevelGrid
{
    [PropertyOrder(-2)]
    [SerializeField] private int _width;
    [SerializeField] private List<LevelSector> _sectors;

#if UNITY_EDITOR
    [ShowInInspector, PropertyOrder(-1)]
    private int HeightInspector => _sectors.Sum(s => s.Height);
#endif

    private int _height = -1;

    public int Height
    {
        get
        {
            if (_height == -1)
            {
                _height = _sectors.Sum(sector => sector.Height);
            }
            return _height;
        }
    }

    public int Width => _width;

    

    public bool ContainsCoord(Vector2Int coord)
    {
        return coord.x < Width && coord.y < Height && coord.x >= 0 && coord.y >= 0;
    }

    public GameObject GetPrefab(Vector2Int coord)
    {
        if (!ContainsCoord(coord))
        {
            Debug.LogError($"coord {coord} doesn't fit into {new Vector2Int(Width, Height)}");
            return null;
        }
        int pastHeight = 0;
        foreach (var sector in _sectors)
        {
            var relativeCoord = new Vector2Int(coord.x, coord.y - pastHeight);
            if (sector.ContainsCoord(relativeCoord))
            {
                return sector.GetPrefab(relativeCoord);
            }
            pastHeight += sector.Height;
        }
        return null;
    }

    //[SerializeReference] private List<TileConfig> _tileConfigs;
    //[SerializeField] private List<TileConfigConst> _tileConfigConsts;
    //[SerializeField] private List<TileConfigRandomFromList> _tileConfigFromList;
    //[SerializeReference]
    //private TileConfig _defaultTile;

    //private Dictionary<Vector2Int, TileConfig> _coordToConfig = new Dictionary<Vector2Int, TileConfig>();

    //private void Initialize()
    //{
    //    // TODO Check for contains
    //    foreach (var config in _tileConfigConsts)
    //    {
    //        _coordToConfig[config.Coord] = config;
    //    }
    //    foreach (var config in _tileConfigFromList)
    //    {
    //        _coordToConfig[config.Coord] = config;
    //    }
    //}

    //public GameObject GetPrefab(Vector2Int coord)
    //{
    //    if (_coordToConfig.Count == 0)
    //    {
    //        Initialize();
    //    }
    //    if (!_coordToConfig.ContainsKey(coord))
    //    {
    //        return _defaultTile.GetPrefab();
    //    }
    //    return _coordToConfig[coord].GetPrefab();
    //}

    //[Serializable]
    //private class TileConfig
    //{
    //    [SerializeField] private Vector2Int _coord;

    //    public Vector2Int Coord => _coord;

    //    public virtual GameObject GetPrefab()
    //    {
    //        return null;
    //    }
    //}

    //[Serializable]
    //private class TileConfigConst : TileConfig
    //{
    //    [SerializeField] private GameObject _tilePrefab;

    //    public override GameObject GetPrefab()
    //    {
    //        return _tilePrefab;
    //    }
    //}

    //[Serializable]
    //private class TileConfigRandomFromList : TileConfig
    //{
    //    [SerializeField] private List<GameObject> _tilePrefabs;

    //    public override GameObject GetPrefab()
    //    {
    //        return _tilePrefabs[Random.Range(0, _tilePrefabs.Count)];
    //    }
    //}
}
