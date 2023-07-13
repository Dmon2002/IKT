using UnityEngine;

public abstract class AbilityDecisionDirection : AbilityDecision
{
    public override bool Decide()
    {
        return DecideDirection() != Vector2.zero;
    }

    public abstract Vector2 DecideDirection();
}
