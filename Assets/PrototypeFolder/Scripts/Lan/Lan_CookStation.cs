using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lan_CookStation : MonoBehaviour
{
    public float processTime = 3.0f;
    public Vector2 overlapBoxSize;
    public LayerMask foodLayer;
    public Image progressBar;
    private GameObject currentFoodItem;
    private bool isProcessing = false;
    private AudioSource cookingSound;

    void Start()
    {
        cookingSound = GetComponent<AudioSource>();  // Get the AudioSource component
    }
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, overlapBoxSize, 0, foodLayer);
        if (!isProcessing && colliders.Length > 0)
        {
            // Process the first food item found that hasn't been processed yet
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Pickup") && collider.gameObject.GetComponent<Lan_FoodItem>() &&
                    !collider.gameObject.GetComponent<Lan_FoodItem>().IsCooked)
                {
                    currentFoodItem = collider.gameObject;
                    StartCoroutine(ProcessFood());
                    break;
                }
            }
        }

        // Check if the current food item has been removed from the station
        // Assuming 'colliders' is an array of Collider2D from OverlapBoxAll
        if (currentFoodItem != null && !Array.Exists(colliders, element => element.gameObject == currentFoodItem))
        {
            ResetProcess(); // Reset the station when the food item is no longer present
        }

    }

    IEnumerator ProcessFood()
    {
        isProcessing = true;
        progressBar.fillAmount = 0;
        cookingSound.Play(); 
        float startTime = Time.time;
        float endTime = startTime + processTime;

        while (Time.time < endTime)
        {
            progressBar.fillAmount = (Time.time - startTime) / processTime;
            yield return null;
        }

        progressBar.fillAmount = 1.0f;
        cookingSound.Stop();
        if (currentFoodItem != null)
        {
            currentFoodItem.GetComponent<Lan_FoodItem>().IsCooked = true; 
        }

        isProcessing = false;
    }

    void ResetProcess()
    {
        progressBar.fillAmount = 0;
        currentFoodItem = null;
        if (isProcessing)
        {
            cookingSound.Stop();  // Ensure sound is stopped if process is reset
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(overlapBoxSize.x, overlapBoxSize.y, 1));
    }
}