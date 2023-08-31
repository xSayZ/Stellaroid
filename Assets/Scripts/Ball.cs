using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private Rigidbody2D rb;
    private Paddle paddle;
    private Vector3 ballToPaddle;
    private bool gameStarted;

    public Vector3 Position
    {
        get
        {
            return transform.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        paddle = GameObject.FindObjectOfType<Paddle>();
        rb = GetComponent<Rigidbody2D>();
        gameStarted = false;
        
    }

    public void AddForce(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public void CheckVelocity()
    {
        if(rb.velocity.x == 0)
        {
            rb.velocity = new Vector2(Random.Range(1, 3), rb.velocity.y);
        }
        else if(rb.velocity.y == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, Random.Range(1, 3));
        }
    }
}
