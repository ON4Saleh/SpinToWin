using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int wallDamage = 4;
    [SerializeField] private int enemyHealing = 3;

    private int health;

    [Header("Player UI")]
    public Image HealthImg;
    public TextMeshProUGUI healthText;
    public static HealthManager instance;

    private void Start()
    {
        health = maxHealth;
        UpdateHealthUI(); 
        UpdateScoreUI(); 
        Debug.Log("Player health initialized to: " + health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            health -= wallDamage;
            Debug.Log("Hit a wall! Health: " + health);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (health < 100)
            {
                health = Mathf.Min(health + enemyHealing, 100);
                Debug.Log("Hit an enemy! Health: " + health);
            }
            else
            {
                Debug.Log("Health is already at maximum (100). No healing applied.");
            }
        }

        health = Mathf.Clamp(health, 0, 100);

        UpdateHealthUI();
        UpdateScoreUI();

        if (health <= 0)
        {
            Debug.Log("Player has died!");
        }
    }

    public void UpdateHealthUI()
    {
        float Wfraction = (float)health / maxHealth; 
        HealthImg.fillAmount = Wfraction;
    }

    public void UpdateScore(int scoreChange)
    {
        health += scoreChange;
        health = Mathf.Clamp(health, 0, maxHealth); 
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        healthText.text = "Score: " + health; 
    }
}