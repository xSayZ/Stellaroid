using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    // Singleton Instance - Do not modify
    private static GameManager _instance;

    // Singleton property to access the GameManager instance
    public static GameManager Instance
    {
        get
        {
            if (_instance is null)
            {
                // Try to find an existing GameManager in the scene
                _instance = FindObjectOfType<GameManager>();

                if (_instance is null)
                {
                    // Log an error if GameManager is not found
                    Debug.LogError("GameManager is not found in the scene.");
                }
            }

            return _instance;
        }
    }

    // References to other game components
    [Header("Game Components")]
    [SerializeField]
    private Ball ball;         
    [SerializeField]
    private LevelLoader levelLoader;

    // Ball speed setting
    [Header("Ball Settings")]
    [Tooltip("The speed at which the ball moves.")]
    [SerializeField]
    private float ballSpeed;

    // Player's score
    [Header("Scoring")]
    [Tooltip("The current score.")]
    [SerializeField]
    private int score = 0;

    // Internal variables
    private Vector3 mousePos;
    private bool ballServed = false;

    private void Awake()
    {
        // Singleton initialization
        _instance = this;

        // Ensure GameManager persists between scenes
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Find and assign references to Ball and LevelLoader components
        ball = FindObjectOfType<Ball>();
        levelLoader = FindObjectOfType<LevelLoader>();

        // Load the previous score if it exists
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
        }
    }

    // Auto-play mode
    [Header("Gameplay Settings")]
    [Tooltip("Enable auto-play mode.")]
    [SerializeField]
    private bool autoPlay;

    // Returns whether auto-play is enabled
    public bool AutoPlay()
    {
        return autoPlay;
    }

    // Returns whether the ball has been served
    public bool IsBallServed()
    {
        return ballServed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ballServed)
        {
            // Get the mouse position
            mousePos.x = Input.mousePosition.x;
            mousePos.y = Input.mousePosition.y;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ServeBall();
            }
        }
        else
        {
            // Check the ball's velocity
            ball.CheckVelocity();
        }
    }

    void ServeBall()
    {
        ballServed = true;

        // Release the ball from its parent
        ball.transform.SetParent(null);

        // Hide the cursor during gameplay
        Cursor.visible = false;

        // Calculate the direction and force to apply to the ball
        Vector3 ballPos = Camera.main.WorldToScreenPoint(ball.Position);
        Vector3 direction = (mousePos - ballPos).normalized;
        Vector2 force = new Vector2(direction.x * ballSpeed, direction.y * ballSpeed);

        // Apply the calculated force to the ball
        ball.AddForce(force);
    }

    // Handle the event when the player loses the game
    public void OnLose()
    {
        Debug.Log("Loading scene 0 due to loss.");
        SceneManager.LoadScene(0);
    }

    // Handle the event when the player wins the game
    public void OnWin()
    {
        Debug.Log("Loading scene 0 due to win.");
        SceneManager.LoadScene(0);
        EventManager.OnScoreChanged.Invoke(score);
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();

        EventManager.OnScoreChanged.Invoke(score);
    }
}
