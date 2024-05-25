using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.UI; 

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
    
    public Animator customerAnimator;

    // Animation states
    private const string ANIM_IDLE = "Idle";
    private const string ANIM_FRONT_WALK = "Front";
    private const string ANIM_BACK_WALK = "Back";
    private const string ANIM_LEFT_WALK = "Left";
    private const string ANIM_RIGHT_WALK = "Right";

    public bool isWaitingForSeat = false;
    private bool hasLeftUnserved = false;
    
    public List<Image> noSeatAvailableImages;
   
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        PatienceBar = GetComponent<Lan_PatienceBar>();
        customerAnimator = GetComponent<Animator>();
        foreach (var image in noSeatAvailableImages)
        {
            image.gameObject.SetActive(false); // Ensure all images are hidden on start
        }
        CheckForSeat();
    }

    
    void CheckForSeat()
    {
        Transform seat = Lan_SeatManager.Instance.GetAvailableSeat();
        if (seat != null)
        {
            AssignSeat(seat);
        }
        else
        {
            Debug.Log("No available seats.");
            isWaitingForSeat = true;
            PatienceBar.SetSeatWaitingMode(true);
            foreach (var image in noSeatAvailableImages)
            {
                image.gameObject.SetActive(true); // Show all no seat images
            }
            StartCoroutine(CheckSeatAvailability());
        }
    }
    IEnumerator CheckSeatAvailability()
    {
        float checkInterval = 1f; // Check for seat availability every 1 second
        float waitTime = 5f; // Total wait time

        while (isWaitingForSeat && waitTime > 0)
        {
            yield return new WaitForSeconds(checkInterval);
            waitTime -= checkInterval;
            Transform newSeat = Lan_SeatManager.Instance.GetAvailableSeat();
            if (newSeat != null)
            {
                AssignSeat(newSeat);
                break;
            }
        }

        if (isWaitingForSeat) // If still waiting after 5 seconds
        {
            GoAway(); // Leave if no seat is found within the allotted time
        }
    }

    void AssignSeat(Transform seat)
    {
        assignedSeat = seat;
        agent.SetDestination(seat.position);
        List<Lan_GameManager.FoodRequest> requests = GenerateRequest();
        RegisterCustomer(requests);
        isWaitingForSeat = false;
        foreach (var image in noSeatAvailableImages)
        {
            image.gameObject.SetActive(false); // Hide all no seat images
        }
        PatienceBar.SetSeatWaitingMode(false);
        PatienceBar.ResetPatience(); // Reset patience to 30 seconds
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
        if (!isBeingServed && !hasLeftUnserved)
        {
            HealthManager.Instance.DecreaseHealth();
            hasLeftUnserved = true; // Set the flag to true after decreasing health
        }
        Destroy(gameObject, 1f);
    }
    void Update()
    {
       
        #region Animation
        Vector3 velocity = agent.velocity;

        if (velocity != Vector3.zero)
        {
            if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
            {
                // Moving horizontally
                if (velocity.x > 0)
                {
                    customerAnimator.Play(ANIM_RIGHT_WALK);
                }
                else
                {
                    customerAnimator.Play(ANIM_LEFT_WALK);
                }
            }
            else
            {
                // Moving vertically
                if (velocity.y > 0)
                {
                    customerAnimator.Play(ANIM_BACK_WALK);
                }
                else
                {
                    customerAnimator.Play(ANIM_FRONT_WALK);
                }
            }
        }
        else
        {
            customerAnimator.Play(ANIM_IDLE);
        }
        #endregion
    }
}
