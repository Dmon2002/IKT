using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _distance;
    [SerializeField] private float _reloadTime = 2;
    public WeaponOwner owner;
    public bool IsCritical = false;
    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    public float Distance => _distance;

    public float ReloadTime => _reloadTime;

    public abstract void Hit();
}

public enum WeaponOwner
{
    Player,
    Enemy
}
