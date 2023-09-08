using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    // Reference to the Ball object
    [Header("References")]
    [Tooltip("Reference to the Ball object.")]
    [SerializeField]
    private Ball ball;

    // Reference to the GameManager object
    [Tooltip("Reference to the GameManager object.")]
    [SerializeField]
    private GameManager gameManager;

    // Array of weapon object
    [Tooltip("Add weapons here to be able to launch particle (shots) from")]
    [SerializeField]
    private GameObject[] weapons;

    [SerializeField]
    private float fireRate = 1f;

    // Current ball position
    [Header("Position")]
    [Tooltip("The current position of the ball.")]
    [SerializeField]
    [ReadOnly]
    private float ballPos;

    // Current mouse X position
    [Tooltip("The current X position of the mouse.")]
    private float mouseXPos;

    void Awake()
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

        } else
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
        paddlePos.x = Mathf.Clamp(ballPos, -13.5f, 4f);
        gameObject.transform.position = paddlePos;

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var powerup = other.gameObject.GetComponent<Powerup>();
        if (powerup != null)
        {
            Debug.Log("Powerup picked up");
            powerup.Destroy();
            OnPowerupPickup(powerup, other);
        }
    }

    public void OnPowerupPickup(Powerup powerup, Collider2D other)
    {
        string powerupName = powerup.GetPowerupName();

        Debug.Log("Paddle picked up power-up: " + powerupName);

        if (powerupName.Equals("Laser Powerup"))
        {
            LaserPowerup();
        }
        if (powerupName.Equals("Multiball Powerup"))
        {
            MultiBallPowerup(other);
        }
    }

    private void LaserPowerup()
    {
        Debug.Log("Shooting should occur");
        foreach (GameObject weapon in weapons)
        {
            weapon.GetComponent<LaserWeapon>().Shoot();
        }

    }

    private void MultiBallPowerup(Collider2D other)
    {
        var paddleY = transform.position.y + 1;
        var spawnPos = new Vector3(transform.position.x, paddleY, 0);
        other.gameObject.GetComponent<MultiballPowerup>().spawnBalls(spawnPos);
    }
}
