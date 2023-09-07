using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{

    // Singleton Instance - Do not modify
    private static BrickManager _instance;

    // Singleton property to access the GameManager instance
    public static BrickManager Instance
    {
        get
        {
            if (_instance is null)
            {
                // Try to find an existing GameManager in the scene
                _instance = FindObjectOfType<BrickManager>();

                if (_instance is null)
                {
                    // Log an error if GameManager is not found
                    Debug.LogError("GameManager is not found in the scene.");
                }
            }

            return _instance;
        }
    }

    [SerializeField]
    private int numOfBreakableBricks;
    private GameManager gameManager;
    
    public void OnDestroyBrick(int amount)
    {
        numOfBreakableBricks--;

        IncreaseScore(amount);

        if (numOfBreakableBricks <= 0)
        {
            gameManager.OnWin();
        }
    }
    public int RemainingBricks()
    {
        return numOfBreakableBricks;
    }


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
         // Find all GameObjects with the "BreakableBrick" tag and count them
         GameObject[] breakableBricks = GameObject.FindGameObjectsWithTag("Breakable");
         numOfBreakableBricks = breakableBricks.Length;
    }

    public void IncreaseScore(int amount)
    {
        gameManager.IncreaseScore(amount);
    }
}
