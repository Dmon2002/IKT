using System;
using UnityEngine;
using Sirenix.OdinInspector;
using Newtonsoft.Json.Linq;

namespace StatSystem
{
    /// <summary>
    /// Дата-класс предназначенный для хранения одного стата какого-либо объекта, у которого могут быть статы.
    /// Зачастую храниться внутри StatContainer
    /// </summary>
    [Serializable]
    public sealed class Stat
    {
#if UNITY_EDITOR
        [OnValueChanged("SetStatConfig")]
        [ShowIf("@_name == \"\"")]
        [SerializeField] private StatConfig _config;

        private void SetStatConfig()
        {
            if (_config == null) return;
            _name = _config.Name;
            _type = _config.Type;
            _isAttribute = _config.IsAttribute;
            _config = null;
        }
#endif
        [ReadOnly]
        [ShowIf("@_name != \"\"")]
        [SerializeField] private string _name;
        [ReadOnly]
        [ShowIf("@_name != \"\"")]
        [SerializeField] private StatType _type;
        [ShowIf("_type", StatType.Float)]
        [ReadOnly]
        [SerializeField] private bool _isAttribute;
        [ShowIf("_type", StatType.Float)]
        public float FloatValue;
        [ShowIf("@_type == StatSystem.StatType.Int || _type == StatSystem.StatType.Enum")]
        public int IntValue;
        [ShowIf("_type", StatType.Bool)]
        public bool BoolValue;
        [ShowIf("_type", StatType.Enum)]
        public EnumType EnumType;
        [ShowIf("_type", StatType.String)]
        public string StringValue;
        
        [ShowIf("@_type == StatSystem.StatType.Float && _isAttribute == true")]
        [SerializeField] private float _minValue;
        [ShowIf("@_type == StatSystem.StatType.Float && _isAttribute == true")]
        [SerializeField] private float _maxValue;

        public float MaxValue
        {
            get
            {
                if (_type != StatType.Float || !_isAttribute)
                    throw new Exception($"Value {_name} is not attribute. So it doesn't have max");
                return _maxValue;
            }
        }

        public float MinValue
        {
            get
            {
                if (_type != StatType.Float || !_isAttribute)
                    throw new Exception($"Value {_name} is not attribute. So it doesn't have min");
                return _minValue;
            }
        }

        [Button("Reset Stat", ButtonSizes.Small)]
        private void ResetStat()
        {
            _name = "";
            _type = StatType.Float;
        }

        public string Name => _name;

        public StatType StatType => _type;

        public Stat(string name, StatType type, bool isAttribute = false)
        {
            _name = name;
            _type = type;
            _isAttribute = isAttribute;
        }

        public void ChangeValue<T>(T change)
        {
            //if (StatType != StatType.Float || StatType != StatType.Int)
            //    throw new System.ArgumentException("not int and not float type value to change");
        }

        public void ChangeValue(Stat add)
        {
            if (add.Name != Name)
                throw new System.ArgumentException("Incompatible stats by name");
            if (add.StatType == StatType.Float)
            {
                FloatValue -= add.FloatValue;
                if (_isAttribute)
                {
                    FloatValue = Mathf.Clamp(FloatValue, _minValue, _maxValue);
                }
            }
            else if (add.StatType == StatType.Int)
            {
                IntValue -= add.IntValue;
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
                    FloatValue = Mathf.Clamp(set.FloatValue, _minValue, _maxValue);
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
            if (predictedType != _type)
            {
                throw new ArgumentException("Type doesn't match:\n\tExpected: " + _type.ToString() + "\tActual: " + _type.ToString("g"));
            }
            return (T)value;
        }

        public Stat CloneStat()
        {
            Stat clonedStat = new(_name, _type, _isAttribute);
            clonedStat.FloatValue = FloatValue;
            clonedStat.EnumType = EnumType;
            clonedStat.IntValue = IntValue;
            clonedStat.BoolValue = BoolValue;
            clonedStat.StringValue = StringValue;
            clonedStat._maxValue = _maxValue;
            clonedStat._minValue = _minValue;
            return clonedStat;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Stat other = (Stat)obj;

            return _name == other._name;
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }

    }

    public enum EnumType
    {
        Team
    }
}