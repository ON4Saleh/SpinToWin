using UnityEngine;

public class EnemyHealthBeyblade : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float enemyDamage = 10f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        BeybladeHealth player = collision.gameObject.GetComponent<BeybladeHealth>();

        if (player != null)
        {
            Vector3 hitPoint = collision.contacts[0].point; // Get impact position
            Debug.Log(gameObject.name + " hit " + player.gameObject.name +
                      " at " + hitPoint + " and dealt " + enemyDamage + " damage!");

            player.TakeDamage(enemyDamage);
        }
        else
        {
            Debug.Log(gameObject.name + " collided with " + collision.gameObject.name);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage! Remaining HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " is destroyed!");
        Destroy(gameObject);
    }
}
