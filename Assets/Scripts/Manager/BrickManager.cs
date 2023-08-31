using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{

    [SerializeField]
    private int numOfBreakableBricks;

    private GameManager gameManager;
    
    public void OnDestroyBrick()
    {
        numOfBreakableBricks--;
    }
    public int RemainingBricks()
    {
        return numOfBreakableBricks;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        // Find all GameObjects with the "BreakableBrick" tag and count them
        GameObject[] breakableBricks = GameObject.FindGameObjectsWithTag("Breakable");
        numOfBreakableBricks = breakableBricks.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (numOfBreakableBricks <= 0)
        {
            gameManager.OnWin();
        }
    }
}
