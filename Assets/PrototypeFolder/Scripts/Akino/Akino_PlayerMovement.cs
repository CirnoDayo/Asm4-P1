using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Akino_PlayerMovement : MonoBehaviour
{
    [Range(0, 20)] float moveSpeed;

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
        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnUnmove;
        controls.Player.Interact.started += Interact;
        controls.Player.Interact.canceled += UnInteract;
    }

    private void OnDisable()
    {
        controls.Disable();
        controls.Player.Move.performed -= OnMove;
        controls.Player.Move.canceled -= OnUnmove;
        controls.Player.Interact.started -= Interact;
        controls.Player.Interact.canceled -= UnInteract;
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

    public void Interact(InputAction.CallbackContext context)
    {
        Akino_Interactions.keyboardInput = true;
    }

    public void UnInteract(InputAction.CallbackContext context)
    {
        Akino_Interactions.keyboardInput = false;
    }
}
