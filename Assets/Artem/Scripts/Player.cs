using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : AliveObject
{
    private Enemy _targetEnemy;

    private float _reloadTimeRemaining = 0;

    private bool _isMoving;
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

        if (IsMoving) return; // ��� ����� ��������

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
        if (collision.gameObject.GetComponent<Enemy>() is Enemy)
        {
            Debug.Log("Collision with an instance of Enemy class");
        }
    }

    private void RotateWeapon()
    {
        Vector2 direction = _targetEnemy.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Weapon.Distance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet potentialBullet = collision.GetComponent<Bullet>();
        if (potentialBullet is Bullet)
        {
            ApplyDamage(potentialBullet.Damage);
            Debug.Log("auch");
        }
    }
}
