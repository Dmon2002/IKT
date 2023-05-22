using Pathfinding;
using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //[SerializeField] private float agreDelay;
    //private Transform _player;

    //public event Action<Enemy> EnemyDied;
    //private AIDestinationSetter _destinationSetter;

    //private void Awake()
    //{
    //    _destinationSetter = GetComponent<AIDestinationSetter>();
    //}

    //private void Start()
    //{
    //    _player = LevelManager.Instance.Player.transform;
    //    StartCoroutine(AgreDelayCoroutine());
    //}

    //protected override void OnEnable()
    //{
    //    base.OnEnable();
    //    Die += OnDied;

    //    if (Weapon != null)
    //    {
    //        Weapon.owner = WeaponOwner.Enemy;
    //    }
    //    GameObject.FindObjectOfType<EnemyManager>().ActiveEnemies.Add(this);
    //}

    //private void OnDisable()
    //{
    //    Die -= OnDied;
    //    EnemyDied?.Invoke(this);
    //}

    //private IEnumerator AgreDelayCoroutine()
    //{
    //    yield return new WaitForSeconds(agreDelay);
    //    Agre();
    //}

    //public virtual void Agre()
    //{
    //    _destinationSetter.target = _player;
    //}

    //private void OnDied()
    //{
    //    gameObject.SetActive(false);
    //}

    //protected virtual void OnTriggerEnter2D(Collider2D collision)
    //{
    //    var potentialWeapon = collision.GetComponent<Weapon>();
    //    if (potentialWeapon is Weapon && potentialWeapon.owner == WeaponOwner.Player)
    //    {
    //        if (potentialWeapon.IsCritical)
    //        {
    //            ApplyDamage(potentialWeapon.Damage*2);
    //        }
    //        else
    //        {
    //            ApplyDamage(potentialWeapon.Damage);
    //        }
           
    //    }
    //}
}
