using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lan_CustomerSpawner : MonoBehaviour
{
    public List<GameObject> customerPrefabs; // List of customer prefabs
    public float spawnInterval = 5f; // Time in seconds between spawns
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
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