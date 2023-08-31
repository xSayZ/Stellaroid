using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    private Ball ball;
    private GameManager gameManager;

    private float ballPos;
    private float mouseXPos;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ball = FindObjectOfType<Ball>();

    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.AutoPlay())
        {
            AutomatedPlay();

        } else if (gameManager.IsBallServed() && !gameManager.AutoPlay())
        {
            MouseMovement();
        }
    }

    private void MouseMovement()
    {
        Vector3 mousePos = Input.mousePosition;
        mouseXPos = Camera.main.ScreenToWorldPoint(mousePos).x;
        Vector3 paddlePos = gameObject.transform.position;
        paddlePos.x = Mathf.Clamp(mouseXPos, -13.5f, 4f);
        gameObject.transform.position = paddlePos;
    }

    private void AutomatedPlay()
    {
        ballPos = ball.transform.position.x;
        Vector3 paddlePos = gameObject.transform.position;
        paddlePos.x = Mathf.Clamp(ballPos, -13.5f, 3.5f);
        gameObject.transform.position = paddlePos;
    }
}
