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
        //gameManager.RegisterSlot(this);
    }

    private void Update()
    {
        CheckForPickupObjects();
    }

    private void CheckForPickupObjects()
    {
       
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, boxSize, 0);
            bool foundPickup = false;

            foreach (Collider2D collider in colliders) {
                if (collider.CompareTag("Pickup")) {
                    foundPickup = true;
                    break;
                }
            }

            gameManager.SetSlotAvailability(slotIndex, !foundPickup);
        
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
