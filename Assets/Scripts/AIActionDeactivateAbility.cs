using UnityEngine;

public class AIActionDeactivateAbility : AIAction
{
    [SerializeField] private ActiveAbility _ability;

    public override void Action()
    {
        _ability.StopActivating();
    }
}
