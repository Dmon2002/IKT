using System.Collections;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private EffectStat _effectStat;

    private Entity _entity;

    protected Entity Entity => _entity;

    protected EffectStat EffectStat => _effectStat;

    public void SetEntity(Entity entity)
    {
        _entity = entity;
        transform.SetParent(entity.transform);
    }

    public void SetEffectStat(EffectStat effectStat)
    {
        _effectStat = effectStat;
    }

    protected virtual void Start()
    {
        ApplyEffect();
    }

    protected virtual void ApplyEffect()
    {
        //_entity.StatContainer.ApplyStatChange(_effectStat.StatChange, _effectStat.IsTemporary);
        if (_effectStat.IsTemporary)
        {
            StartCoroutine(LifeTimeCoroutine());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void RevertEffect()
    {
        //_entity.StatContainer.RevertChange(_effectStat.StatChange);
    }

    private IEnumerator LifeTimeCoroutine()
    {
        yield return new WaitForSeconds(_effectStat.Duration);
        RevertEffect();
        Destroy(gameObject);
    }
}
