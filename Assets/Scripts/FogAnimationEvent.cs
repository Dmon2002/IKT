using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FogAnimationEvent : MonoBehaviour
{
    [SerializeField] private Room room;

    private SpriteRenderer spriteRenderer;

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
    public void FogAnimationStart(Transform playerTr)
    {
        if (!CheckStart)
        {
            StartCoroutine(StartMove(playerTr));
            CheckStart = true;
        }
    }
    private IEnumerator StartMove(Transform playerTr)
    {
        if (!IsDie)
        {
            while (playerTr == null || spriteRenderer == null)
            {
                yield return new WaitForFixedUpdate();
            }
            IsDie = true;
            StartCoroutine(Move(playerTr));
        }
    }
        private IEnumerator Move(Transform playerTr)
    {
        while (spriteRenderer.color.a>0)
        {
            yield return new WaitForFixedUpdate();
            spriteRenderer.color = new Color(1,1,1, spriteRenderer.color.a -  Time.deltaTime*2);
            transform.localScale -=Vector3.one*Time.deltaTime;
            transform.position += (transform.position - playerTr.position).normalized*Time.deltaTime*2;
        }
       // print("Марат мне не верит");
    }
}
