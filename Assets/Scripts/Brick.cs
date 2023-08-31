using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    private BrickManager brickManager;    // Keep track of number of brickable bricks remain, move to

    [SerializeField]
    public int hitLimit;

    private int hitTimes;


    // Start is called before the first frame update
    void Start()
    {
        hitTimes = 0;
        brickManager = GameObject.FindObjectOfType<BrickManager>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.tag == "Breakable")
        {
            HandleBreakableBricks();
        }
        else
        {

        }
    }

    private void HandleBreakableBricks()
    {
        hitTimes++;
        if (hitTimes >= hitLimit)
        {
            brickManager.OnDestroyBrick();
            Destroy(gameObject);
        }
    }
}
