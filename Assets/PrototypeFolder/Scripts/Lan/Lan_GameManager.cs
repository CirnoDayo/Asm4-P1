using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lan_GameManager : MonoBehaviour
{   
    [System.Serializable]
    public class FoodRequest
    {
        public string FoodName;
        public string CookingMethod;

        public FoodRequest(string foodName, string cookingMethod)
        {
            FoodName = foodName;
            CookingMethod = cookingMethod;
        }
    }
    public static Lan_GameManager Instance { get; private set; }
    
    private List<Lan_Customer> customers = new List<Lan_Customer>();
    private Dictionary<Lan_Customer, List<FoodRequest>> customerRequests = new Dictionary<Lan_Customer, List<FoodRequest>>();
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    public void RegisterCustomer(Lan_Customer customer, List<Lan_GameManager.FoodRequest> requests)
    {
        if (!customers.Contains(customer))
        {
            customers.Add(customer);
            customerRequests.Add(customer, requests);

            string requestDetails = "";
            foreach (var request in requests)
            {
                requestDetails += $"Food: {request.FoodName}, Method: {request.CookingMethod}\n";
            }
            Debug.Log($"Registered Customer: {customer.gameObject.name}, Requests: {requestDetails}");
        }
        else
        {
            Debug.Log("Customer already registered, skipping...");
        }
    }



    public bool ProcessOrder(string deliveredFood, string cookingMethod)
    {
        foreach (var customer in customers)
        {
            if (customerRequests.TryGetValue(customer, out List<FoodRequest> requests))
            {
                if (!customer.isBeingServed && requests.Exists(req => req.FoodName == deliveredFood && req.CookingMethod == cookingMethod))
                {
                    ServeCustomer(customer);
                    return true; // Return true as soon as one matching customer is found
                }
            }
        }
        return false; // Return false if no matching customer is found
    }

    public void UnregisterCustomer(Lan_Customer customer)
    {
        if (customers.Contains(customer))
        {
            customers.Remove(customer);
            customerRequests.Remove(customer);
        }
    }

    public void ServeCustomer(Lan_Customer customer)
    {
        customer.ServeCustomer();
        UnregisterCustomer(customer);
    }

    

}
