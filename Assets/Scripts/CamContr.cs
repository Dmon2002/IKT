using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CamContr : MonoBehaviour {
	public Transform playerTransform;
	public float moveSpeed;
	public Vector2 StartPos;
	public float EndPos;

	void Start () {
		transform.position = StartPos;

	}

	void Update () {



		Vector3 target = new Vector3()
		{
			x = StartPos.x,
			y = playerTransform.position.y,
			z = -10,
		};

        if (target.y > EndPos)
        {
			target = new Vector3(StartPos.y, EndPos, -10);

		}
		else if (target.y < StartPos.y)
		{
			target = new Vector3(StartPos.x, StartPos.y, -10);

		}

		Vector3 pos= Vector3.Lerp(a: transform.position,b: target, t: moveSpeed * Time.deltaTime);
		transform.position = pos;
		
		

	}
}
