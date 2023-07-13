using UnityEngine;

public abstract class AbilityDecision : MonoBehaviour
{
    private BaseActiveAbility _ability;

    protected BaseActiveAbility Ability => _ability;

    public void SetAbility(BaseActiveAbility ability)
    {
        _ability = ability;
    }

    public abstract bool Decide();
}
