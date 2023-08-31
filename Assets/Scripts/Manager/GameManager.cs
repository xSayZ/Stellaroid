using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    Ball ball;
    [SerializeField]
    float ballSpeed;

    private Vector3 mousePos;
    bool ballServed = false;

    [SerializeField]
    bool autoPlay;


    // Start is called before the first frame update
    void Start()
    {
        ball = FindObjectOfType<Ball>();

    }

    public bool AutoPlay()
    {
        return autoPlay;
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

        ball.transform.SetParent(null);

        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;

        Cursor.visible = false;

        Vector3 ballPos = Camera.main.WorldToScreenPoint(ball.Position);

        Vector3 direction = (mousePos - ballPos);
        direction = direction.normalized;

        Vector2 force = new Vector2(direction.x * ballSpeed, direction.y * ballSpeed);

        ball.AddForce(force);
    }

    public void OnLose()
    {
        SceneManager.LoadScene(0);
    }
    public void OnWin()
    {
        SceneManager.LoadScene(0);
    }
}
