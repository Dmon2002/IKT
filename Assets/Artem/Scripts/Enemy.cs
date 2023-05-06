using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : AliveObject
{
    // Start is called before the first frame update
    public event Action<Enemy> enemyDied;

    protected override void OnEnable()
    {
        base.OnEnable();
        died += OnDied;

        if (Weapon != null)
        {
            Weapon.owner = WeaponOwner.Enemy;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDied()
    {
        enemyDied?.Invoke(this);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var potentialWeapon = collision.GetComponent<Weapon>();
        Debug.Log("trigger");
        if (potentialWeapon is Weapon && potentialWeapon.owner == WeaponOwner.Player)
        {
            ApplyDamage(potentialWeapon.Damage);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("triggerexit");
    }


}
