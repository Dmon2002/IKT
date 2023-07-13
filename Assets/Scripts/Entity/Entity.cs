using Sirenix.OdinInspector;
using StatSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatContainer))]
public abstract class Entity : MonoBehaviour
{
    [SerializeField] private float _maxhp;

    private List<BaseActiveAbility> _activeAbilities;
    private List<PassiveAbility> _passiveAbilities;
    private StatContainer _statContainer;

    private Rigidbody2D _rb;

    private float _hp;

    public float MaxHp => _maxhp;

    public float Hp => _hp;

    public Rigidbody2D Rb => _rb;

    public StatContainer StatContainer => _statContainer;

    public event Action HealthChanged;
    public event Action Died;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _activeAbilities = new List<BaseActiveAbility>(transform.GetComponentsInChildren<BaseActiveAbility>());
        _passiveAbilities = new List<PassiveAbility>(transform.GetComponentsInChildren<PassiveAbility>());
        foreach (var ability in _activeAbilities)
        {
            ability.SetEntity(this);
        }
        foreach (var ability in _passiveAbilities)
        {
            ability.SetEntity(this);
        }
        _statContainer = GetComponent<StatContainer>();
    }

    protected virtual void OnEnable()
    {
        _hp = _maxhp;
    }

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
        gameObject.SetActive(false);
    }

    [Button("Setup Stats")]
    private void SetupStats()
    {
        var statContainer = GetComponent<StatContainer>();
        statContainer.AddStat(new Stat("MaxHP", StatType.Float));
    }
}
