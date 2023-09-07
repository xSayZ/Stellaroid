using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{

    private BrickManager brickManager;    // Keep track of number of brickable bricks remain, move to
    private PowerupManager powerupManager;

    [SerializeField]
    private int hitLimit = 1;
    public int HitLimit
    {
        get { return hitLimit; }
        set { hitLimit = value; }
    }

    private int hitTimes;

    [SerializeField]
    private int _brickWorth = 100;
    public int BrickWorth
    {
        get { return _brickWorth; }
        set { _brickWorth = value; }
    }

    void Awake()
    {
        brickManager = FindObjectOfType<BrickManager>();
        if(brickManager == null){
            Debug.LogError("BrickManager not found!");
        }

        powerupManager = FindObjectOfType<PowerupManager>();
        if(powerupManager == null)
        {
            Debug.LogError("PowerManager not found!");
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        hitTimes = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Breakable"))
        {
            HandleBreakableBricks();
        }
    }

    private void HandleBreakableBricks()
    {
        hitTimes++;
        if (hitTimes >= hitLimit)
        {
            brickManager.OnDestroyBrick(BrickWorth);
            powerupManager.DropRandomPickup(this.transform.position);
            Destroy(gameObject);
        }
    }
}
