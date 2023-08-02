using System;
using UnityEngine;

namespace StatSystem
{
    [System.Serializable]
    public class StatChange
    {
        [SerializeField] private ChangeType _changeType;
        [SerializeField] private Stat _statSubstract;

        public ChangeType ChangeType => _changeType;

        public string StatName => _statSubstract.Name;

        public Stat StatSubstract => _statSubstract;
        
        public void ApplyStatChange(Stat stat, Func<float, float> formula = null)
        {
            if (stat.Name != _statSubstract.Name)
                throw new System.ArgumentException("incompatible stat names");
            if (_changeType == ChangeType.Set)
            {
                stat.SetValue(_statSubstract);
            }
            else
            {
                stat.ChangeValue(_statSubstract);
            }
        }

        public void ChangeSubValue(float value)
        {
            _statSubstract.FloatValue = value;
        } 
    }

    public enum ChangeType
    {
        Set,
        Change
    }
}