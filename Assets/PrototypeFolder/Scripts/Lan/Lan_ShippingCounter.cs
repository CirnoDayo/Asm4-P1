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
        if (hit.collider != null && hit.collider.CompareTag("Pickup"))
        {
            Debug.Log(hit.transform.name + " have been delivered");
            ProcessFoodItem(hit.collider);
        }
    }

    void ProcessFoodItem(Collider2D item)
    {
        string foodName = ExtractBaseFoodName(item.gameObject.name);
        string cookingMethod = GetCookingMethod(item.gameObject); // Assuming you have a way to determine this
        Destroy(item.gameObject); // Remove the food item from the scene

        // Call the new ProcessOrder method with both food name and cooking method
        if (Lan_GameManager.Instance.ProcessOrder(foodName, cookingMethod))
        {
            Debug.Log(foodName + " with " + cookingMethod + " has been served!");
        }
        else
        {
            Debug.Log("No one ordered this dish: " + foodName + " with " + cookingMethod);
        }
    }
    string ExtractBaseFoodName(string fullName)
    {
        int cloneIndex = fullName.IndexOf("(Clone)");
        if (cloneIndex > -1)
        {
            return fullName.Substring(0, cloneIndex).Trim();
        }
        return fullName;
    }
    string GetCookingMethod(GameObject foodItem)
    {
        Lan_FoodItem foodComponent = foodItem.GetComponent<Lan_FoodItem>();
        if (foodComponent != null)
        {
            if (foodComponent.IsCooked)
                return "Cooked";  // Return "Cooked" if the food has been cooked
             // Return "Chopped" if the food has been chopped
        }
        return "None";  // Return "None" if no specific cooking method has been applied
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
