using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatContainer : MonoBehaviour 
{
    [SerializeField] private List<Stat> _startingStats;

    private Dictionary<string, Stat> _baseStats = new();

    private Dictionary<string, Stat> _stats = new();

    private Dictionary<string, List<StatChange>> _changes = new();

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        foreach (var stat in _startingStats)
        {
            _baseStats[stat.Name] = stat;
            _changes[stat.Name] = new ();
        }
        _stats = new Dictionary<string, Stat>(_baseStats.ToDictionary(pair => pair.Key, pair => pair.Value.CloneStat()));
    }

    public void ApplyStatChange(StatChange change, bool isTemporarely)
    {
        if (_baseStats.Count != _startingStats.Count)
            throw new Exception("Had to initialize statContainer before using(");
        if (isTemporarely)
        {
            _changes[change.StatName].Add(change);
            change.ApplyStatChange(_stats[change.StatName]);
        } 
        else
        {
            change.ApplyStatChange(_baseStats[change.StatName]);
        }
        change.ApplyStatChange(GetStat(change.StatName));
    }

    public void RevertStatChange(StatChange change)
    {
        _changes[change.StatName].Remove(change);
        var stat = _baseStats[change.StatName].CloneStat();
        _stats[change.StatName] = stat;
        foreach (var statChange in _changes[change.StatName])
        {
            statChange.ApplyStatChange(stat);
        }
    }

    private Stat GetStat(string name)
    {
        if (_baseStats.Count != _startingStats.Count)
            throw new Exception("Had to initialize statContainer before using(");
        if (!_stats.ContainsKey(name))
            return null;
        return _stats[name];
    }

    private bool TryGetStat(string name, out Stat stat)
    {
        stat = GetStat(name);
        return true;
    }

    public float GetStatFloatValue(string name)
    {
        var stat = GetStat(name);
        if (stat == null)
            throw new ArgumentException("Stat " + name + " doesn't exist");
        if (stat.StatType != StatType.Float)
            throw new System.ArgumentException("Wrong StatType");
        return stat.FloatValue;
    }

    public int GetStatIntValue(string name)
    {
        var stat = GetStat(name);
        if (stat == null)
            throw new ArgumentException("Stat " + name + " doesn't exist");
        if (stat.StatType != StatType.Int)
            throw new System.ArgumentException("Wrong StatType");
        return stat.IntValue;
    }

    public bool GetStatBoolValue(string name)
    {
        var stat = GetStat(name);
        if (stat == null)
            throw new ArgumentException("Stat " + name + " doesn't exist");
        if (stat.StatType != StatType.Bool)
            throw new System.ArgumentException("Wrong StatType");
        return stat.BoolValue;
    }

    public int GetStatEnumValue(string name)
    {
        var stat = GetStat(name);
        if (stat == null)
            throw new ArgumentException("Stat " + name + " doesn't exist");
        if (stat.StatType != StatType.Enum)
            throw new System.ArgumentException("Wrong StatType");
        return stat.EnumValue;
    }

    public string GetStatStringValue(string name)
    {
        var stat = GetStat(name);
        if (stat == null)
            throw new ArgumentException("Stat " + name + " doesn't exist");
        if (stat.StatType != StatType.String)
            throw new System.ArgumentException("Wrong StatType");
        return stat.StringValue;
    }

    //public T GetStat<T>(string name)
    //{
    //    Type genericType = typeof(T);
    //    string a = _names[T];
    //    return default(T);
    //}
}
