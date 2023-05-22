using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class AbilityStats
{
    [SerializeField] private float _abilityCooldown;
    [SerializeField] private float _detectRadius;

    private float _abilityRemainingCooldown;
    private bool _abilityOnCooldown = false;

    public bool AbilityOnCooldown => _abilityOnCooldown;
    public float AbilityCooldown => _abilityCooldown;

    public float DetectRadius => _detectRadius;

    public IEnumerator RefreshCooldown()
    {
        _abilityRemainingCooldown = _abilityCooldown;
        _abilityOnCooldown = true;
        while (_abilityRemainingCooldown > 0)
        {
            yield return null;
            _abilityRemainingCooldown -= Time.deltaTime;
        }
        _abilityOnCooldown = false;
    }
}
