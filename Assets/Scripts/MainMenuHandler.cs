using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    LevelManager levelManager;

    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            levelManager.LoadNextLevel();
        }
    }
}
