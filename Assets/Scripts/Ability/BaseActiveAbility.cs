using StatSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatContainer))]
public abstract class BaseActiveAbility : MonoBehaviour
{
    [SerializeField] private bool _usesAbilityDelay;

    protected List<AbilityDecision> Decisions;

    private bool _isActive;
    private Entity _entity;

    public event System.Action Casted;

    public StatContainer StatContainer { get; protected set; }

    public bool IsActive => _isActive;

    public bool Activated { get; protected set; }

    public bool UsesAbilityDelay => _usesAbilityDelay;

    public Entity Entity {
        get
        {
            if (_entity == null)
                throw new Exception("Accessing ability without setting Entity (SetEntity)");
            return _entity;
        }
    }

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

    protected virtual IEnumerator Cast()
    {
        Activated = true;
        if (_usesAbilityDelay)
        {
            yield return new WaitForSeconds(StatContainer.GetStat<float>(StatNames.AbilityDelay));
            Debug.Log("CastDelay");
        }
        Casted?.Invoke();
        Activated = false;
    }
}
