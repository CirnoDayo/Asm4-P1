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

    private void Start()
    {
        patienceLeft = maxPatience;
        patienceBar.fillAmount = 1;
    }

    private void Update()
    {
        if (!isBeingServed)
        {
        patienceLeft -= Time.deltaTime;
        patienceBar.fillAmount = patienceLeft / maxPatience;

        if (patienceLeft <= 0)
        {
            Destroy(gameObject);
        }
        }
        else
        {
            patienceBar.fillAmount = patienceLeft / maxPatience;
        }
    }
}
