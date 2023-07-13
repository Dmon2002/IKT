using StatSystem;
using UnityEngine;

public class AbilityDecisionCooldown : AbilityDecision
{
    private CooldownReload _cooldown;

    private void Awake()
    {
        Ability.Casted += ResetCooldown;
        _cooldown = new CooldownReload(Ability.StatContainer.GetStat<float>(StatNames.AbilityCooldown),this);
    }

    public override bool Decide()
    {
        return !_cooldown.OnCooldown;
    }

    private void ResetCooldown()
    {
        if (_cooldown.OnCooldown)
            throw new System.Exception("Still on cooldown");
        _cooldown.StartReloading();
    }

}
