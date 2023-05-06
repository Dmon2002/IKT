using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float _damage;
    [SerializeField] private string _name;
    [SerializeField] private float _distance;

    public float Damage => _damage;
    public string Name => _name;

    public float Distance => _distance;
}
