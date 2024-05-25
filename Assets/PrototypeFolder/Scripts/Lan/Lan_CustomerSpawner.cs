using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lan_CustomerSpawner : MonoBehaviour
{
    public List<GameObject> customerPrefabs; // List of customer prefabs
    public float initialSpawnInterval = 3f; // Time in seconds for the first spawn
    public float regularSpawnInterval = 10f; // Time in seconds between regular spawns
    private float timer;
    private bool isFirstCustomerSpawned = false; // Flag to track if the first customer has been spawned

    private void Update()
    {
        timer += Time.deltaTime;

        // Check if the timer exceeds the current required spawn interval
        if (!isFirstCustomerSpawned && timer >= initialSpawnInterval)
        {
            SpawnCustomer();
            timer = 0f; // Reset timer after spawning
            isFirstCustomerSpawned = true; // Update flag after first customer spawn
        }
        else if (isFirstCustomerSpawned && timer >= regularSpawnInterval)
        {
            SpawnCustomer();
            timer = 0f; // Reset timer after spawning
        }
    }

    private void SpawnCustomer()
    {
        if (customerPrefabs.Count == 0)
        {
            Debug.LogWarning("No customer prefabs assigned.");
            return;
        }

        int randomIndex = Random.Range(0, customerPrefabs.Count);
        GameObject selectedPrefab = customerPrefabs[randomIndex];
        Instantiate(selectedPrefab, transform.position, Quaternion.identity);
    }
}