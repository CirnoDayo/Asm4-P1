using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lan_PatienceBar : MonoBehaviour
{
    public Image patienceBar;
    public float maxPatience = 30f; // Max time the customer will wait
    public float patienceLeft;
    public bool isBeingServed = false;
    public bool seatWaitingMode = false;
    public Lan_Customer LanCustomer;

    private void Start()
    {
        patienceLeft = maxPatience;
        patienceBar.fillAmount = 1;
        LanCustomer = FindObjectOfType<Lan_Customer>();
    }

    private void Update()
    {
        if (!isBeingServed && !seatWaitingMode)
        {
            UpdatePatience();
        }
        else if (seatWaitingMode)
        {
            UpdateSeatWaiting();
        }
    }
    private void UpdatePatience()
    {
        patienceLeft -= Time.deltaTime;
        patienceBar.fillAmount = patienceLeft / maxPatience;

        if (patienceLeft <= 0)
        {
            Lan_SeatManager.Instance.MakeSeatAvailable(LanCustomer.assignedSeat);
            LanCustomer.GoAway();
        }
    }

    private void UpdateSeatWaiting()
    {
        patienceLeft -= Time.deltaTime * (maxPatience / 5f); // Speed up the countdown to fit 5 seconds
        patienceBar.fillAmount = patienceLeft / maxPatience;

        if (patienceLeft <= 0)
        {
            LanCustomer.GoAway(); // No seat becomes available, customer leaves
        }
    }

    public void SetSeatWaitingMode(bool isWaiting)
    {
        seatWaitingMode = isWaiting;
        if (isWaiting)
        {
            patienceLeft = 5f; // Reset the timer for seat waiting
        }
    }
    public void ResetPatience()
    {
        patienceLeft = maxPatience; // Reset to full patience
        patienceBar.fillAmount = 1; // Reset the UI element
    }
}
