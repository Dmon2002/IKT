using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : AliveObject
{
    // Start is called before the first frame update
    public event Action<Enemy> enemyDied;

    protected override void OnEnable()
    {
        base.OnEnable();
        died += OnDied;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDied()
    {
        enemyDied?.Invoke(this);
        Destroy(gameObject);
    }
    

}
