using StatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatContainer))]
public class ActiveAbility : MonoBehaviour
{

    [SerializeField] private bool _spawnsAttack;
    [SerializeField] private Attack _attackPrefab;
    [SerializeField] private bool _isDirectional;
    [SerializeField] private AbilityDirectionDecision _directionDecision;
    [SerializeField] private bool _activateOnStart;
    
    private StatContainer _statContainer;

    private Entity _entity;
    private List<AbilityDecision> _decisions;
    private bool _isActive;
    private CooldownReload _abilityCooldownReload;
    private Vector2 _direction;

    private IEnumerator _activatingCoroutine;

    public bool IsActive => _isActive;

    public StatContainer StatContainer => _statContainer;

    public bool CanActivate
    {
        get
        {
            if (_abilityCooldownReload.OnCooldown)
                return false;
            foreach (var decision in _decisions)
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
        _statContainer = GetComponent<StatContainer>();
        _decisions = new List<AbilityDecision>(GetComponentsInChildren<AbilityDecision>());
        foreach (var decision in _decisions)
        {
            decision.SetAbility(this);
        }
    }

    private void Start()
    {
        _abilityCooldownReload = new CooldownReload(_statContainer.GetStat<float>(StatNames.AbilityCooldown), this);
        if (_activateOnStart)
        {
            StartActivating();
        }

    }

    public void SetEntity(Entity entity)
    {
        _entity = entity;
    }

    public virtual void Activate()
    {
        if (!CanActivate)
            return;
        if (_spawnsAttack)
        {
            var attack = Instantiate(_attackPrefab, transform.position, Quaternion.identity);
            if (_isDirectional)
            {
                attack.SetDirection(_direction);
            }
        }
        _abilityCooldownReload.StartReloading();
    }

    public void SetDirection()
    {
        if (!_isDirectional)
            return;
        _direction = _directionDecision.DecideDirection();
    }

    public void StartActivating()
    {
        _isActive = true;
        _activatingCoroutine = ActivateCoroutine();
        StartCoroutine(_activatingCoroutine);
    }

    public void StopActivating()
    {
        _isActive = false;
    }

    protected virtual IEnumerator ActivateCoroutine()
    {
        while (_isActive)
        {
            if (_isDirectional)
            {
                SetDirection();
            }
            Activate();
            yield return null;
        }
    }
}
