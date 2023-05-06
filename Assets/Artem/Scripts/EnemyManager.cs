using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager: MonoBehaviour
{
    // Start is called before the first frame update
    private static EnemyManager instance;

    private void OnEnable()
    {
        foreach (var enemy in ActiveEnemies)
        {
            enemy.enemyDied += OnEnemyDied;
        }
    }
    public static EnemyManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<EnemyManager>();
            }
            return instance;
        }
    }

    public List<Enemy> ActiveEnemies;

    private void OnEnemyDied(Enemy enemy)
    {
        ActiveEnemies.Remove(enemy);
    }
}
