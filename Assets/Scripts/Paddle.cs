using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    [Header("Audio")]
    [Tooltip("List of Audio that can be played. Specify AudioName to play sound")]
    [SerializeField]
    private List<SoundData> soundConfigurations = new List<SoundData>();

    private string soundNameToPlay;

    // Reference to the Ball object
    [Header("References")]
    [Tooltip("Reference to the Ball object.")]
    [SerializeField]
    private Ball ball;

    [Tooltip("Reference to the Camera object.")]
    [SerializeField]
    private CameraShake cameraShake;

    // Reference to the GameManager object
    [Tooltip("Reference to the GameManager object.")]
    [SerializeField]
    private GameManager gameManager;

    // Array of weapon object
    [Tooltip("Add weapons here to be able to launch particle (shots) from")]
    [SerializeField]
    private GameObject[] weapons;

    [Tooltip("The current position of the left clamp")]
    [SerializeField]
    private GameObject leftClamp;

    [Tooltip("The current position of the right clamp")]
    [SerializeField]
    private GameObject rightClamp;

    [Header("Internal variables")]
    // Current mouse X position
    private float mouseXPos;
    private float ballPos;
    private Vector3 originalBallPos;
    private float currentTime;
    private float powerupTime;
    private float PaddleLength;
    private float PaddleWidth;
    private bool shooting;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        cameraShake = FindObjectOfType<CameraShake>();
        ball = FindObjectOfType<Ball>();
        originalBallPos = ball.transform.position;

        PaddleLength = gameObject.transform.localScale.x;
        PaddleWidth = gameObject.transform.localScale.y;
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
        paddlePos = PaddleClamp(paddlePos);
        gameObject.transform.position = paddlePos;
    }

    private void AutomatedPlay()
    {
        ballPos = ball.transform.position.x;
        Vector3 paddlePos = gameObject.transform.position;
        paddlePos = PaddleClamp(paddlePos);
        gameObject.transform.position = paddlePos;

    }
    private Vector3 PaddleClamp(Vector3 paddlePos)
    {
        float leftClampPos = leftClamp.transform.position.x + PaddleLength / 2;
        float rightClampPos = rightClamp.transform.position.x - PaddleLength / 2;
        paddlePos.x = Mathf.Clamp(mouseXPos, leftClampPos, rightClampPos);
        return paddlePos;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
        {
            soundNameToPlay = "Ball";
            cameraShake.start = true;
            PlaySound();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var powerup = other.gameObject.GetComponent<Powerup>();
        if (powerup != null)
        {
            soundNameToPlay = "Powerup";
            PlaySound();
            Debug.Log("Powerup picked up");
            Destroy(other.gameObject);
            OnPowerupPickup(powerup, other);
        }
    }

    public void OnPowerupPickup(Powerup powerup, Collider2D other)
    {
        string powerupName = powerup.GetPowerupName();

        Debug.Log("Paddle picked up power-up: " + powerupName);

        if (powerupName.Equals("Laser Powerup"))
        {
            StartCoroutine(LaserPowerup(other));
        }
        if (powerupName.Equals("Multiball Powerup"))
        {
            MultiBallPowerup(other);
        }
        if (powerupName.Equals("Paddle Powerup"))
        {
            PaddlePowerup(other);
        }
        if (powerupName.Equals("Catch Powerup"))
        {
            CatchPowerup();
        }
    }

    private IEnumerator LaserPowerup(Collider2D other)
    {
        if (!shooting)
        {
            var fireRate = other.gameObject.GetComponent<LaserPowerup>().ReturnFireRate();
            var amountToShoot = other.gameObject.GetComponent<LaserPowerup>().ReturnAmountToShoot();

            for (int i = 0; i < amountToShoot; i++)
            {
                foreach (GameObject weapon in weapons)
                {
                    weapon.GetComponent<LaserWeapon>().Shoot();
                    yield return new WaitForSeconds(fireRate);
                }

                shooting = false;
            }
        }

    }

    private void MultiBallPowerup(Collider2D other)
    {
        var paddleY = transform.position.y + 1;
        var spawnPos = new Vector3(transform.position.x, paddleY, 0);
        other.gameObject.GetComponent<MultiballPowerup>().OnPickUp(spawnPos);
    }

    private void CatchPowerup()
    {
        ball.transform.position = originalBallPos;
        var ballRigidBody = ball.ReturnBallRigidbody();
        ballRigidBody.velocity = new Vector2(0, 0);
        ball.transform.SetParent(this.transform);
        gameManager.SetBallServed(true);
    }

    private void PaddlePowerup(Collider2D other)
    {
        var powerupTime = other.gameObject.GetComponent<PaddlePowerup>().ReturnPowerupTime();
        PaddleLength = other.gameObject.GetComponent<PaddlePowerup>().ReturnPaddleXSizeToChange();
        PaddleWidth = other.gameObject.GetComponent<PaddlePowerup>().ReturnPaddleYSizeToChange();
        currentTime = powerupTime;

        StartCoroutine(ChangeSize());
    }

    private IEnumerator ChangeSize()
    {
        // Apply the power-up effect
        transform.localScale = new Vector3(PaddleLength, PaddleWidth, 1);

        while (currentTime > 0)
        {
            Debug.Log("Time remaining: " + currentTime);
            // Wait for a frame and decrement the timer
            yield return new WaitForSeconds(1f);
            currentTime -= 1f;
        }
        Debug.Log("Exited while loop");

        // Restore the original paddle size
        transform.localScale = new Vector3(2, 2, 1);
        PaddleLength = transform.localScale.x;
        PaddleWidth = transform.localScale.y;
    }

    private void PlaySound()
    {
        SoundData soundToPlay = soundConfigurations.Find(soundData => soundData.audioName == soundNameToPlay);

        if (soundToPlay != null)
        {
            // Play the sound with the specified name
            EventManager.OnPlaySound.Invoke(soundToPlay);
        }
        else
        {
            Debug.LogWarning("Sound with name '" + soundNameToPlay + "' not found.");
        }
    }
}