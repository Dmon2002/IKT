using UnityEngine;

public class AbilityDecisionEntityCollidedPlayer : AbilityDecisionEntity
{
    private Player _collidedPlayer;

    private bool _makeNull = false;

    public override Entity DecideEntity()
    {
        _makeNull = true;
        var toReturn = _collidedPlayer;
        return toReturn;
    }

    private void Awake()
    {
        Ability.Entity.Collided += OnCollide;
    }

    private void LateUpdate()
    {
        if (_makeNull)
        {
            _collidedPlayer = null;
            _makeNull = false;
        }
    }

    private void OnCollide(Collision2D collision2D)
    {
        if (collision2D.gameObject.TryGetComponent<Player>(out var player))
        {
            _collidedPlayer = player;
        }
    }
}
