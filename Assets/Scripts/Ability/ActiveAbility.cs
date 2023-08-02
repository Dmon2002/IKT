using Sirenix.OdinInspector;
using StatSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using ActionSystem;
using System.Collections;

public class ActiveAbility : BaseActiveAbility
{
    [SerializeField] private AbilityActionType _actionType;
    [SerializeField] private bool _isDirectional;
    [ShowIf("_isDirectional")]
    [SerializeField] private AbilityDecisionDirection _directionDecision;
    [EnumToggleButtons]
    [SerializeField] private ActivationMethod _methodActivateOn;
    [ShowIf("@(_actionType & AbilityActionType.SpawnsAttack) == AbilityActionType.SpawnsAttack")]
    [SerializeField] private List<Attack> _attackPrefabs;
    [ShowIf("@(_actionType & AbilityActionType.EffectSelf) == AbilityActionType.EffectSelf || (_actionType & AbilityActionType.EffectOtherEntity) == AbilityActionType.EffectOtherEntity")]
    [SerializeField] private List<Effect> _effects;
    [ShowIf("@(_actionType & AbilityActionType.EffectOtherEntity) == AbilityActionType.EffectOtherEntity")]
    [SerializeField] private AbilityDecisionEntity _entityDecision;
    [ShowIf("@(_actionType & AbilityActionType.PerformActions) == AbilityActionType.PerformActions")]
    [SerializeField] private ActionSystem.Actions _actions;

    private Vector2 _lastDirection;


    private void Update()
    {
        if (_methodActivateOn != ActivationMethod.Update)
            return;
        StartCoroutine(Cast());
    }

    private void FixedUpdate()
    {
        if (_methodActivateOn != ActivationMethod.FixedUpdate)
            return;
        StartCoroutine(Cast());
    }

    private void LateUpdate()
    {
        if (_methodActivateOn != ActivationMethod.LateUpdate)
            return;
        StartCoroutine(Cast());
    }


    protected override IEnumerator Cast()
    {
        if (!IsActive)
            yield break;
        if (Activated)
            yield break;
        if (!CanActivate)
            yield break;
        yield return StartCoroutine(base.Cast());
        if (_isDirectional)
        {
            DefineDirection();
        }
        if ((_actionType & AbilityActionType.SpawnsAttack) == AbilityActionType.SpawnsAttack)
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
        if ((_actionType & AbilityActionType.EffectSelf) == AbilityActionType.EffectSelf)
        {
            foreach (var effect in _effects)
            {
                effect.ApplyEffect(Entity.StatContainer);
            }
        }
        if ((_actionType & AbilityActionType.EffectOtherEntity) == AbilityActionType.EffectOtherEntity)
        {
            var entity = _entityDecision.DecideEntity();
            if (entity == null)
                yield break;
            foreach (var effect in _effects)
            {
                effect.ApplyEffect(entity.StatContainer);
            }
        }
        if ((_actionType & AbilityActionType.PerformActions) == AbilityActionType.PerformActions)
        {
            _actions.Perform();
        }
        Activated = false;
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
    private enum AbilityActionType
    {
        SpawnsAttack = 1 << 0,
        EffectSelf = 1 << 1,
        EffectLevel = 1 << 2,
        EffectOtherEntity = 1 << 3,
        PerformActions = 1 << 4
    }
}
