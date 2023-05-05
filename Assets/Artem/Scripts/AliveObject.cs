using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliveObject : MonoBehaviour
{
    [SerializeField] private float _maxhp;
    private float _hp;

    public void ApplyDamage(float damage)
    {
        if (damage<0)
        {
            throw new ArgumentOutOfRangeException(nameof(damage));
        }
        _hp-=damage;
    }
}
