using System.Collections;
using UnityEngine;

namespace StatSystem
{
    public class EffectImposer : MonoBehaviour
    {
        private Effect _effectStat;

        private StatContainer _statContainer;

        protected StatContainer StatContainer => _statContainer;

        protected Effect EffectStat => _effectStat;

        public void SetStatContainer(StatContainer statContainer)
        {
            _statContainer = statContainer;
            transform.SetParent(statContainer.transform);
        }

        public void SetEffect(Effect effect)
        {
            _effectStat = effect;
        }

        protected virtual void Start()
        {
            ApplyEffect();
        }

        protected virtual void ApplyEffect()
        {
            if (_statContainer == null) return;
            _statContainer.ApplyStatChange(_effectStat.StatChange, true);
            if (_effectStat.Duration != 1)
            {
                StartCoroutine(LifeTimeCoroutine());
            }
        }

        protected virtual void RevertEffect()
        {
            _statContainer.RevertStatChange(_effectStat.StatChange);
        }

        private IEnumerator LifeTimeCoroutine()
        {
            yield return new WaitForSeconds(_effectStat.Duration);
            RevertEffect();
            Destroy(gameObject);
        }
    }
}