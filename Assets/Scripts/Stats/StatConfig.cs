using UnityEngine;

[CreateAssetMenu(fileName = "New Stat", menuName = "StatConfig", order = 1)]
public class StatConfig : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private StatType _statType;

    public string Name => _name;

    public StatType Type => _statType;
}

public enum StatType
{
    Float,
    Int,
    Bool,
    Enum,
    String
}