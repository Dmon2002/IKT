using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public abstract class AliveObject : MonoBehaviour
{

    [SerializeField] private Weapon _weapon;
    [SerializeField] private float _maxhp;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private bool _isFlying;
    [SerializeField] private bool _canReveal;

    private float _hp;

    public bool CanReveal => _canReveal;

    
    private HashSet<Vector2Int> _roomsIn = new ();

    public bool IsFlying => _isFlying;

    public Weapon Weapon => _weapon;
    public float MoveSpeed => _moveSpeed;

    private SpriteRenderer spriteRenderer;

   /* private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }*/

    public float HP
    {
        get { return _hp; }
        set { _hp = value;
            if (_hp<=0)
            {
                died?.Invoke();
            }
        }
    }

    public Action died;

    protected virtual void OnEnable()
    {
        _hp = _maxhp;
        
    }

    public void ApplyDamage(float damage)
    {
        if (TryGetComponent<Player>(out var _))
        {
            Debug.Log("Apply");
        }
        if (damage<0)
        {
            throw new ArgumentOutOfRangeException(nameof(damage));
        }
        HP-=damage;
        StartCoroutine(RedAnim());
    }

    public void Heal(float healPower)
    {
        if (healPower < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(healPower));
        }
        if (_hp+healPower>_maxhp)
        {
            HP = _maxhp;
        }
        else
        {
            HP += healPower;
        }
        
    }

    public void Die()
    {
        HP = 0;
    }


    
    private IEnumerator RedAnim()
    {
        print(spriteRenderer);
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        if (spriteRenderer != null)
        {
            while (spriteRenderer.color.g > 0)
            {
                yield return new WaitForFixedUpdate();
                spriteRenderer.color = new Color(1, spriteRenderer.color.g-4f*Time.deltaTime, spriteRenderer.color.b - 4f * Time.deltaTime, 1);
            }
            while (spriteRenderer.color.g < 1)
            {
                yield return new WaitForFixedUpdate();
                spriteRenderer.color = new Color(1, spriteRenderer.color.g + 4f * Time.deltaTime, spriteRenderer.color.b + 4f * Time.deltaTime, 1);
            }
        }
    }
    
}
