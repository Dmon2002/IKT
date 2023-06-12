using UnityEngine;

public class AbilityDirectionDecisionPlayer : AbilityDirectionDecision
{
    public override Vector2 DecideDirection()
    {
        return (LevelManager.Instance.Player.transform.position - transform.position).normalized;
    }
}
