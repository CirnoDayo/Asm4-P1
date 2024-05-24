using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lan_CounterSlot : MonoBehaviour
{
    private Lan_PlaceholderScript gameManager;
    public int slotIndex;
    public Vector3 boxSize = new Vector3(1, 1, 1); // Adjust the size as needed
    public LayerMask pickupLayer; // LayerMask to detect "Pickup" objects

    private void Start()
    {
        gameManager = FindObjectOfType<Lan_PlaceholderScript>();
        gameManager.RegisterSlot(this);
    }

    private void Update()
    {
        CheckForPickupObjects();
    }

    private void CheckForPickupObjects()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, boxSize, 0);
        if (colliders.Length > 0)
        {
            // Process the first food item found that hasn't been processed yet
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Pickup"))
                {

                    //bool isAvailable = false;
                    gameManager.SetSlotAvailability(slotIndex, false);
                    break;
                }
                else
                {
                    gameManager.SetSlotAvailability(slotIndex, true);
                }
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
