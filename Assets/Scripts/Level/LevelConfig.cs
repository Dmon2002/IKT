using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//TODO width, height, complex tiles (2x2, 2x3...)
[CreateAssetMenu(fileName = "New LevelConfig", menuName = "LevelConfig", order = 3)]
public class LevelConfig : ScriptableObject
{
    [SerializeReference] private List<TileConfig> _tileConfigs;
    [SerializeField] private List<TileConfigConst> _tileConfigConsts;
    [SerializeField] private List<TileConfigRandomFromList> _tileConfigFromList;
    [SerializeReference]
    private TileConfig _defaultTile;

    private Dictionary<Vector2Int, TileConfig> _coordToConfig = new Dictionary<Vector2Int, TileConfig>();

    private void Initialize()
    {
        // TODO Check for contains
        foreach (var config in _tileConfigConsts)
        {
            _coordToConfig[config.Coord] = config;
        }
        foreach (var config in _tileConfigFromList)
        {
            _coordToConfig[config.Coord] = config;
        }
    }

    public GameObject GetPrefab(Vector2Int coord)
    {
        if (_coordToConfig.Count == 0)
        {
            Initialize();
        }
        if (!_coordToConfig.ContainsKey(coord))
        {
            return _defaultTile.GetPrefab();
        }
        return _coordToConfig[coord].GetPrefab();
    }

    [Serializable]
    private class TileConfig
    {
        [SerializeField] private Vector2Int _coord;

        public Vector2Int Coord => _coord;

        public virtual GameObject GetPrefab()
        {
            return null;
        }
    }

    [Serializable]
    private class TileConfigConst : TileConfig
    {
        [SerializeField] private GameObject _tilePrefab;

        public override GameObject GetPrefab()
        {
            return _tilePrefab;
        }
    }

    [Serializable]
    private class TileConfigRandomFromList : TileConfig
    {
        [SerializeField] private List<GameObject> _tilePrefabs;

        public override GameObject GetPrefab()
        {
            return _tilePrefabs[Random.Range(0, _tilePrefabs.Count)];
        }
    }
}
