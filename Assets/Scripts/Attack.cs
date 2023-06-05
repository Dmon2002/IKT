using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] private StatContainer _statContainer;
    [SerializeField] private float _lifetime;
    [SerializeField] private List<EffectStat> _effectStats;
    [SerializeField] private bool _dealsDamage;
    [SerializeField] private float _damage;

    public StatContainer StatContainer => _statContainer;

    protected Vector2 Direction { get; set; }

    private void Start()
    {
        Shoot();
        StartCoroutine(LifetimeCoroutine());
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    private IEnumerator LifetimeCoroutine()
    {
        yield return new WaitForSeconds(_lifetime);
        Destroy(gameObject);
    }

    protected virtual void CollideEntity(Entity entity)
    {
        var ourTeam = (Team)_statContainer.GetStatEnumValue(Stat.TeamStatName);
        var enemyTeam = (Team)entity.StatContainer.GetStatEnumValue(Stat.TeamStatName);
        if (ourTeam.GetAgainstTeams().Contains(enemyTeam))
        {
            if (_dealsDamage)
            {
                entity.ApplyDamage(_damage);
            }
            foreach (var effectStat in _effectStats)
            {
                var effect = effectStat.SpawnEffect(transform.position);
                effect.SetEntity(entity);
                effect.SetEffectStat(effectStat);
            }
        }
    }

    protected abstract void Shoot();
    //{
    //switch (_attackType)
    //{
    //    case AttackType.Melee:
    //        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, Utilities.ToAngle(direction)));
    //        break;
    //    case AttackType.Projectile:
    //        _currentSpeed = _statContainer.GetStatFloatValue(Stat.MovementSpeedStatName);

    //}
    //}
}
