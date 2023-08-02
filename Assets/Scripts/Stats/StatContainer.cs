using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StatSystem
{
    public class StatContainer : MonoBehaviour
    {
        [SerializeField] private List<Stat> _startingStats;

        private Dictionary<string, Stat> _baseStats = new();

        private Dictionary<string, Stat> _actualStats = new();

        private Dictionary<string, List<StatChange>> _changes = new();

        private Dictionary<string, Func<float, float>> _formulas = new();

        public event Action<string, float> StatEffected;

        [ShowInInspector]
        private List<Stat> ActualStats => _actualStats.Values.ToList();

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            foreach (var stat in _startingStats)
            {
                _baseStats[stat.Name] = stat;
                _changes[stat.Name] = new();
                _actualStats[stat.Name] = stat.CloneStat();
            }
        }

        public void AddFormula(string name, Func<float, float> formula)
        {
            _formulas[name] = formula;
        }

        public float ProcessFormula(string name, float value)
        {
            if (!_formulas.ContainsKey(name))
            {
                return value;
            }
            return _formulas[name](value);
        }

        public void ApplyStatChange(StatChange change, bool isTemporarely)
        {
            if (_baseStats.Count != _startingStats.Count)
                throw new Exception("Had to initialize statContainer before using");
            if (!_changes.ContainsKey(change.StatName))
                return;
            StatEffected?.Invoke(change.StatName, change.StatSubstract.FloatValue);
            if (isTemporarely)
            {
                _changes[change.StatName].Add(change);
                change.ApplyStatChange(_actualStats[change.StatName]);
            }
            else
            {
                change.ApplyStatChange(_baseStats[change.StatName]);
            }
            change.ApplyStatChange(GetStat(change.StatName));
        }

        public void RevertStatChange(StatChange change)
        {
            if (!_changes.ContainsKey(change.StatName))
                return;
            _changes[change.StatName].Remove(change);
            var stat = _baseStats[change.StatName].CloneStat();
            _actualStats[change.StatName] = stat;
            foreach (var statChange in _changes[change.StatName])
            {
                statChange.ApplyStatChange(stat);
            }
        }

        private Stat GetStat(string name)
        {
            if (_baseStats.Count != _startingStats.Count)
                throw new Exception("Had to initialize statContainer before using(");
            if (!_actualStats.ContainsKey(name))
                return null;
            return _actualStats[name];
        }

        public T GetStat<T>(string name)
        {
            if (!_actualStats.ContainsKey(name))
                throw new ArgumentException($"StatContainer doesn't have {name} stat");
            return _actualStats[name].GetValue<T>();
        }

        public bool ContainsStat(string name)
        {
            return _actualStats.ContainsKey(name);
        }
        
        public void AddStat(Stat stat)
        {
            if (_startingStats.Contains(stat)) return;
            _startingStats.Add(stat);
        }

        public float GetMinValue(string name)
        {
            if (!_actualStats.ContainsKey(name))
                throw new ArgumentException($"StatContainer doesn't have {name} stat");
            return _actualStats[name].MinValue;
        }

        public float GetMaxValue(string name)
        {
            if (!_actualStats.ContainsKey(name))
                throw new ArgumentException($"StatContainer doesn't have {name} stat");
            return _actualStats[name].MaxValue;
        }
    }
}