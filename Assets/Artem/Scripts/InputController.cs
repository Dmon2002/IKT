using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputController : MonoBehaviour
{
    [SerializeField] private float moveThreshold = 0.1f;

    private Rigidbody2D rb; // Rigidbody component reference
    private Player _player;

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<Player>();
    }

    void FixedUpdate()
    {
        // Get the horizontal and vertical inputs (e.g. WASD, arrow keys)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a movement vector based on the input and movement speed
        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * _player.MoveSpeed;

        // Apply the movement to the Rigidbody component
        rb.velocity = movement;

        if (Mathf.Abs(moveHorizontal) > moveThreshold || Mathf.Abs(moveVertical) > moveThreshold)
        {
            _player.IsMoving = true;
        }
        else
        {
            _player.IsMoving = false;
        }
    }
}
