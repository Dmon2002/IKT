using UnityEngine;

public abstract class AbilityDirectionDecision : AbilityDecision
{
    public override bool Decide()
    {
        return DecideDirection() != Vector2.zero;
    }

    public abstract Vector2 DecideDirection();
}
