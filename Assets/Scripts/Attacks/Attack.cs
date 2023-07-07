using StatSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(StatContainer))]
public abstract class Attack : MonoBehaviour
{
    [SerializeField] private float _lifetime;
    [SerializeField] private List<Effect> _effectStats;
    [SerializeField] private bool _dealsDamage;
    [SerializeField] private float _damage;

    private StatContainer _statContainer;

    public StatContainer StatContainer => _statContainer;

    protected Vector2 Direction { get; set; }

    private void Awake()
    {
        _statContainer = GetComponent<StatContainer>();
    }

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
        var ourTeam = _statContainer.GetStat<Team>(StatNames.Team);
        var enemyTeam = entity.StatContainer.GetStat<Team>(StatNames.Team);
        if (ourTeam.GetAgainstTeams().Contains(enemyTeam))
        {
            if (_dealsDamage)
            {
                entity.ApplyDamage(_damage);
            }
            foreach (var effectStat in _effectStats)
            {
                var effect = effectStat.SpawnEffect(transform.position);
                effect.SetStatContainer(entity.StatContainer);
                effect.SetEffect(effectStat);
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
