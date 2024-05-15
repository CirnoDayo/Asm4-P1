using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class Akino_Player : MonoBehaviour
{
    #region Variables

    [Range(0, 20)] public float moveSpeed = 10f;

    public static Vector2 movement;
    public static Vector2 aimRotation;
    public static bool primaryInput;
    public static bool secondaryInput;
    public static bool grabbing;

    Akino_UIManager UIManager;
    Rigidbody2D rb;
    Transform pointer;
    Transform lookTarget;
    Transform thingInRange;
    Transform grabbedObject;
    float highestVelocity;
    LayerMask layerMask;

    #endregion

    private void Start()
    {
        UIManager = GetComponent<Akino_UIManager>();
        rb = GetComponent<Rigidbody2D>();
        pointer = GameObject.Find("Pointer").transform;
        lookTarget = GameObject.Find("LookTarget").transform;
    }

    private void FixedUpdate()
    {
        #region Movement
        rb.velocity = movement * moveSpeed;
        if (rb.velocity.magnitude > (highestVelocity * 0.5f))
        {
            aimRotation = rb.velocity;
            highestVelocity = rb.velocity.magnitude;
        }

        if (movement == Vector2.zero)
        {
            highestVelocity = 0;
        }
        #endregion
    }

    private void Update()
    {
        #region Interactions

        #region Input
        if (primaryInput || secondaryInput)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, aimRotation, 1.25f, layerMask);
            thingInRange = hit.transform;
            if (grabbing)
            {
                /*RaycastHit2D hit = */
                Physics2D.Raycast(transform.position, aimRotation, 1.25f, 6);
            }
        }
        else
            thingInRange = null;
        #endregion

        #region Interaction Matrix (this thing is hell, do NOT touch)
        if (primaryInput && thingInRange != null && !Akino_ControlsManager.inMenu)
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
            primaryInput = false;
        }

        if (secondaryInput && Akino_ControlsManager.inMenu)
        {
            Akino_EventManager.instance.FridgeInteract();
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

        #endregion
    }
}