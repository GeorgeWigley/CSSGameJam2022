using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private int currentLevel;
    public List<string> levels;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadMainMenu()
    {
        LoadASceneAsync("MainMenu");
    }

    public void LoadNextLevel() 
    {
        currentLevel++;
        StartCoroutine(LoadASceneAsync(levels[currentLevel]));

    }

    private IEnumerator LoadASceneAsync(string scene)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
