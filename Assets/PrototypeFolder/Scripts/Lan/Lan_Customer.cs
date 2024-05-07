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
    //public Lan_ChairFinding[] targetList;
    private int targetIndex;
    [SerializeField] private List<Transform> chairPositions;
    private NavMeshAgent agent;
    

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
    
    private void GenerateRequest()
    {
        int foodIndex = Random.Range(0, foodIcons.Count);
        Transform placeToInstantiate = requestPopup.transform;
        GameObject foodRequest = Instantiate(foodIcons[foodIndex], placeToInstantiate);
        foodRequest.transform.localPosition = Vector3.zero;
    }
    
}
