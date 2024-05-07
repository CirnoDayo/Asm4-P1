using System.Collections.Generic;
using UnityEngine;

public class Lan_SeatManager : MonoBehaviour
{
    public static Lan_SeatManager Instance;
    private List<Transform> availableSeats;
    private Dictionary<Transform, bool> seatOccupancy; // Tracks whether a seat is occupied

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
        foreach (Transform seat in availableSeats)
        {
            if (!seatOccupancy[seat]) 
            {
                seatOccupancy[seat] = true; 
                return seat;
            }
        }
        return null; 
    }

    public void MakeSeatAvailable(Transform seat)
    {
        if (seatOccupancy.ContainsKey(seat))
        {
            seatOccupancy[seat] = false; 
            if (!availableSeats.Contains(seat))
                availableSeats.Add(seat); 
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