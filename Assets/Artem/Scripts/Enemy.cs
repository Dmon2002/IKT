using System;

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

    private void OnDisable()
    {
        died -= OnDied;
        enemyDied?.Invoke(this);
    }

    private void OnDied()
    {
        gameObject.SetActive(false);
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
