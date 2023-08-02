using Sirenix.OdinInspector;
using StatSystem;
using System;
using System.Collections;
using UnityEngine;

public class EntityMovement : BaseActiveAbility
{
    [SerializeField] private AbilityDecisionDirection _directionDecision;
    [EnumToggleButtons]
    [SerializeField] private MovementType _movementType;

    private float _defaultSpeed;

    public float Speed => _defaultSpeed;

    private Vector2 _lastDirection;

    public Vector2 LastDirection => _lastDirection;

    protected override void Awake()
    {
        base.Awake();
    }

    private void FixedUpdate()
    {
        DefineDirection();
        StartCoroutine(Cast());
    }

    protected override IEnumerator Cast()
    {
        switch (_movementType) {
            case MovementType.Transform:
                Entity.transform.Translate(_lastDirection * Entity.StatContainer.GetStat<float>(StatNames.MoveSpeed) * Time.fixedDeltaTime);
                break;
            case MovementType.Rigidbody:
                Entity.Rb.velocity = _lastDirection * Entity.StatContainer.GetStat<float>(StatNames.MoveSpeed);
                break;
            default:
                Debug.LogError("Unconsidered MovementType - " + _movementType.ToString());
                break;
        }
        yield break;
    }

    private void DefineDirection()
    {
        _lastDirection = _directionDecision.DecideDirection();
    }

    private enum MovementType
    {
        Rigidbody,
        Transform
    }

//#if UNITY_EDITOR
//    [Button("Setup Stats")]
//    protected virtual void SetupStats()
//    {
//        var statContainer = GetComponent<StatContainer>();
//        statContainer.AddStat(new Stat(StatNames.MoveSpeed, StatType.Float));
//    }
//#endif
}