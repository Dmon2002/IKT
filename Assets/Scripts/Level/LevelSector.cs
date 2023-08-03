using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;



public class LevelSector : LevelGrid
{
    //TODO Write validation for intersecting grids
    //[InlineEditor]
    [SerializeReference] private List<LevelGridCoord> _levelGridCoords;
    [AssetSelector]
    [SerializeReference] private TileConfig _defaultTile;

    // TODO test LevelSector(int width, int height)
    public LevelSector(Vector2Int size) : base(size)
    {
        
    }

    public override Room GetPrefab(Vector2Int coord)
    {
        if (coord.x < 0 || coord.y < 0)
        {
            Debug.LogError("Can't be non positive width or height - (" + coord.x + ", " + coord.y + ")");
        }
        for (int i = 0; i < _levelGridCoords.Count; i++)
        {
            Vector2Int relativeCoord = coord - _levelGridCoords[i].Coord;
            if (_levelGridCoords[i].LevelGrid.Contains(relativeCoord))
            {
                return _levelGridCoords[i].LevelGrid.GetPrefab(relativeCoord);
            }
        }
        return _defaultTile.GetPrefab();
    }
}

[Serializable]
public class LevelGridCoord
{
    [SerializeField] private Vector2Int _coord;
    [SerializeField] private LevelGrid _levelGrid;

    public Vector2Int Coord => _coord;

    public LevelGrid LevelGrid => _levelGrid;
}

[Serializable]
public abstract class LevelGrid
{ 
    private Vector2Int _size;

    public Vector2Int Size => _size;

    public LevelGrid(Vector2Int size)
    {
        if (size.x <= 0 || size.y <= 0)
        {
            Debug.LogError("Can't be non positive width or height - (" + size.x + ", " + size.y + ")");
        }
        _size = size;
    }

    public LevelGrid(int width, int height) : this(new Vector2Int(width, height)) { }

    public bool Contains(Vector2Int coord)
    {
        return coord.x >= 0 && coord.x < _size.x && coord.y >= 0 && coord.y < _size.y;
    }

    public abstract Room GetPrefab(Vector2Int coord);

    public Room GetPrefab()
    {
        return GetPrefab(Vector2Int.zero);
    }
}
[Serializable]
public class TileConfig : LevelGrid
{
    [AssetsOnly]
    [SerializeField] private Room _roomPrefab;

    public TileConfig() : base(Vector2Int.one) { }

    public override Room GetPrefab(Vector2Int coord)
    {
        return _roomPrefab;
    }
}
