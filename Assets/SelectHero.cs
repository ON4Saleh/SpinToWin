using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectHero : MonoBehaviour
{
    public GameObject[] characters; // Assign all character prefabs in Inspector
    private int selectedCharacterIndex = 0;

    public void NextCharacter()
    {
        characters[selectedCharacterIndex].SetActive(false);
        selectedCharacterIndex = (selectedCharacterIndex + 1) % characters.Length;
        characters[selectedCharacterIndex].SetActive(true);
    }

    public void PrevCharacter()
    {
        characters[selectedCharacterIndex].SetActive(false);
        selectedCharacterIndex = (selectedCharacterIndex - 1 + characters.Length) % characters.Length;
        characters[selectedCharacterIndex].SetActive(true);
    }

    public void StartGame()
    {
        PlayerPrefs.SetString("SelectedCharacter", characters[selectedCharacterIndex].name); // Save the character name
        PlayerPrefs.Save();
        SceneManager.LoadScene("MergeMap");
        Debug.Log("instantiate 1");
    }
}