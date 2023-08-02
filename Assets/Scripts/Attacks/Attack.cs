using StatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatContainer))]
public abstract class Attack : MonoBehaviour
{
    [SerializeField] private List<Effect> _effectStats;
    [SerializeField] private float _lifetime;

    protected virtual void Start()
    {
        StartCoroutine(LifetimeCoroutine());
        Shoot();
    }

    private IEnumerator LifetimeCoroutine()
    {
        yield return new WaitForSeconds(_lifetime);
        Destroy(gameObject);
    }

    private StatContainer _statContainer;

    public StatContainer StatContainer => _statContainer;

    protected Vector2 Direction { get; set; }

    protected virtual void Awake()
    {
        _statContainer = GetComponent<StatContainer>();
    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
    }

    protected virtual void CollideEntity(Entity entity)
    {
        var ourTeam = _statContainer.GetStat<Team>(StatNames.Team);
        var enemyTeam = entity.StatContainer.GetStat<Team>(StatNames.Team);
        if (ourTeam.GetAgainstTeams().Contains(enemyTeam))
        {
            foreach (var effectStat in _effectStats)
            {
                effectStat.ApplyEffect(entity.StatContainer);
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
