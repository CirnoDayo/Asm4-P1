using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Lan_Customer : MonoBehaviour
{
    public List<GameObject> foodIcons; 
    public List<GameObject> cookingMethodIcons; 
    public GameObject requestPopup;
    private int targetIndex;
    public List<string> requestedFoods = new List<string>();
    //[SerializeField] private List<Transform> chairPositions;
    private NavMeshAgent agent;
    public bool isBeingServed = false;
    public Lan_PatienceBar PatienceBar;
    public Transform assignedSeat;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Transform seat = Lan_SeatManager.Instance.GetAvailableSeat();
        if (seat != null)
        {
            assignedSeat = seat;
            agent.SetDestination(seat.position);
            List<Lan_GameManager.FoodRequest> requests = GenerateRequest();
            RegisterCustomer(requests);
        }
        else
        {
            Debug.Log("No available seats.");
        }

        PatienceBar = GetComponent<Lan_PatienceBar>();
    }

    private void RegisterCustomer(List<Lan_GameManager.FoodRequest> requests)
    {
        Lan_GameManager.Instance.RegisterCustomer(this, requests);
    }
    
    public List<Lan_GameManager.FoodRequest> GenerateRequest()
    {
        List<Lan_GameManager.FoodRequest> requests = new List<Lan_GameManager.FoodRequest>();
        int foodIndex = Random.Range(0, foodIcons.Count);
        int methodIndex = Random.Range(0, cookingMethodIcons.Count);

        GameObject foodRequest = Instantiate(foodIcons[foodIndex], requestPopup.transform);
        GameObject methodRequest = Instantiate(cookingMethodIcons[methodIndex], requestPopup.transform);
        foodRequest.transform.localPosition = new Vector3(-0.75f, 0, 0);
        methodRequest.transform.localPosition = new Vector3(0.75f, 0, 0);

        Lan_GameManager.FoodRequest newRequest = new Lan_GameManager.FoodRequest(foodIcons[foodIndex].name, cookingMethodIcons[methodIndex].name);
        requests.Add(newRequest);

        Debug.Log($"Generated Request - Food: {newRequest.FoodName}, Method: {newRequest.CookingMethod}");

        return requests;
    }
    public void ServeCustomer()
    {
        isBeingServed = true;
        requestPopup.SetActive(false);
        PatienceBar.isBeingServed = true;
        StartCoroutine(EatingRoutine());
    }

    IEnumerator EatingRoutine()
    {
        yield return new WaitForSeconds(5);
        Lan_SeatManager.Instance.MakeSeatAvailable(assignedSeat);
        GoAway();
        //Destroy(gameObject);
    }

    public void GoAway()
    {
        Destroy(gameObject,1f);
    }
}
