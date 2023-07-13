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

        public void ApplyStatChange(StatChange change, bool isTemporarely)
        {
            if (_baseStats.Count != _startingStats.Count)
                throw new Exception("Had to initialize statContainer before using(");
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
            return _actualStats[name].GetValue<T>();
        }
        
        public void AddStat(Stat stat)
        {
            if (_startingStats.Contains(stat)) return;
            _startingStats.Add(stat);
        }
    }
}