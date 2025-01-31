using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int enemiesRemaining;

    [SerializeField] private string nextLevelScene = "MergeMapLvl2"; // Set in Inspector
    [SerializeField] private string winScene = "YouWin";

    private void Awake()
    {
        // Singleton pattern to ensure only one GameManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep the GameManager across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Delay the setting of the enemy count to allow all enemies to spawn
        Invoke(nameof(SetEnemyCount), 0.5f);
    }

    public void SetEnemyCount()
    {
        // Find all enemies in the scene and count them
        enemiesRemaining = FindObjectsOfType<EnemyHealthManagment>().Length;
        Debug.Log("Total Enemies in Scene: " + enemiesRemaining);
    }

    public void EnemyDied()
    {
        // Decrease the number of remaining enemies when one dies
        enemiesRemaining--;
        Debug.Log("Enemy defeated! Remaining enemies: " + enemiesRemaining);

        if (enemiesRemaining <= 0)
        {
            // If all enemies are defeated, load the win screen or the next level
            Debug.Log("All enemies defeated. Moving to next scene...");

            // Check if the current scene is not the final level
            if (SceneManager.GetActiveScene().name != "MergeMapLvl2")
            {
                SceneManager.LoadScene(nextLevelScene); // Load next level
            }
            else
            {
                SceneManager.LoadScene(winScene); // Load win screen
            }
        }
    }
}
