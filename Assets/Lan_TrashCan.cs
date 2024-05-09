using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lan_TrashCan : MonoBehaviour
{ public Vector2 overlapBoxSize = new Vector2(1, 1); // Size of the overlap box
    public LayerMask foodLayer; // Layer mask to filter for food items only

    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, overlapBoxSize, 0, foodLayer);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Pickup")) // Ensure it is a food item
            {
                Destroy(collider.gameObject); // Destroy the food item
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the overlap box in the editor to visualize the area
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(overlapBoxSize.x, overlapBoxSize.y, 1));
    }
}
