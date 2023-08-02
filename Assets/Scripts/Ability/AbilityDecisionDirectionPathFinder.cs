using Pathfinding;
using Sirenix.OdinInspector;
using UnityEngine;

public class AbilityDecisionDirectionPathFinder : AbilityDecisionDirection
{
    [Required]
    [SerializeField] private AIDestinationSetter _AIDestinationSetter;
    [Required]
    [SerializeField] private AIPath _aiPath;

    private void Awake()
    {
        _AIDestinationSetter.target = LevelManager.Instance.Player.transform;
    }

    public override Vector2 DecideDirection()
    {
        Vector2 entityPosition = Ability.Entity.transform.position;
        Vector3 nextPosition;
        _aiPath.MovementUpdate(Time.fixedDeltaTime, out nextPosition, out var nextRotation);
        return ((Vector2)nextPosition - entityPosition).normalized;
    }
}
