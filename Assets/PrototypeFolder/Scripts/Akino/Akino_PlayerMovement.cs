using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Akino_PlayerMovement : MonoBehaviour
{
    public InputAction playerInteraction;
    [Range(0, 20)] public float moveSpeed;
    
    Controls controls;
    Vector2 movement;
    Rigidbody2D rb;

    private void Awake()
    {
        controls = new Controls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.PlayerMovement.Move.performed += OnMove;
        controls.PlayerMovement.Move.canceled += OnUnmove;
    }

    private void OnDisable()
    {
        controls.Disable();
        controls.PlayerMovement.Move.performed -= OnMove;
        controls.PlayerMovement.Move.canceled -= OnUnmove;
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    private void OnUnmove(InputAction.CallbackContext context)
    {
        movement = Vector2.zero;
    }
}
