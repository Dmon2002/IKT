using StatSystem;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovement : BaseActiveAbility
{
    [SerializeField] private AbilityDecisionDirection _directionDecision;

    private float _defaultSpeed;

    public float Speed => _defaultSpeed;

    private Vector2 _lastDirection;

    public Vector2 LastDirection => _lastDirection;

    private void FixedUpdate()
    {
        DefineDirection();
        Cast();
    }

    protected override void Cast()
    {
        base.Cast();
        Entity.Rb.velocity = _lastDirection * Entity.StatContainer.GetStat<float>(StatNames.MoveSpeed);
    }

    private void DefineDirection()
    {
        _lastDirection = _directionDecision.DecideDirection();
    }
}