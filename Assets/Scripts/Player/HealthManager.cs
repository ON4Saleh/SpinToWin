using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Health Settings")]
    private int maxHealth = 100;  // Set max health
    private int wallDamage = 4;   // Wall damage
    private int enemyDamage = 3;  // Healing from enemy (not damage)
    private int bulletDamage = 1; // Bullet damage

    private int health = 200; // Current health of the player
    private int bulletCount = 0;  // To track how many bullets have hit the player

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
            health += enemyDamage;  // Instead of damage, player now heals
            Debug.Log("Touched an enemy! Healed " + enemyDamage + ". Health: " + health);
        }
        else if (collision.gameObject.CompareTag("Enemy2"))
        {
            health += enemyDamage;  // Instead of damage, player now heals
            Debug.Log("Touched an enemy! Healed " + enemyDamage + ". Health: " + health);
        }

        // Clamp health to ensure it doesn't exceed maxHealth
        health = Mathf.Clamp(health, 0, maxHealth);

        // Update the health UI
        UpdateHealthUI();
        UpdateScoreUI();

        // If health reaches 0, the player dies and game over
        if (health == 0)
        {
            Debug.Log("Player has died! Game Over.");
            Time.timeScale = 1f;
            SceneManager.LoadScene("GameOver");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            bulletCount++;  // Increment the bullet collision count
            Debug.Log("Hit by a bullet! Bullet count: " + bulletCount);

            // Check if the player has been hit by 4 bullets
            if (bulletCount >= 2)
            {
                TakeDamage(bulletDamage);  // Take damage after 4 bullets
                bulletCount = 0;  // Reset the bullet count
            }
        }
    }

    // Method to handle taking damage
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;  // Decrease health by damage amount
        health = Mathf.Clamp(health, 0, maxHealth);  // Clamp health to 0 if it goes negative
        Debug.Log("Player took " + damageAmount + " damage. Current Health: " + health);

        // Update the health UI after damage
        UpdateHealthUI();
        UpdateScoreUI();

        // If health reaches 0, the player dies and game over
        if (health <= 0)
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
