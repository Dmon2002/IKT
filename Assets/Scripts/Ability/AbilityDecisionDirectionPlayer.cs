using UnityEngine;

public class AbilityDecisionDirectionPlayer : AbilityDecisionDirection
{
    public override Vector2 DecideDirection()
    {
        return (LevelManager.Instance.Player.transform.position - transform.position).normalized;
    }
}
