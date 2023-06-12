using UnityEngine;

public abstract class AbilityDecision : MonoBehaviour
{
    private ActiveAbility _ability;

    protected ActiveAbility Ability => _ability;

    public void SetAbility(ActiveAbility ability)
    {
        _ability = ability;
    }

    public abstract bool Decide();
}
