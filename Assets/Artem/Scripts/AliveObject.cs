using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

public abstract class AliveObject : MonoBehaviour
{

    [SerializeField] private Weapon _weapon;
    [SerializeField] private float _maxhp;
    [SerializeField] private float _moveSpeed;

    private float _hp;
    
    private HashSet<Vector2Int> _roomsIn = new ();

    public void AddRoom(Vector2Int roomPos) => _roomsIn.Add(roomPos);

    public void RemoveRoom(Vector2Int roomPos) => _roomsIn.Remove(roomPos);

    public bool ContainsRoom(Vector2Int roomPos) => _roomsIn.Contains(roomPos);

    public Weapon Weapon => _weapon;
    public float MoveSpeed => _moveSpeed;

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
        if (damage<0)
        {
            throw new ArgumentOutOfRangeException(nameof(damage));
        }
        HP-=damage;
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
}
