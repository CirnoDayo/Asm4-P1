using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lan_SeatManager : MonoBehaviour
{   
    public static Lan_SeatManager Instance;
    private List<Transform> availableSeats;

    private void Awake()
    {
        Instance = this;
        RefreshAvailableSeats();
    }

    public Transform GetAvailableSeat()
    {
        if (availableSeats.Count > 0)
        {
            Transform selectedSeat = availableSeats[0];
            availableSeats.RemoveAt(0);  // Remove the seat from available list
            return selectedSeat;
        }
        return null; // No seats available
    }

    public void RefreshAvailableSeats()
    {
        availableSeats = new List<Transform>();
        GameObject[] chairs = GameObject.FindGameObjectsWithTag("Lan_Chair");
        Debug.Log($"Found {chairs.Length} chairs."); // Check how many chairs are found

        foreach (GameObject chair in chairs)
        {
            Transform seatTransform = chair.transform.Find("ChairPosition");
            if (seatTransform)
            {
                if (IsPositionAvailable(seatTransform))
                {
                    availableSeats.Add(seatTransform);
                    Debug.Log($"Seat added: {seatTransform.name}");
                }
                else
                {
                    Debug.Log($"Seat blocked: {seatTransform.name}");
                }
            }
            else
            {
                Debug.Log("ChairPosition not found");
            }
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
