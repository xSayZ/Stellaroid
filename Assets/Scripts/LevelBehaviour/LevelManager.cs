using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public LevelData[] levels;

    private int currentLevelIndex = 0;

    public void LoadNextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex < levels.Length)
        {
            SceneManager.LoadScene(levels[currentLevelIndex].buildIndex);
        }
        else
        {
            Debug.LogWarning("No more levels to load.");
        }
    }

    public void LoadPreviousLevel()
    {
        currentLevelIndex--;
        if (currentLevelIndex >= 0)
        {
            SceneManager.LoadScene(levels[currentLevelIndex].buildIndex);
        }
        else
        {
            Debug.LogWarning("Already at the first level.");
        }
    }
    public void LoadCurrentLevel()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene(levels[currentLevelIndex].buildIndex);
    }
}

