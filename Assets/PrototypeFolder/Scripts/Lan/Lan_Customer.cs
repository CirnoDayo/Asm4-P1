using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lan_Customer : MonoBehaviour
{
    public List<GameObject> foodIcons; // List of food icon prefabs
    public GameObject requestPopup; // UI popup for requests

    private void Start()
    {
        GenerateRequest();
    }

    private void GenerateRequest()
    {
        int foodIndex = Random.Range(0, foodIcons.Count);
        Transform placeToInstantiate = requestPopup.transform;
        Debug.Log(requestPopup.transform);  
        GameObject foodRequest = Instantiate(foodIcons[foodIndex], placeToInstantiate);
        foodRequest.transform.localPosition = Vector3.zero;
        
    }
}
