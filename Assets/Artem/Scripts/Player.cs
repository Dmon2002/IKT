using System;
using System.Collections;
using UnityEngine;

public class Player : AliveObject
{
    private Enemy _targetEnemy;

    private float _reloadTimeRemaining = 0;

    private bool _isMoving;

    private bool _isImmortal = false;

    [SerializeField] private float _immortalTime=2f;

    private float _defaultWeaponDamage;


    public bool IsMoving
    {
        get { return _isMoving; }
        set { _isMoving = value; }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        if (Weapon != null)
        {
            Weapon.owner = WeaponOwner.Player;
            _defaultWeaponDamage= Weapon.Damage;
        }

    }

    private void Update()
    {
        if (_reloadTimeRemaining > 0)
            _reloadTimeRemaining -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        _targetEnemy = GetNearestEnemy();
        TryAttackEnemy();
    }


    private Enemy GetNearestEnemy()
    {
        Enemy nearestEnemy = null;
        float currentTargetDistance = Mathf.Infinity;
        if (_targetEnemy != null)
        {
            currentTargetDistance = Vector3.Distance(transform.position, _targetEnemy.transform.position);
        }

        foreach (var enemy in EnemyManager.Instance.ActiveEnemies)
        {
            float potentialDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (potentialDistance < Weapon.Distance && potentialDistance < currentTargetDistance)
            {
                currentTargetDistance = potentialDistance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    private void TryAttackEnemy()
    {
        if (_targetEnemy == null) return;
        if (Weapon == null) return;

        RotateWeapon();

        if (IsMoving) return;

        if (_reloadTimeRemaining > 0) return;
        
        AttackEnemy();
    }

    private void AttackEnemy()
    {
        Weapon.Hit();
        _reloadTimeRemaining = Weapon.ReloadTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy potentialEnemy = collision.gameObject.GetComponent<Enemy>();
        if (potentialEnemy is Enemy && _isImmortal==false)
        {
            StartCoroutine(MakeImmortal());
            ApplyDamage(potentialEnemy.Weapon.Damage);
        }
    }

    private void RotateWeapon()
    {
        Vector2 direction = _targetEnemy.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        Bullet potentialBullet = collision.GetComponent<Bullet>();
        if (potentialBullet is Bullet && _isImmortal==false)
        {
            ApplyDamage(potentialBullet.Damage);
            StartCoroutine(MakeImmortal());
        }
    }

    private IEnumerator MakeImmortal()
    {
        _isImmortal = true;
        yield return new WaitForSeconds(_immortalTime);
        _isImmortal = false;
    }

    public void BuffWeapon(float multiplication, float duration)
    {
        StartCoroutine(ChangeDamage(multiplication, duration));
    }

    IEnumerator ChangeDamage(float multiplication, float duration)
    {
       Weapon.Damage=Weapon.Damage*multiplication;
        yield return new WaitForSeconds(duration);
        Weapon.Damage = _defaultWeaponDamage;
    }
}
