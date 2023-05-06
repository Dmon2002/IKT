using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputController : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
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
        Move();
    }

    public void Move()
    {
        if (!_player.CanMove)
            return;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (joystick.Horizontal != 0 && moveHorizontal == 0)
        {
            moveHorizontal = joystick.Horizontal;
        }
        if (joystick.Vertical != 0 && moveVertical == 0)
        {
            moveVertical = joystick.Vertical;
        }
        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * _player.MoveSpeed;

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
