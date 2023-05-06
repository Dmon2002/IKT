using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private float _damage;
    private float _speed;

    [SerializeField] private float _lifeTime = 8f;
    public float Damage => _damage;
    public float Speed => _speed;




    public void Initialize(float damage, float speed)
    {
        _damage = damage;
        _speed = speed;
    }

    private void Update()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime<=0)
            Destroy(gameObject);
    }

    public void Fire(Vector3 direction)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * Speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() is Player)
        {
            Destroy(gameObject);
        }
    }

}
