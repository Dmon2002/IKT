using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Bullet _bulletPrefab;

    private float _reloadTimeRemaining;

    public override void Hit()
    {
        Bullet bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
        bullet.Initialize(Damage, _bulletSpeed);
        Vector3 direction = Vector3.Normalize(GameManager.Instance.Player.transform.position - transform.position);
        bullet.Fire(direction);
    }

    private void Start()
    {
        _reloadTimeRemaining = ReloadTime;
    }

    private void Update()
    {
        if (_reloadTimeRemaining > 0)
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
