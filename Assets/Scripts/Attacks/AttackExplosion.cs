using System.Collections;
using UnityEngine;

public class AttackExplosion : Attack
{
    private Collider2D _col;

    protected override void Awake()
    {
        base.Awake();
        _col = GetComponent<Collider2D>();
    }

    protected override void Shoot()
    {
        
        var resultColliders = new Collider2D[10];
        var filter = new ContactFilter2D();
        int count = _col.OverlapCollider(new ContactFilter2D().NoFilter(), resultColliders);
        if (_col.OverlapCollider(new ContactFilter2D().NoFilter(), resultColliders) != 0)
        {
            foreach (var collider in resultColliders)
            {
                if (collider == null)
                {
                    return;
                }
                if (collider.gameObject.TryGetComponent<Entity>(out var entity))
                {
                    CollideEntity(entity);
                }
            }
        }
    }
}
