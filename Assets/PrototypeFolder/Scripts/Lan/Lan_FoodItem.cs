using UnityEngine;

public class Lan_FoodItem : MonoBehaviour
{
    public bool isCooked = false; // Tracks if the food is cooked
    public Sprite cookedSprite; // Sprite to display when the food is cooked
    private SpriteRenderer spriteRenderer; // Sprite renderer to change the sprite

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer component missing from the food item!");
        }
    }

    // Property to manage the cooked state with sprite change
    public bool IsCooked
    {
        get => isCooked;
        set
        {
            isCooked = value;
            if (isCooked)
            {
                UpdateSprite();
            }
        }
    }

    // Method to update the sprite of the food item
    private void UpdateSprite()
    {
        if (spriteRenderer != null && cookedSprite != null)
        {
            spriteRenderer.sprite = cookedSprite;
        }
        else
        {
            Debug.LogWarning("Cooked sprite not assigned or SpriteRenderer missing!");
        }
    }
}