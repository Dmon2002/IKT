using Sirenix.OdinInspector;
using UnityEngine;

namespace StatSystem
{
    [CreateAssetMenu(fileName = "New Stat", menuName = "StatConfig", order = 1)]
    public class StatConfig : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private StatType _statType;
        [ShowIf("_statType", StatType.Float)]
        [SerializeField] private bool _isAttribute;

        public string Name => _name;

        public StatType Type => _statType;

        public bool IsAttribute => _isAttribute;

        public StatConfig(string name, StatType statType)
        {
            _name = name;
            _statType = statType;
        }
    }

    public enum StatType
    {
        Float,
        Int,
        Bool,
        Enum,
        String
    }
}