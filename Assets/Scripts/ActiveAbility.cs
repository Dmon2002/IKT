using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbility : MonoBehaviour
{
    [SerializeField] private bool _spawnsAttack;
    [SerializeField] private Attack _attackPrefab;
    [SerializeField] private StatContainer _statContainer;
    [SerializeField] private bool _isAutomatic;
    [SerializeField] private bool _isActiveOnStart;

    private Entity _entity;
    private List<AbilityDecision> _decisions;
    private bool _isActive;
    private CooldownReload _abilityCooldownReload;

    private IEnumerator _activatingCoroutine;

    public StatContainer StatContainer => _statContainer;

    public virtual bool CanActivate
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

    private void Awake()
    {
        _abilityCooldownReload = new CooldownReload(_statContainer.GetStatFloatValue(Stat.AbilityCooldownName), this);
        _decisions = new List<AbilityDecision>(GetComponentsInChildren<AbilityDecision>());
        _decisions.ForEach(decision =>
        {
            decision.SetAbility(this);
        });
    }

    private void Start()
    {
        if (_isActiveOnStart && _isAutomatic)
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
        var attack = Instantiate(_attackPrefab, transform.position, Quaternion.identity);
        _abilityCooldownReload.StartReloading();
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
            Activate();
            yield return null;
        }
    }
}
