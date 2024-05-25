using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Instance { get; private set; }

    public Image healthBar;
    public int maxHealth = 10;
    public int currentHealth;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            currentHealth = maxHealth;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DecreaseHealth()
    {
            currentHealth--;
            healthBar.fillAmount = currentHealth / (float)maxHealth;
            Debug.Log("Health Decreased: " + currentHealth);

            if (currentHealth <= 0)
            {
                LoadGameOver();
            }
        
    }

    private void LoadGameOver()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene("GameOver"); // Replace "GameOverScene" with the actual name of your game over scene.
    }
}