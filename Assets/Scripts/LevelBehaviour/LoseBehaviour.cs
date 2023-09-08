using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseBehaviour : MonoBehaviour
{
    private GameManager gameManager;
    private BrickManager brickManager;
    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        brickManager = GameObject.FindObjectOfType<BrickManager>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // If while waiting for transition to next level, don't check lose condition
        if (brickManager.RemainingBricks() > 0)
        {
            if (collision.tag == "Powerup")
            {
                gameManager.balls.Remove(collision.gameObject.GetComponent<Ball>());
                Destroy(collision.gameObject);
            }
            else
            {
                gameManager.OnLose();
            }
        }
    }
}
