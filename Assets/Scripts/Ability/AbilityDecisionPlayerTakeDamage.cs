using UnityEngine;

public class AbilityDecisionPlayerTakeDamage : AbilityDecision
{
    [SerializeField] private Player _player;

    private bool _damaged;
    private bool _makeDefault;

    private void OnEnable()
    {
        _player.DamageTaken += OnDamageTaken;
    }

    private void OnDisable()
    {
        _player.DamageTaken -= OnDamageTaken;
    }

    private void Update()
    {
        if (_makeDefault)
        {
            _makeDefault = false;
            _damaged = false;
        }
    }

    private void OnDamageTaken(float damage)
    {
        _makeDefault = true;
        _damaged = true;
    }

    public override bool Decide()
    {
        if (_damaged)
        {
            var a = 1;
        }
        return _damaged;
    }
}
