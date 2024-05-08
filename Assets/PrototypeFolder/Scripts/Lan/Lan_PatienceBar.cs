using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lan_PatienceBar : MonoBehaviour
{
    public Image patienceBar;
    public float maxPatience = 30f; // Max time the customer will wait
    private float patienceLeft;
    public bool isBeingServed = false;
    public Lan_Customer LanCustomer;

    private void Start()
    {
        patienceLeft = maxPatience;
        patienceBar.fillAmount = 1;
        LanCustomer = FindObjectOfType<Lan_Customer>();
    }

    private void Update()
    {
        if (!isBeingServed)
        {
        patienceLeft -= Time.deltaTime;
        patienceBar.fillAmount = patienceLeft / maxPatience;

        if (patienceLeft <= 0)
        {
            Lan_SeatManager.Instance.MakeSeatAvailable(LanCustomer.assignedSeat);
            LanCustomer.GoAway();
        }
        }
        else
        {
            patienceBar.fillAmount = patienceLeft / maxPatience;
        }
    }
}
