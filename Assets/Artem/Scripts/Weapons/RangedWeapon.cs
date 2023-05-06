using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Bullet _BulletPrefab;

    private float _reloadTimeRemaining;
    public override void Hit()
    {
        Bullet bullet = Instantiate(_BulletPrefab, transform);
        bullet.Initialize(Damage, _bulletSpeed);
        Vector3 direction = Vector3.Normalize(GameManager.Instance.Player.transform.position - transform.position);
        bullet.Fire(direction);
    }

    // Start is called before the first frame update
    void Start()
    {
        _reloadTimeRemaining = ReloadTime;
    }

    // Update is called once per frame
    void Update()
    {
        _reloadTimeRemaining-=Time.deltaTime;
        
    }

    private void FixedUpdate()
    {
        if (_reloadTimeRemaining <= 0)
        {
            Hit();
            _reloadTimeRemaining = ReloadTime;
        }
    }
}
