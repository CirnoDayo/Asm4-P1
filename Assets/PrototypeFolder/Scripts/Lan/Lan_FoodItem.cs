using UnityEngine;

public class Lan_FoodItem : MonoBehaviour
{
    public bool isCooked = false;
    public bool isChopped = false;
    public string cookingMethod = ""; // Stores the method used (e.g., "Cooked" or "Chopped")

    public Color cookedColor = Color.red; // Color when the food is cooked
    public Color choppedColor = Color.green; // Color when the food is chopped
    private SpriteRenderer spriteRenderer; // Sprite renderer to change the color

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer component missing from the food item!");
        }
    }

    // Property to manage the cooked state with color change
    public bool IsCooked
    {
        get => isCooked;
        set
        {
            isCooked = value;
            if (isCooked)
            {
                UpdateColor(cookedColor);
            }
        }
    }

    // Property to manage the chopped state with color change
    public bool IsChopped
    {
        get => isChopped;
        set
        {
            isChopped = value;
            if (isChopped)
            {
                UpdateColor(choppedColor);
            }
        }
    }

    // Method to update the color of the sprite
    private void UpdateColor(Color newColor)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = newColor;
        }
    }
}