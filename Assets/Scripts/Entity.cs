using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] private float _maxhp;
    [SerializeField] private StatContainer _statContainer;

    private float _hp;

    public float MaxHp => _maxhp;

    public float Hp => _hp;

    public StatContainer StatContainer => _statContainer;

    public event Action HealthChanged;
    public event Action Died;

    protected virtual void Awake()
    {
        _statContainer.Initialize();
    }

    protected virtual void OnEnable()
    {
        _hp = _maxhp;
    }

    public Stat getStat(string name) => _statContainer.GetStat(name);

    public virtual void ApplyDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentOutOfRangeException("Damage by negative number");
        _hp -= damage;
        HealthChanged?.Invoke();
        if (_hp < 0)
        {
            Die();
        }
    }

    public virtual void Heal(float healPower)
    {
        if (healPower < 0)
            throw new ArgumentOutOfRangeException("Heal by negative number");
        _hp += healPower;
        HealthChanged?.Invoke();
        if (_hp > _maxhp)
        {
            _hp = _maxhp;
        }
    }

    public virtual void Die()
    {
        Died?.Invoke();
    }
}
