using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akino_Interactions : MonoBehaviour
{
    public Transform pointer;
    public Transform lookTarget;

    public static bool keyboardInput = false;
    public static Vector2 aimRotation;

    bool input;
    bool inputReceived;
    bool grabbing;
    Transform grabbedObject;
    Transform thingInRange;
    LayerMask layerMask;

    private void Start()
    {
        pointer = GameObject.Find("Pointer").transform;
        lookTarget = GameObject.Find("LookTarget").transform;
    }

    private void Update()
    {
        if (keyboardInput)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, aimRotation, 1.5f, layerMask);
            thingInRange = hit.transform;
        }
        else
            thingInRange = null;

        #region DA JUICE
        float angle = Mathf.Atan2(-aimRotation.x, aimRotation.y) * Mathf.Rad2Deg;
        lookTarget.parent.transform.rotation = Quaternion.Euler(0, 0, angle);
        pointer.position = Vector2.Lerp(pointer.position, lookTarget.position, 25f * Time.deltaTime);
        pointer.rotation = Quaternion.Lerp(pointer.rotation, lookTarget.rotation, 50f * Time.deltaTime);
        #endregion

        if (keyboardInput && thingInRange != null)
        {
            if (!inputReceived)
            {
                input = true;
                inputReceived = true;
            }
        }
        else
            inputReceived = false;

        #region Object Interacting
        if (input && thingInRange != null)
        {
            if (grabbing)
            {
                grabbing = false;
                grabbedObject.position = thingInRange.position;
                grabbedObject = null;
                input = false;
            }
            else
            {
                grabbedObject = thingInRange;
                grabbing = true;
                input = false;
            }
        }
        if (grabbing)
        {
            grabbedObject.transform.position = transform.position;
            layerMask = LayerMask.GetMask("Placeable");
        }
        else
            layerMask = LayerMask.GetMask("Interactable");
        #endregion
    }
}
