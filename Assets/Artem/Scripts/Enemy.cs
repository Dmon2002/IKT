using Pathfinding;
using System;
using System.Collections;
using UnityEngine;

public class Enemy : AliveObject
{
    [SerializeField] private float agreDelay;
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
        StartCoroutine(AgreDelayCoroutine());
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        died += OnDied;

        if (Weapon != null)
        {
            Weapon.owner = WeaponOwner.Enemy;
        }
        GameObject.FindObjectOfType<EnemyManager>().ActiveEnemies.Add(this);
    }

    private void OnDisable()
    {
        died -= OnDied;
        
        EnemyDied?.Invoke(this);
    }

    private IEnumerator AgreDelayCoroutine()
    {
        yield return new WaitForSeconds(agreDelay);
        Agre();
    }

    public virtual void Agre()
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
