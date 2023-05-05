using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputController : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed of the player

    private Rigidbody2D rb; // Rigidbody component reference

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the GameObject
    }

    void FixedUpdate()
    {
        // Get the horizontal and vertical inputs (e.g. WASD, arrow keys)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a movement vector based on the input and movement speed
        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * moveSpeed;

        // Apply the movement to the Rigidbody component
        rb.velocity = movement;
    }
}
