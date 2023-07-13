using Sirenix.OdinInspector;
using StatSystem;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbility : BaseActiveAbility
{
    [SerializeField] private AbilityAction _action;
    [SerializeField] private bool _isDirectional;
    [ShowIf("_isDirectional")]
    [SerializeField] private AbilityDecisionDirection _directionDecision;
    [EnumToggleButtons]
    [SerializeField] private ActivationMethod _methodActivateOn;
    [ShowIf("@(_action & AbilityAction.SpawnsAttack) == AbilityAction.SpawnsAttack")]
    [SerializeField] private List<Attack> _attackPrefabs;
    [ShowIf("@(_action & AbilityAction.EffectSelf) == AbilityAction.EffectSelf")]
    [SerializeField] private List<Effect> _effects;
    
    //private CooldownReload _abilityCooldownReload;
    private Vector2 _lastDirection;


    private void Update()
    {
        if (_methodActivateOn != ActivationMethod.Update)
            return;
        Cast();
    }

    private void FixedUpdate()
    {
        if (_methodActivateOn != ActivationMethod.FixedUpdate)
            return;
        Cast();
    }

    private void LateUpdate()
    {
        if (_methodActivateOn != ActivationMethod.LateUpdate)
            return;
        Cast();
    }


    protected override void Cast()
    {
        if (!IsActive)
            return;
        if (!CanActivate)
            return;
        base.Cast();
        if (_isDirectional)
        {
            DefineDirection();
        }
        if ((_action & AbilityAction.SpawnsAttack) == AbilityAction.SpawnsAttack)
        {
            foreach (var attackPrefab in _attackPrefabs)
            {
                var attack = Instantiate(attackPrefab, transform.position, Quaternion.identity);
                if (_isDirectional)
                {
                    attack.SetDirection(_lastDirection);
                }
            }
        }
        if ((_action & AbilityAction.EffectSelf) == AbilityAction.EffectSelf)
        {
            foreach (var effect in _effects)
            {
                effect.ApplyEffect(Entity.StatContainer);
            }
        }
    }

    private void DefineDirection()
    {
        _lastDirection = _directionDecision.DecideDirection();
    }

    private enum ActivationMethod
    {
        Update,
        FixedUpdate,
        LateUpdate
    }

    [Flags]
    private enum AbilityAction
    {
        SpawnsAttack = 1 << 0,
        EffectSelf = 1 << 1,
        EffectLevel = 1 << 2,
    }
}
