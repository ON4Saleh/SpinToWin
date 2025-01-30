using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int initialHealth = 100; // Initial health of the player
    [SerializeField] private int wallDamage = 4;     // Health lost when colliding with a wall
    [SerializeField] private int enemyHealing = 3;   // Health gained when colliding with an enemy

    private int health; // Current health of the player

    private void Start()
    {
        // Initialize health to the initial value
        health = initialHealth;
        Debug.Log("Player health initialized to: " + health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collided with a wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Reduce health by wallDamage
            health -= wallDamage;
            Debug.Log("Hit a wall! Health: " + health);
        }

        // Check if the player collided with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Only heal if health is less than 100
            if (health < 100)
            {
                // Increase health by enemyHealing, but cap it at 100
                health = Mathf.Min(health + enemyHealing, 100);
                Debug.Log("Hit an enemy! Health: " + health);
            }
            else
            {
                Debug.Log("Health is already at maximum (100). No healing applied.");
            }
        }

        // Clamp health to ensure it stays within a reasonable range (0 to 100)
        health = Mathf.Clamp(health, 0, 100);

        // Check if health drops to 0 or below
        if (health <= 0)
        {
            Debug.Log("Player has died!");
            // Add logic for player death (e.g., restart level, game over screen, etc.)
        }
    }
}