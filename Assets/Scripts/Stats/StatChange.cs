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
        
        internal void ApplyStatChange(Stat stat)
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
    }

    public enum ChangeType
    {
        Set,
        Change
    }
}