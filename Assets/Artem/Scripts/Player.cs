using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : AliveObject
{
    private Enemy _targetEnemy;

    private bool _isMoving;
    public bool IsMoveing
    {
        get { return _isMoving; }
        set { _isMoving = value; }
    }

    private void Update()
    {
        _targetEnemy = GetNearestEnemy();
        if(_targetEnemy!=null)
        {
            AttackEnemy(_targetEnemy);
        }
    }


    private Enemy GetNearestEnemy()
    {
        Enemy nearestEnemy = null;
        float distance = Mathf.Infinity;
        foreach (var enemy in EnemyManager.Instance.ActiveEnemies)
        {
            float currentDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (currentDistance < Weapon.Distance)
            {
                distance = currentDistance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    private void AttackEnemy(Enemy enemy)
    {
        enemy.ApplyDamage(Weapon.Damage);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() is Enemy)
        {
            Debug.Log("Collision with an instance of Enemy class");
        }
    }

}
