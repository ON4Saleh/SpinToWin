using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For UI Text
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int enemiesRemaining;
    private int currentEnemyIndex = 0; 

    [SerializeField] private string[] levelScenes; 
    [SerializeField] private string winScene = "YouWin";
    [SerializeField] private string loseScene = "YouLose";
    [SerializeField] private Text startText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        // Start the coroutine to display the "press mouse" message
        StartCoroutine(ShowStartText());

        Invoke(nameof(SetEnemyCount), 0.5f);
    }
    private IEnumerator ShowStartText()
    {
        // Show the text for 3 seconds, or until the mouse is clicked
        startText.gameObject.SetActive(true); // Make sure the text is visible
        float timer = 0f;

        // Show the text for 3 seconds or until mouse click
        while (timer < 3f)
        {
            // Debug log to check the timer
            Debug.Log("Timer: " + timer);

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse clicked, hiding text.");
                break; // Exit if mouse click is detected
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // Hide the text after 3 seconds or when the mouse is clicked
        Debug.Log("Hiding start text after 3 seconds or mouse click.");
        startText.gameObject.SetActive(false);
    }
    public void SetEnemyCount()
    {
        enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy").Length;
        Debug.Log("Total Enemies in Scene: " + enemiesRemaining);
    }

    public void EnemyDied()
    {
        enemiesRemaining--;
        Debug.Log("Enemy defeated! Remaining enemies: " + enemiesRemaining);

        if (enemiesRemaining <= 0)
        {
            Debug.Log("All enemies defeated. Moving to next scene...");

            // Ensure there's another level to load before moving to win screen
            if (currentEnemyIndex < levelScenes.Length)
            {
                // Load next level in the sequence
                SceneManager.LoadScene(levelScenes[currentEnemyIndex]);
                currentEnemyIndex++;
            }
            else
            {
                // Once all levels are done, show the win screen
                SceneManager.LoadScene(winScene);
            }
        }
    }


    public void PlayerLost()
    {
        Debug.Log("Player lost! Moving to lose scene...");
        SceneManager.LoadScene(loseScene); 
    }
}
