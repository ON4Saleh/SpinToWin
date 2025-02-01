using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManagment : MonoBehaviour
{
    [Header("Health Settings")]
    int maxHealth = 150; // Increased to make enemies stronger
    int wallDamage = 3;  // Increased wall damage for more impact
    int playerDamage = 4; // Decreased player damage for balance

    private int health;

    [Header("Enemy UI")]
    public Image HealthImg;

    private void Start()
    {
        health = maxHealth;
        UpdateHealthUI();
        Debug.Log(gameObject.name + " health initialized to: " + health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(gameObject.name + " collided with " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Wall"))
        {
            health -= wallDamage;
            Debug.Log(gameObject.name + " hit a wall! Health: " + health);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            // Ensure we only take damage once per collision
            if (health > 0)
            {
                health -= playerDamage;
                Debug.Log(gameObject.name + " hit the Player! Took damage. Health: " + health);
            }
        }

        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        if (health == 0)
        {
            Debug.Log(gameObject.name + " has died!");

            // Ensure GameManager.instance is not null before calling EnemyDied
            if (GameManager.instance != null)
            {
                GameManager.instance.EnemyDied();
            }
            else
            {
                Debug.LogError("GameManager instance is null!");
            }

            Destroy(gameObject);
        }
    }

    public void UpdateHealthUI()
    {
        float fraction = (float)health / maxHealth;
        HealthImg.fillAmount = fraction;
    }
}
