using Pathfinding;
using Sirenix.OdinInspector;
using UnityEngine;

public class AbilityDecisionDirectionalNearestRoom : AbilityDecisionDirection
{
    [Required]
    [SerializeField] private AIDestinationSetter _AIDestinationSetter;
    [Required]
    [SerializeField] private AIPath _aiPath;

    private Room _currentRoom;

    public override Vector2 DecideDirection()
    {
        Vector2 entityPosition = Ability.Entity.transform.position;
        if (_currentRoom == null || _aiPath.reachedDestination)
        {
            _currentRoom = LevelManager.Instance.GetNearestRoom(entityPosition, room => !room.FogRevealed);
        }
        if (_currentRoom == null)
            return Vector2.zero;
        _AIDestinationSetter.target = _currentRoom.transform;
        Vector3 nextPosition;
        _aiPath.MovementUpdate(Time.fixedDeltaTime, out nextPosition, out var nextRotation);
        return ((Vector2)nextPosition - entityPosition).normalized;
    }

    //private static bool IsBetween(float value, float value1, float value2)
    //{
    //    return value <= Mathf.Max(value1, value2) && value >= Mathf.Min(value1, value2);
    //}

    //private static bool InRectanle(Vector2 point, Vector2 corner1, Vector2 corner2)
    //{
    //    return IsBetween(point.x, corner1.x, corner2.x) && IsBetween(point.y, corner1.y, corner2.y);
    //}
}
