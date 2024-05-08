using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akino_Interactions : MonoBehaviour
{
    public Transform pointer;
    public Transform lookTarget;

    public static bool keyboardInput = false;
    public static Vector2 aimRotation;
    public static bool grabbing = false;

    bool input;
    bool inputReceived;
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
        #region Input
        if (keyboardInput)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, aimRotation, 1.5f, layerMask);
            thingInRange = hit.transform;
        }
        else
            thingInRange = null;


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
        #endregion

        #region Interactions (this thing is hell, do NOT touch)
        if (input)
        {
            if (!grabbing)
            {
                if (thingInRange.CompareTag("Pickup"))
                {
                    grabbedObject = thingInRange;
                    grabbedObject.GetComponent<Collider2D>().enabled = false;
                    grabbing = true;
                }

                if (thingInRange.CompareTag("Storage"))
                {
                    Akino_EventManager.instance.FridgeInteract();
                }
            }
            else
            {
                if (thingInRange.CompareTag("Place"))
                {
                    grabbing = false;
                    grabbedObject.position = thingInRange.position;
                    grabbedObject.GetComponent<Collider2D>().enabled = true;
                    grabbedObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    grabbedObject = null;
                }
            }
            input = false;
        }

        if (grabbing)
        {
            layerMask = LayerMask.GetMask("Placeable");
        }
        else
            layerMask = LayerMask.GetMask("Interactable");
        #endregion

        #region Orange Juice
        float angle = Mathf.Atan2(-aimRotation.x, aimRotation.y) * Mathf.Rad2Deg;
        lookTarget.parent.transform.rotation = Quaternion.Euler(0, 0, angle);
        pointer.position = Vector2.Lerp(pointer.position, lookTarget.position, 25f * Time.deltaTime);
        pointer.rotation = Quaternion.Lerp(pointer.rotation, lookTarget.rotation, 50f * Time.deltaTime);

        if (grabbedObject != null)
        {
            grabbedObject.GetComponent<SpriteRenderer>().sortingOrder = 6;
            grabbedObject.position = pointer.position;
        }
        #endregion
    }
}
