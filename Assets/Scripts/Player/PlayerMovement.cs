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
    float moveSpeed = 3f;
    [SerializeField]
    float speed = 1.5f;

    WeaponManager weaponManager;
    void Start()
    {
        animationController = GetComponent<AnimationController>();
        rb = GetComponent<Rigidbody2D>();
        weaponManager = GetComponent<WeaponManager>();
    }

    void Update()
    {
        movement = new Vector2(joystick.Horizontal, joystick.Vertical);
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            animationController.Move(speed);
        }
        else
        {
            animationController.Move(0.5f);
        }
    }

    private void FixedUpdate()
    {
        float currentSpeed = moveSpeed;
        if(weaponManager.weaponType == WeaponManager.WeaponType.Knife)
        {
            currentSpeed = 4f;
        }
        rb.velocity = movement.normalized * currentSpeed;
    }
}
