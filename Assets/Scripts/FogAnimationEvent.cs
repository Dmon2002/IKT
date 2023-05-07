using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FogAnimationEvent : MonoBehaviour
{
    [SerializeField] private Room room;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private float speed=2;

    public bool IsDie = false;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void OnAnimationEnd()
    {
        room.OnFogAnimationEnd();
    }


    private bool CheckStart = false;
    public void FogAnimationStart(Vector2 direction)
    {
        if (!CheckStart)
        {
            StartMove(direction);
            CheckStart = true;
        }
    }
    private void StartMove(Vector2 direction)
    {
        if (!IsDie)
        {
            IsDie = true;
            StartCoroutine(Move(direction));
        }
    }
    private IEnumerator Move(Vector2 direction)
    {
        while (spriteRenderer.color.a > 0)
        {
            yield return new WaitForFixedUpdate();
            spriteRenderer.color = new Color(1, 1, 1, spriteRenderer.color.a - 1 / speed * Time.deltaTime);
            transform.localScale -= Vector3.one*Time.deltaTime / (speed * 5);
            transform.position += (Vector3)direction.normalized * speed * Time.deltaTime;
        }
        room.OnFogAnimationEnd();
    }
}
