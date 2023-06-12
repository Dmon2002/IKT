using System;
using UnityEngine;

/// <summary>
/// Дата-класс предназначенный для хранения одного стата какого-либо объекта, у которого могут быть статы.
/// Зачастую храниться внутри StatContainer
/// </summary>
[Serializable]
public class Stat
{
    public const string MovementSpeedStatName = "MoveSpeed";
    public const string CanRevealFogStatName = "CanReveal";
    public const string TeamStatName = "Team";
    public const string AbilityCooldownName = "AbilityCooldown";
    public const string AbilityAttackRadiusName = "AbilityAttackRadius";

    // Потом через скрипты буду убирать Serialize fields в зависимости от _type
    [SerializeField] private StatConfig _config; 
    [SerializeField] private float _floatValue;
    [SerializeField] private int _intValue;
    [SerializeField] private bool _boolValue;
    // Потом буду превращать _intValue в enum в инспекторе
    [SerializeField] private EnumType _enumType;
    [SerializeField] private string _stringValue;

    public string Name => _config.Name;

    public StatType StatType => _config.Type;

    public float FloatValue
    {
        get
        {
            if (StatType != StatType.Float)
                throw new System.ArgumentException("Not float type value");
            return _floatValue;
        }
        set
        {
            if (StatType != StatType.Float)
                throw new System.ArgumentException("Not float type value");
            _floatValue = value;
        }
    }

    public int IntValue
    {
        get
        {
            if (StatType != StatType.Int)
                throw new System.ArgumentException("Not int type value");
            return _intValue;
        }
        set
        {
            if (StatType != StatType.Int)
                throw new System.ArgumentException("Not int type value");
            _floatValue = value;
        }
    }

    public bool BoolValue
    {
        get
        {
            if (StatType != StatType.Bool)
                throw new System.ArgumentException("Not bool type value");
            return _boolValue;
        }
        set
        {
            if (StatType != StatType.Bool)
                throw new System.ArgumentException("Not bool type value");
            _boolValue = value;
        }
    }

    public int EnumValue
    {
        get
        {
            if (StatType != StatType.Enum)
                throw new System.ArgumentException("Not enum type value");
            return _intValue;
        }
        set
        {
            if (StatType != StatType.Enum)
                throw new System.ArgumentException("Not enum type value");
            _intValue = value;
        }
    }

    public string StringValue
    {
        get
        {
            if (StatType != StatType.String)
                throw new System.ArgumentException("Not string type value");
            return _stringValue;
        }
        set
        {
            if (StatType != StatType.String)
                throw new System.ArgumentException("Not string type value");
            _stringValue = value;
        }
    }

    public void ChangeValue(float change)
    {
        if (StatType != StatType.Float)
            throw new System.ArgumentException("Not float type value");
        _floatValue += change;
    }

    public void ChangeValue(int change)
    {
        if (StatType != StatType.Int)
            throw new System.ArgumentException("Not int type value");
        _intValue += change;
    }

    public void ChangeValue(Stat sub)
    {
        if (sub.Name != Name)
            throw new System.ArgumentException("Incompatible stats by name");
        if (sub.StatType == StatType.Float)
        {
            _floatValue += sub.FloatValue;
        }
        else if (sub.StatType == StatType.Int)
        {
            _intValue += sub.IntValue;
        } 
        else
        {
            throw new System.ArgumentException("Attempt to sub not number type");
        }
    }

    public void SetValue(Stat sub)
    {
        if (sub.Name != Name)
            throw new System.ArgumentException("Incompatible stats by name");
        switch (StatType)
        {
            case StatType.Float:
                _floatValue = sub.FloatValue;
                break;
            case StatType.Int:
            case StatType.Enum:
                _intValue = sub.IntValue;
                break;
            case StatType.Bool:
                _boolValue = sub.BoolValue;
                break;
            case StatType.String:
                _stringValue = sub.StringValue;
                break;

        }
    }

    public Stat CloneStat()
    {
        Stat clonedStat = new ();
        clonedStat._config = _config;
        clonedStat._floatValue = _floatValue;
        clonedStat._enumType = _enumType;
        clonedStat._intValue = _intValue;
        clonedStat._boolValue = _boolValue;
        clonedStat._stringValue = _stringValue;
        return clonedStat;
    }
}



public enum EnumType
{
    Team
}