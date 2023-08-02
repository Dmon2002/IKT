using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace StatSystem
{
    [Serializable]
    public class Effect
    {
        [SerializeField] private bool _isPermanent;
        [AssetsOnly, ShowIf("@!_isPermanent")]
        [SerializeField] private EffectImposer _prefab;
        [ShowIf("@!_isPermanent")]
        [SerializeField] private float _duration;
        [SerializeField] private StatChange _statChange;

        public StatChange StatChange => _statChange;

        public float Duration => _duration;

        public void ApplyEffect(StatContainer container)
        {
            if (_isPermanent)
            {
                if (_statChange.StatSubstract.StatType == StatType.Float)
                {
                    _statChange.ChangeSubValue(container.ProcessFormula(_statChange.StatName, _statChange.StatSubstract.FloatValue));
                }
                container.ApplyStatChange(_statChange, false);
            }
            else
            {
                var imposer = GameObject.Instantiate(_prefab, container.transform.position, Quaternion.identity);
                imposer.SetEffect(this);
                imposer.SetStatContainer(container);
            }
            
        }
    }
}