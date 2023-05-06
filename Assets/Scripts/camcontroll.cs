using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class camcontroll : MonoBehaviour {
	public Transform playerTransform;
	public float moveSpeed;
	public float MinY;
	public float MaxY;
	private float currentY;
	public float startX;
	void Start () {

		transform.position = new Vector3(startX, MinY, -10);
	}

	void Update () {

        if (playerTransform.position.y < MinY)
        {
			currentY = MinY;

		}
		else if(playerTransform.position.y > MaxY)
        {
			currentY = MaxY;
		}
        else
        {
			currentY = playerTransform.position.y;

		}


		if(currentY< playerTransform.position.y)
        {
			currentY = playerTransform.position.y;

		}

		Vector3 target = new Vector3()
		{
			x = startX,
			y = currentY,
			z = -10,

		};
			Vector3 pos= Vector3.Lerp(a: transform.position,b: target, t: moveSpeed * Time.deltaTime);
		transform.position = pos;
		
		

	}
}
