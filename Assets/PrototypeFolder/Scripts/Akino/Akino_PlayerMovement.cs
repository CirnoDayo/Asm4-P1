using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Akino_PlayerMovement : MonoBehaviour
{
    [Range(0, 20)] public float moveSpeed = 10f;
    public Animator animator;

    public static Vector2 movement;
    float highestVelocity;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
        if (rb.velocity.magnitude > (highestVelocity * 0.8f))
        {
            Akino_Interactions.aimRotation = rb.velocity;
            highestVelocity = rb.velocity.magnitude;

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        if (movement == Vector2.zero)
        {
            highestVelocity = 0;
        }
    }
}
