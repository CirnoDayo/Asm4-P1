using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lan_ShippingCounter : MonoBehaviour
{
    List<string> currentOrder = new List<string>(); // Temporarily holds the food items placed on the counter

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "FoodItem")
        {
            currentOrder.Add(other.gameObject.name);
            Destroy(other.gameObject); // Optionally destroy the food item game object
            CheckOrders();
        }
    }

    void CheckOrders()
    {
        foreach (Lan_Customer customer in FindObjectsOfType<Lan_Customer>())
        {
            if (!customer.isBeingServed && HasMatchingOrder(customer.requestedFoods, currentOrder))
            {
                customer.ServeCustomer();
                currentOrder.Clear(); 
                break; 
            }
        }
    }

    bool HasMatchingOrder(List<string> requestedFoods, List<string> deliveredFoods)
    {
      
        return new HashSet<string>(requestedFoods).SetEquals(deliveredFoods);
    }
}
