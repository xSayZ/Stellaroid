using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    Ball ball;
    [SerializeField]
    float ballSpeed;

    private Vector3 mousePos;
    bool ballServed = false;


    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<Ball>();

    }

    public bool IsBallServed()
    {
        return ballServed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ballServed)
        {
            mousePos.x = Input.mousePosition.x;
            mousePos.y = Input.mousePosition.y;
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ServeBall();
            }
        }
        else
        {
            ball.CheckVelocity();
        }
    }

    void ServeBall()
    {
        ballServed = true;

        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;

        Cursor.visible = false;

        Vector3 ballPos = Camera.main.WorldToScreenPoint(ball.Position);

        Vector3 direction = (mousePos - ballPos);
        direction = direction.normalized;

        Vector2 force = new Vector2(direction.x * ballSpeed, direction.y * ballSpeed);

        ball.AddForce(force);
    }
}
