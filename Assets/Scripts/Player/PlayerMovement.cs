using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private AnimationController animationController;
    private Rigidbody2D rb;
    private Vector2 movement;

    [SerializeField]
    Joystick joystick;
    [SerializeField]
    float moveSpeed = 1.9f;
    void Start()
    {
        animationController = GetComponent<AnimationController>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement = new Vector2(joystick.Horizontal, joystick.Vertical);
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        } 
        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;
        animationController.Move(moveSpeed);
    }

    private void FixedUpdate()
    {
        rb.velocity = movement.normalized * moveSpeed;
    }
}
