using System;
using UnityEngine;

public class Enemy : AliveObject
{
    public event Action<Enemy> EnemyDied;

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

    private void OnDied()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var potentialWeapon = collision.GetComponent<Weapon>();
        if (potentialWeapon is Weapon && potentialWeapon.owner == WeaponOwner.Player)
        {
            ApplyDamage(potentialWeapon.Damage);
        }
    }
}
