using StatSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatContainer))]
public abstract class BaseActiveAbility : MonoBehaviour
{
    protected List<AbilityDecision> Decisions;

    private bool _isActive;
    private Entity _entity;

    public event Action Casted;

    public StatContainer StatContainer { get; protected set; }

    public bool IsActive => _isActive;

    public Entity Entity => _entity;

    public bool CanActivate
    {
        get
        {
            foreach (var decision in Decisions)
            {
                if (!decision.Decide())
                {
                    return false;
                }
            }
            return true;
        }
    }

    protected virtual void Awake()
    {
        StatContainer = GetComponent<StatContainer>();
        Decisions = new List<AbilityDecision>(GetComponentsInChildren<AbilityDecision>());
        foreach (var decision in Decisions)
        {
            decision.SetAbility(this);
        }
    }

    private void OnEnable()
    {
        _isActive = true;
    }

    private void OnDisable()
    {
        _isActive = false;
    }

    public void SetEntity(Entity entity)
    {
        _entity = entity;
    }

    public void Deactivate()
    {
        _isActive = false;
    }

    protected virtual void Cast()
    {
        Casted?.Invoke();
    }
}
