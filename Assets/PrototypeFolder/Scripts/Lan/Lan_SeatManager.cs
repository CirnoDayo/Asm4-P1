using System.Collections.Generic;
using UnityEngine;

public class Lan_SeatManager : MonoBehaviour
{
    public static Lan_SeatManager Instance;
    public List<Transform> availableSeats;
    public Dictionary<Transform, bool> seatOccupancy; // Tracks whether a seat is occupied

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        InitializeSeats();
    }

    void InitializeSeats()
    {
        availableSeats = new List<Transform>();
        seatOccupancy = new Dictionary<Transform, bool>();
        GameObject[] chairs = GameObject.FindGameObjectsWithTag("Lan_Chair");
        foreach (GameObject chair in chairs)
        {
            Transform seatTransform = chair.transform.Find("ChairPosition");
            if (seatTransform && IsPositionAvailable(seatTransform))
            {
                availableSeats.Add(seatTransform);
                seatOccupancy[seatTransform] = false; 
            }
        }
    }

    public Transform GetAvailableSeat()
    {
        foreach (var seat in seatOccupancy)
        {
            if (!seat.Value)
            {
                seatOccupancy[seat.Key] = true;
                return seat.Key;
            }
        }
        return null; 
    }

    public void MakeSeatAvailable(Transform seat)
    {
        if (seat != null && seatOccupancy.ContainsKey(seat) && seatOccupancy[seat])
        {
            seatOccupancy[seat] = false; // Mark the seat as available
            Debug.Log("Free the seat");
        }
    }

    private bool IsPositionAvailable(Transform position)
    {
        Vector2 boxSize = new Vector2(0.5f, 0.5f); 
        
        Collider2D[] hits = Physics2D.OverlapBoxAll(position.position, boxSize, 0);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Lan_Customer")) 
            {
                
                return false;
            }
        }
        return true;
    }
}