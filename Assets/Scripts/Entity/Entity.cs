using Sirenix.OdinInspector;
using StatSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(StatContainer))]
public abstract class Entity : MonoBehaviour
{
    [ChildGameObjectsOnly]
    [SerializeField] private Transform _activeAbilitiesContainer;
    [ChildGameObjectsOnly]
    [SerializeField] private Transform _passiveAbilitiesContainer;

    private List<BaseActiveAbility> _activeAbilities;
    private List<PassiveAbility> _passiveAbilities;
    private StatContainer _statContainer;

    private Rigidbody2D _rb;

    public event Action<Collision2D> Collided;

    public Rigidbody2D Rb => _rb;

    public StatContainer StatContainer => _statContainer;
    
    public event System.Action Died;

    protected virtual void Awake()
    {
        _activeAbilities = new List<BaseActiveAbility>();
        if (_activeAbilitiesContainer != null)
        {
            _activeAbilities = _activeAbilitiesContainer?.GetComponentsInChildren<BaseActiveAbility>().ToList();
        }
        _passiveAbilities = new List<PassiveAbility>();
        if (_passiveAbilitiesContainer != null)
        {
            _passiveAbilities = _passiveAbilitiesContainer?.GetComponentsInChildren<PassiveAbility>().ToList();
        }
        _rb = GetComponent<Rigidbody2D>();
        _statContainer = GetComponent<StatContainer>();
        foreach (var ability in _activeAbilities)
        {
            ability.SetEntity(this);
        }
        foreach (var ability in _passiveAbilities)
        {
            ability.SetEntity(this);
        }
    }

    private void Update()
    {
        if (!_statContainer.ContainsStat(StatNames.HP))
            return;
        if (_statContainer.GetStat<float>(StatNames.HP) <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Died?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collided?.Invoke(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Collided?.Invoke(collision);
    }

#if UNITY_EDITOR
    [Button("Setup Stats")]
    protected virtual void SetupStats()
    {
        var statContainer = GetComponent<StatContainer>();
        statContainer.AddStat(new Stat(StatNames.CanReveal, StatType.Bool));
        statContainer.AddStat(new Stat(StatNames.HP, StatType.Float, true));
        statContainer.AddStat(new Stat(StatNames.Team, StatType.Enum));
    }
#endif
}
