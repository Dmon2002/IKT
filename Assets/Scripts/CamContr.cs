using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CamContr : MonoBehaviour {
	public Transform playerTransform;
	public float moveSpeed;
	public Vector2 StartPos;
	public float DownPos;
	void Start () {

		transform.position = (Vector3)StartPos + new Vector3(0f, 0f, -10f);
		
	}

	void Update () {
		Vector3 target = new Vector3()
		{
			x = StartPos.x,
			y = playerTransform.position.y- DownPos,
			z = -10,
		};

		if (target.y < StartPos.y)
		{
			target = new Vector3(StartPos.x, StartPos.y, -10);

		}

		Vector3 pos= Vector3.Lerp(a: transform.position,b: target, t: moveSpeed * Time.deltaTime);
		transform.position = pos;
		StartPos = new Vector2(StartPos.x,pos.y);

	}
}
