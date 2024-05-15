using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Akino_ControlsManager : MonoBehaviour
{
    public static Controls controls;
    public static bool inMenu;

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
        if (!inMenu)
            Akino_Player.movement = controls.Player.Move.ReadValue<Vector2>();
        else
            Akino_Player.movement = Vector2.zero;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
            Akino_Player.primaryInput = true;
        else if (Akino_Player.primaryInput)
            StartCoroutine(primaryDelay());
    }

    public void Return(InputAction.CallbackContext context)
    {
        if (context.started)
            Akino_Player.secondaryInput = true;
        else if (Akino_Player.secondaryInput)
            StartCoroutine(secondaryDelay());
    }

    IEnumerator primaryDelay()
    {
        yield return new WaitForEndOfFrame();
        Akino_Player.primaryInput = false;
    }

    IEnumerator secondaryDelay()
    {
        yield return new WaitForEndOfFrame();
        Akino_Player.secondaryInput = false;
    }
}
