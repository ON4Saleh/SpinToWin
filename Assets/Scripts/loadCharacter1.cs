using UnityEngine;
public class loadCharacter : MonoBehaviour
{ 
    public GameObject[] characterPrefabs; // Assign prefabs in Inspector
    public Transform spawnPoint;

    private void Start()
    {
        string selectedCharacterName = PlayerPrefs.GetString("SelectedCharacter", characterPrefabs[0].name);

        foreach (GameObject prefab in characterPrefabs)
        {
            if (prefab.name == selectedCharacterName)
            {
                Debug.Log("instantiate 2");

                GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
                clone.transform.SetParent(spawnPoint);
              
                break;
                
            }
        }
    }
}