using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Akino_ControlsManager : MonoBehaviour
{
    public static Controls controls;

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void Move(InputAction.CallbackContext context)
    {
        Akino_PlayerMovement.movement = context.ReadValue<Vector2>().normalized;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        Akino_Interactions.externalInput = !controls.Player.Interact.IsPressed();
    }

    public void Return(InputAction.CallbackContext context)
    {

    }

    public void Swap(InputAction.CallbackContext context)
    {

    }
}
