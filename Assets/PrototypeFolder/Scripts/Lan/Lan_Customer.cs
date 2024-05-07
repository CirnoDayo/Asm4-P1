using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Lan_Customer : MonoBehaviour
{
    public List<GameObject> foodIcons; 
    public GameObject requestPopup;
    private int targetIndex;
    public List<string> requestedFoods = new List<string>();
    //[SerializeField] private List<Transform> chairPositions;
    private NavMeshAgent agent;
    public bool isBeingServed = false;
    

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        Transform seat = Lan_SeatManager.Instance.GetAvailableSeat();
        if (seat != null)
        {
            agent.SetDestination(seat.position);
            GenerateRequest();
        }
        else
        {
            Debug.Log("No available seats.");
        }
    }
    
    void GenerateRequest()
    {
        int foodIndex = Random.Range(0, foodIcons.Count);
        GameObject foodRequest = Instantiate(foodIcons[foodIndex], requestPopup.transform);
        foodRequest.transform.localPosition = Vector3.zero;
        requestedFoods.Add(foodIcons[foodIndex].name); 
        /*for (int i = 0; i < Random.Range(1, 4); i++)
        {
            
        }*/
    }
    public void ServeCustomer()
    {
        isBeingServed = true;
        StartCoroutine(EatingRoutine());
    }

    IEnumerator EatingRoutine()
    {
        yield return new WaitForSeconds(5);
        Lan_SeatManager.Instance.MakeSeatAvailable(transform);
        Destroy(gameObject);
    }
}
