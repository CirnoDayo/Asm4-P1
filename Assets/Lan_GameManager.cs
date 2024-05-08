using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lan_GameManager : MonoBehaviour
{   
    public static Lan_GameManager Instance { get; private set; }
    
    private List<Lan_Customer> customers = new List<Lan_Customer>(); // List of all customers
    private Dictionary<Lan_Customer, List<string>> customerRequests = new Dictionary<Lan_Customer, List<string>>();
    private List<GameObject> dishHaveBeenShipped;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    public void RegisterCustomer(Lan_Customer customer, List<string> requests)
    {
        if (!customers.Contains(customer))
        {
            customers.Add(customer);
            customerRequests.Add(customer, requests);

            foreach (KeyValuePair<Lan_Customer, List<string>> customerRequest in customerRequests)
            {
                Debug.Log($"Key: {customerRequest.Key}, Value: {string.Join(", ", customerRequest.Value)}");
            }
        }
    }
    public bool ProcessOrder(string deliveredFood)
    {
        // Loop through the customers list to maintain the order they were added
        foreach (var customer in customers)
        {
            if (customerRequests.TryGetValue(customer, out List<string> requests))
            {
                if (!customer.isBeingServed && requests.Contains(deliveredFood))
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
            Debug.Log("Customer unregistered: " + customer.gameObject.name);
        }
    }


    public List<string> GetCustomerRequest(Lan_Customer customer)
    {
        if (customerRequests.TryGetValue(customer, out List<string> requests))
        {
            return requests;
        }
        return new List<string>(); 
    }

    public void ServeCustomer(Lan_Customer customer)
    {
        
        customer.ServeCustomer();
        UnregisterCustomer(customer);
    }

    

}
