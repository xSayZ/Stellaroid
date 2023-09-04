using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{

    [SerializeField]
    private int numOfBreakableBricks;

    private GameManager gameManager;
    private LevelLoader levelLoader;
    
    public void OnDestroyBrick()
    {
        numOfBreakableBricks--;

        if (numOfBreakableBricks <= 0)
        {
            gameManager.OnWin();
        }
    }
    public int RemainingBricks()
    {
        return numOfBreakableBricks;
    }

    // Start is called before the first frame update
    void Start()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
        if (levelLoader.LevelLoad())
        {
            gameManager = GameObject.FindObjectOfType<GameManager>();
            // Find all GameObjects with the "BreakableBrick" tag and count them
            GameObject[] breakableBricks = GameObject.FindGameObjectsWithTag("Breakable");
            numOfBreakableBricks = breakableBricks.Length;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
