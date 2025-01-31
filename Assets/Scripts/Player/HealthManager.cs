using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    [Header("Health Settings")]
    private int maxHealth = 120;  // Set max health
    private int wallDamage = 4;   // Wall damage
    private int enemyDamage = 15; // Enemy damage

    private int health = 200; // Current health of the player

    [Header("Player UI")]
    public Image HealthImg; // UI Health Bar
    public TextMeshProUGUI healthText; // Text displaying health
    public static HealthManager instance;

    private void Start()
    {
        health = maxHealth;  // Initialize health
        UpdateHealthUI();  // Update UI on start
        UpdateScoreUI();    // Update health score UI
        Debug.Log("Player health initialized to: " + health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Prevent further actions if health is already 0
        if (health <= 0) return;

        if (collision.gameObject.CompareTag("Wall"))
        {
            health -= wallDamage;  // Decrease health if colliding with wall
            Debug.Log("Hit a wall! Health: " + health);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= enemyDamage;  // Decrease health if colliding with enemy
            Debug.Log("Hit an enemy! Took damage. Health: " + health);
        }

        // Clamp health to ensure it's between 0 and maxHealth
        health = Mathf.Clamp(health, 0, maxHealth);

        // Update the health UI
        UpdateHealthUI();

        // Update health score UI
        UpdateScoreUI();

        // If health reaches 0, the player dies and game over
        if (health == 0)
        {
            Debug.Log("Player has died! Game Over.");
            Time.timeScale = 1f;
            SceneManager.LoadScene("GameOver");
        }
    }

    // Updates the health bar UI
    public void UpdateHealthUI()
    {
        float fraction = (float)health / maxHealth;  // Get health percentage
        HealthImg.fillAmount = fraction;  // Set the health bar fill
    }

    // Updates the health text UI
    public void UpdateScoreUI()
    {
        healthText.text = "Health: " + health.ToString();  // Display current health
    }
}
