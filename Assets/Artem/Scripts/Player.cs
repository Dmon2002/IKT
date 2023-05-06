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


    
    private void Update()
    {

        if (_reloadTimeRemaining> 0)
        {
            _reloadTimeRemaining -= Time.deltaTime;
        }
        _targetEnemy = GetNearestEnemy();
        Debug.Log(_targetEnemy);
        AttackEnemy();

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

            if (potentialDistance < Weapon.Distance && potentialDistance<currentTargetDistance)
            {
                currentTargetDistance = potentialDistance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    private void AttackEnemy()
    {
        if (IsMoving) return;
        if (_targetEnemy == null) return;
        if (Weapon == null) return;
        if (_reloadTimeRemaining > 0) return;

        _targetEnemy.ApplyDamage(Weapon.Damage);
        _reloadTimeRemaining = Weapon.ReloadTime;

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() is Enemy)
        {
            Debug.Log("Collision with an instance of Enemy class");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Weapon.Distance);
    }
}
