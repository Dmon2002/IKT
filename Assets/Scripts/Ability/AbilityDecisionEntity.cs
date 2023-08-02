
public abstract  class AbilityDecisionEntity : AbilityDecision
{
    public override bool Decide()
    {
        return DecideEntity() != null;
    }

    public abstract Entity DecideEntity();
}
