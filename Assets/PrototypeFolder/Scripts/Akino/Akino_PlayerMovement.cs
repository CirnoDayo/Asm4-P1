using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Akino_PlayerMovement : MonoBehaviour
{
    [Range(0, 20)] public float moveSpeed = 10f;
    public Animator animator;

    private Vector2 lastDirection;
    public static Vector2 movement;
    float highestVelocity;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

     private void FixedUpdate()
    {
        /* rb.velocity = movement * moveSpeed;
        if (rb.velocity.magnitude > (highestVelocity * 0.8f))
        {
            Akino_Interactions.aimRotation = rb.velocity;
            highestVelocity = rb.velocity.magnitude;

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        if (animator.GetFloat("Horizontal", movement.x))
            {

            }
                    
        }
        if (movement == Vector2.zero)
        {
            Debug.Log(movement);
            highestVelocity = 0;
            animator.SetFloat("Speed",0);
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        } */

        rb.velocity = movement * moveSpeed;

        if (movement != Vector2.zero)
        {
            Akino_Interactions.aimRotation = rb.velocity;
            lastDirection = movement; // Update lastDirection only when there is movement
            highestVelocity = rb.velocity.magnitude;

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
        else if (movement == Vector2.zero && rb.velocity.magnitude < 0.01f) // Check if player has stopped
        {
            highestVelocity = 0;
            animator.SetFloat("Speed", 0);
            animator.SetFloat("Horizontal", lastDirection.x); // Use lastDirection to set the idle animation
            animator.SetFloat("Vertical", lastDirection.y);
        }

    }
}
