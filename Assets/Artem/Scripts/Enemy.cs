using System;

public abstract class Enemy : AliveObject
{
    // Start is called before the first frame update
    public event Action<Enemy> enemyDied;

    protected override void OnEnable()
    {
        base.OnEnable();
        died += OnDied;
    }

    private void OnDisable()
    {
        died -= OnDied;
        enemyDied?.Invoke(this);
    }

    private void OnDied()
    {
        gameObject.SetActive(false);
    }
    

}
