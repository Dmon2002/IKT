using Pathfinding;
using System;
using UnityEngine;

public class Enemy : AliveObject
{
    [SerializeField] private LayerMask raycastLayers;
    private Transform _player;

    public event Action<Enemy> EnemyDied;
    private AIDestinationSetter _destinationSetter;

    private void Awake()
    {
        _destinationSetter = GetComponent<AIDestinationSetter>();
    }

    private void Start()
    {
        _player = GameManager.Instance.Player.transform;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        died += OnDied;

        if (Weapon != null)
        {
            Weapon.owner = WeaponOwner.Enemy;
        }
    }

    private void OnDisable()
    {
        died -= OnDied;
        EnemyDied?.Invoke(this);
    }

    private void Update()
    {
        TryAgre();
    }

    private void TryAgre()
    {
        if (Physics2D.Raycast(transform.position, transform.position - _player.position, float.PositiveInfinity, raycastLayers))
            return;
        Agre();
    }

    public void Agre()
    {
        _destinationSetter.target = _player;
    }

    private void OnDied()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        var potentialWeapon = collision.GetComponent<Weapon>();
        if (potentialWeapon is Weapon && potentialWeapon.owner == WeaponOwner.Player)
        {
            ApplyDamage(potentialWeapon.Damage);
        }
    }
}
