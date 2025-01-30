//using UnityEngine;

//public class BeybladeHealth : MonoBehaviour
//{
//    [SerializeField] private float maxHealth = 100f;
//    [SerializeField] private float playerDamage = 20f;
//    private float currentHealth;

//    private void Start()
//    {
//        currentHealth = maxHealth;
//    }

//    private void OnCollisionEnter(Collision collision)
//    {
//        EnemyHealthBeyblade enemy = collision.gameObject.GetComponent<EnemyHealthBeyblade>();

//        if (enemy != null)
//        {
//            Vector3 hitPoint = collision.contacts[0].point; // Get impact position
//            Debug.Log(gameObject.name + " hit " + enemy.gameObject.name +
//                      " at " + hitPoint + " and dealt " + playerDamage + " damage!");

//            enemy.TakeDamage(playerDamage);
//        }
//        else
//        {
//            Debug.Log(gameObject.name + " collided with " + collision.gameObject.name);
//        }
//    }

//    public void TakeDamage(float damage)
//    {
//        currentHealth -= damage;
//        Debug.Log(gameObject.name + " took " + damage + " damage! HP: " + currentHealth);

//        if (currentHealth <= 0)
//        {
//            Die();
//        }
//    }

//    private void Die()
//    {
//        Debug.Log(gameObject.name + " is destroyed!");
//        Destroy(gameObject);
//    }
//}
