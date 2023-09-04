using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    private BrickManager brickManager;    // Keep track of number of brickable bricks remain, move to

    [SerializeField]
    public int hitLimit = 1;

    private int hitTimes;

    [SerializeField]
    private int brickWorth = 100;


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
            brickManager.OnDestroyBrick(brickWorth);
            Destroy(gameObject);
        }
    }

    public int GetHitLimits()
    {
        return hitLimit;
    }
}
