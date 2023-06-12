using UnityEngine;

[System.Serializable]
public class StatChange
{
    [SerializeField] private ChangeType _changeType;
    [SerializeField] private Stat _statSubstract;

    public ChangeType ChangeType => _changeType;

    public Stat StatSubstract => _statSubstract;

    public string StatName => _statSubstract.Name;

    public void ApplyStatChange(Stat stat)
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
