using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippingCounter : MonoBehaviour
{
    public Transform raycastOrigin; // The origin point of the raycast
    public float raycastLength = 2f; // Length of the raycast
    public LayerMask foodLayer; // Layer that only food items are on
    private List<string> currentOrder = new List<string>(); // Temporarily holds the food items placed on the counter

    void Update()
    {
        // Cast a ray forward from the raycast origin
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, Vector2.right, raycastLength, foodLayer);
        if (hit.collider != null && hit.collider.CompareTag("Lan_FoodItem"))
        {
            ProcessFoodItem(hit.collider);
            Debug.Log(hit.transform.name +" have been delivered");
        }
    }

    void ProcessFoodItem(Collider2D item)
    {
        // Process the food item that was hit by the ray
        currentOrder.Add(item.gameObject.name);
        Destroy(item.gameObject); // Remove the food item from the scene
        CheckOrders();
    }

    void CheckOrders()
    {
        foreach (Lan_Customer customer in FindObjectsOfType<Lan_Customer>())
        {
            if (!customer.isBeingServed && HasMatchingOrder(customer.requestedFoods, currentOrder))
            {
                customer.ServeCustomer();
                currentOrder.Clear(); // Clear the order list after serving
                break; // Exit the loop after serving one customer
            }
        }
    }

    bool HasMatchingOrder(List<string> requestedFoods, List<string> deliveredFoods)
    {
        // Check if all requested foods are in the delivered foods
        return new HashSet<string>(requestedFoods).SetEquals(deliveredFoods);
    }

    void OnDrawGizmos()
    {
        // Draw the ray in the editor to visualize it
        if (raycastOrigin != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(raycastOrigin.position, raycastOrigin.position + Vector3.right * raycastLength);
        }
    }
}
