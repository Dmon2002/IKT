using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AliveObject : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private float _maxhp;
    [SerializeField] private float _moveSpeed;

    private float _hp;
    

    public Weapon Weapon => _weapon;
    public float MoveSpeed => _moveSpeed;


    public void ApplyDamage(float damage)
    {
        if (damage<0)
        {
            throw new ArgumentOutOfRangeException(nameof(damage));
        }
        _hp-=damage;
    }

    public void Heal(float healPower)
    {
        if (healPower < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(healPower));
        }
        if (_hp+healPower>_maxhp)
        {
            _hp = _maxhp;
        }
        else
        {
            _hp += healPower;
        }
        
    }


}
