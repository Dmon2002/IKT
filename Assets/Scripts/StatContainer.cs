using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatContainer
{
    [SerializeField] private List<Stat> _stats;

    private Dictionary<string, Stat> _nameToStat = new();

    private Dictionary<string, StatChange> _effects = new();
    
    public void Initialize()
    {
        foreach (var stat in _stats)
        {
            _nameToStat[stat.Name] = stat;
        }
    }

    public void ApplyStatChange(StatChange change)
    {
        change.ApplyStatChange(GetStat(change.StatName));
    }

    public Stat GetStat(string name)
    {
        if (_nameToStat.Count != _stats.Count)
        {
            Initialize();
        }
        if (!_nameToStat.ContainsKey(name))
            return null;
        return _nameToStat[name];
    }

    public bool TryGetStat(string name, out Stat stat)
    {
        stat = GetStat(name);
        return stat != null;
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
}
