using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void play()
    {

        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("MergeMap");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadPlayerController()
    {
        SceneManager.LoadSceneAsync("MergeMap");
    }
    public void SwitchCharacter()
    {
        SceneManager.LoadSceneAsync("SwitchCharacter");
    }
}
