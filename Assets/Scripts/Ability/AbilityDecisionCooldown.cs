using Sirenix.OdinInspector;
using StatSystem;
using UnityEngine;

public class AbilityDecisionCooldown : AbilityDecision
{
    private CooldownReload _cooldown;

    [ShowInInspector]
    public bool isEnabled => enabled;

    private void Awake()
    {
        Ability.Casted += ResetCooldown;
        _cooldown = new CooldownReload(Ability.StatContainer.GetStat<float>(StatNames.AbilityCooldown), this);
    }

    public override bool Decide()
    {
        //return false;
        return !_cooldown.OnCooldown;
    }

    private void ResetCooldown()
    {
        if (_cooldown.OnCooldown)
            throw new System.Exception("Still on cooldown");
        _cooldown.StartReloading();
    }

}
