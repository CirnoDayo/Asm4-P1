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
            List<string> requests = GenerateRequest();
            Lan_GameManager.Instance.RegisterCustomer(this, requests);
        }
        else
        {
            Debug.Log("No available seats.");
        }

        PatienceBar = GetComponent<Lan_PatienceBar>();
    }
    
    List<string> GenerateRequest()
    {
        List<string> requests = new List<string>();
            int foodIndex = Random.Range(0, foodIcons.Count);
            GameObject foodRequest = Instantiate(foodIcons[foodIndex], requestPopup.transform);
            foodRequest.transform.localPosition = Vector3.zero;
            requests.Add(foodIcons[foodIndex].name);
            //Debug.Log("food request");
       // for (int i = 0; i < Random.Range(1, 4); i++)
        //{
        //}
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
