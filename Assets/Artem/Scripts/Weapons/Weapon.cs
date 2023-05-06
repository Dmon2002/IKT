using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float _damage;
    [SerializeField] private string _name;
    [SerializeField] private float _distance;
    [SerializeField] private float _reloadTime = 2;



    public WeaponOwner owner;

    public float Damage => _damage;
    public string Name => _name;

    public float Distance => _distance;

    public float ReloadTime => _reloadTime;

    public abstract void Hit();
    



}

public enum WeaponOwner
{
    Player,
    Enemy
}
