using System;
using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
    /// <summary>
    /// Дата-класс предназначенный для хранения одного стата какого-либо объекта, у которого могут быть статы.
    /// Зачастую храниться внутри StatContainer
    /// </summary>
    [Serializable]
    internal sealed class Stat
    {

        [SerializeField] private StatConfig _config;
        public float FloatValue;
        public int IntValue;
        public bool BoolValue;
        public EnumType EnumType;
        public string StringValue;

        public string Name => _config.Name;

        public StatType StatType => _config.Type;

        public void ChangeValue<T>(T change)
        {
            if (StatType != StatType.Float || StatType != StatType.Int)
                throw new System.ArgumentException("not int and not float type value to change");

        }

        public void ChangeValue(Stat add)
        {
            if (add.Name != Name)
                throw new System.ArgumentException("Incompatible stats by name");
            if (add.StatType == StatType.Float)
            {
                FloatValue += add.FloatValue;
            }
            else if (add.StatType == StatType.Int)
            {
                IntValue += add.IntValue;
            }
            else
            {
                throw new System.ArgumentException("Attempt to add` not number type");
            }
        }

        public void SetValue(Stat set)
        {
            if (set.Name != Name)
                throw new System.ArgumentException("Incompatible stats by name");
            switch (StatType)
            {
                case StatType.Float:
                    FloatValue = set.FloatValue;
                    break;
                case StatType.Int:
                case StatType.Enum:
                    IntValue = set.IntValue;
                    break;
                case StatType.Bool:
                    BoolValue = set.BoolValue;
                    break;
                case StatType.String:
                    StringValue = set.StringValue;
                    break;

            }
        }

        public T GetValue<T>()
        {
            Type tType = typeof(T);
            StatType predictedType;
            object value;
            if (tType == typeof(float))
            {
                predictedType = StatType.Float;
                value = FloatValue;
            }
            else if (tType.IsEnum)
            {
                predictedType = StatType.Enum;
                value = IntValue;
            }
            else if (tType == typeof(int))
            {
                predictedType = StatType.Int;
                value = IntValue;
            }
            else if (tType == typeof(bool))
            {
                predictedType = StatType.Bool;
                value = BoolValue;
            }
            else if (tType == typeof(string))
            {
                predictedType = StatType.String;
                value = StringValue;
            }
            else
            {
                throw new Exception("No such type - " + tType.Name);
            }
            if (predictedType != _config.Type)
            {
                throw new ArgumentException("Type doesn't match:\n\tExpected: " + _config.Type.ToString() + "\tActual: " + _config.Type.ToString("g"));
            }
            return (T)value;
        }

        public Stat CloneStat()
        {
            Stat clonedStat = new();
            clonedStat._config = _config;
            clonedStat.FloatValue = FloatValue;
            clonedStat.EnumType = EnumType;
            clonedStat.IntValue = IntValue;
            clonedStat.BoolValue = BoolValue;
            clonedStat.StringValue = StringValue;
            return clonedStat;
        }
    }

    public enum EnumType
    {
        Team
    }
}