using UnityEngine;

[RequireComponent (typeof(Animator))]
public class Explosion : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
