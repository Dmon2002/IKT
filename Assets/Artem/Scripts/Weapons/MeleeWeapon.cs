using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MeleeWeapon : Weapon
{
    [SerializeField] private float colliderTime = 0.1f;
    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }

    public override void Hit()
    {
        _collider.enabled = true;
        transform.position = transform.position + new Vector3(0, 0.001f, 0);
        StartCoroutine(DisableCollider(colliderTime));
    }

    private IEnumerator DisableCollider(float colliderTime)
    {
        yield return new WaitForSeconds(colliderTime);
        _collider.enabled = false;
        transform.position = transform.position + new Vector3(0, -0.001f, 0);
    }
}
