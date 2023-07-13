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
        [SerializeField] private bool _dealsDamage;
        [ShowIf("_dealsDamage")]
        [SerializeField] private float _damage;

        public StatChange StatChange => _statChange;

        public float Duration => _duration;

        public void ApplyEffect(StatContainer container)
        {
            var imposer = GameObject.Instantiate(_prefab, container.transform.position, Quaternion.identity);
            imposer.SetEffect(this);
            imposer.SetStatContainer(container);
        }
    }
}