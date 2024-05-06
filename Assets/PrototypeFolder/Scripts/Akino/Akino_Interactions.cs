using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akino_Interactions : MonoBehaviour
{
    public static bool keyboardInput = false;

    bool input;
    bool lastToggledInput;
    bool grabbing;
    LayerMask layerMask;

    private void Update()
    {
        Debug.Log(keyboardInput);
        CheckInput();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f, layerMask);
        Transform thingInRange = hit.transform;

        if (!grabbing)
        {
            layerMask = LayerMask.GetMask("Interactable");
            if (thingInRange != null && input)
            {
                grabbing = true;
            }
        }
        else
        {
            layerMask = LayerMask.GetMask("Placeable");
            if (thingInRange != null && input)
            {
                grabbing = false;
            }
        }
    }

    private void CheckInput()
    {
        if (keyboardInput)
        {
            input = !input;
            lastToggledInput = input;
        }
    }
}
