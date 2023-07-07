using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    private PlayerMovement _movement;

    protected override void Awake()
    {
        base.Awake();
        _movement = GetComponentInChildren<PlayerMovement>();
        _movement.SetStatContainer(StatContainer);
    }

    //protected override void OnEnable()
    //{
    //    base.OnEnable();
    //    Die += OnDied;
    //    if (Weapon != null)
    //    {
    //        Weapon.owner = WeaponOwner.Player;
    //        _defaultWeaponDamage= Weapon.Damage;
    //    }
    //}

    //private void OnDisable()
    //{
    //    Die -= OnDied;
    //}

    //private void FixedUpdate()
    //{
    //    WeaponTr.position = transform.position;
    //    _targetEnemy = GetNearestEnemy();
    //    TryAttackEnemy();
    //}


    //private Enemy GetNearestEnemy()
    //{
    //    Enemy nearestEnemy = null;
    //    float currentTargetDistance = Mathf.Infinity;
    //    if (_targetEnemy != null)
    //    {
    //        currentTargetDistance = Vector3.Distance(transform.position, _targetEnemy.transform.position);
    //    }

    //    foreach (var enemy in EnemyManager.Instance.ActiveEnemies)
    //    {
    //        if (!enemy.gameObject.activeSelf)
    //        {
    //            EnemyManager.Instance.ActiveEnemies.Remove(enemy);
    //        }
    //        else
    //        {
    //            float potentialDistance = Vector3.Distance(transform.position, enemy.transform.position);

    //            if (potentialDistance < Weapon.Distance && potentialDistance < currentTargetDistance)
    //            {
    //                currentTargetDistance = potentialDistance;
    //                nearestEnemy = enemy;
    //            }
    //        }
    //    }
    //    return nearestEnemy;
    //}

    //private void TryAttackEnemy()
    //{
    //   // print(_targetEnemy);
    //    if (_targetEnemy == null) return;
    //    if (Weapon == null) return;

    //    RotateWeapon();

    //    if (_reloadTimeRemaining > 0) return;

    //    AttackEnemy();
    //}

    //private void AttackEnemy()
    //{
    //    Weapon.Hit();
    //    _reloadTimeRemaining = Weapon.ReloadTime;
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Enemy potentialEnemy = collision.gameObject.GetComponent<Enemy>();
    //    if (potentialEnemy is Enemy && _isImmortal == false)
    //    {
    //        if (potentialEnemy.Weapon == null)
    //            return;
    //        StartCoroutine(MakeImmortal());
    //        ApplyDamage(potentialEnemy.Weapon.Damage);
    //    }
    //}

    //private void RotateWeapon()
    //{
    //    Vector2 direction = _targetEnemy.transform.position - transform.position;
    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //    Weapon.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //    Bullet potentialBullet = collision.GetComponent<Bullet>();
    //    if (potentialBullet is Bullet && _isImmortal == false)
    //    {
    //        ApplyDamage(potentialBullet.Damage);
    //        StartCoroutine(MakeImmortal());
    //    }
    //}

    //private IEnumerator MakeImmortal()
    //{
    //    _isImmortal = true;
    //    yield return new WaitForSeconds(_immortalTime);
    //    _isImmortal = false;
    //}

    //public void BuffWeapon(float multiplication, float duration)
    //{
    //    StartCoroutine(ChangeDamage(multiplication, duration));
    //}

    //IEnumerator ChangeDamage(float multiplication, float duration)
    //{
    //   Weapon.Damage=Weapon.Damage*multiplication;
    //    yield return new WaitForSeconds(duration);
    //    Weapon.Damage = _defaultWeaponDamage;
    //}



    //public void OnDied()
    //{
    //    AudioManager.instance.PlayPlayerDead();      
    //    Time.timeScale = 0;
    //    GameObject.FindObjectOfType<SavesController>().LoseLevel();
    //    deadScreen.SetActive(true);
    //}
}
