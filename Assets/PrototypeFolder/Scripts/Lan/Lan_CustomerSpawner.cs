using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lan_CustomerSpawner : MonoBehaviour
{
     public GameObject customerPrefab; // Assign your customer prefab in the inspector
     public float spawnInterval = 5f; // Time in seconds between spawns
     private float timer;
 
     private void Update()
     {
         timer += Time.deltaTime;
         if (timer >= spawnInterval)
         {
             Instantiate(customerPrefab, transform.position, Quaternion.identity);
             timer = 0f; // Reset timer after spawning
         }
     }
}
