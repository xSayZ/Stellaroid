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

    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private List<SoundData> soundConfigurations = new List<SoundData>();
    private string soundNameToPlay;

    [Tooltip("List of balls in current scene")]
    public List<Ball> balls;

    // Player's score
    [Header("Scoring")]
    [Tooltip("The current score.")]
    [SerializeField]
    private int score = 0;

    // Auto-play mode
    [Header("Gameplay Settings")]
    [Tooltip("Enable auto-play mode.")]
    [SerializeField]
    private bool autoPlay;

    private bool ballServed = false;
    private bool hasProcessedEvent = false;

    private void Awake()
    {
        // Singleton initialization
        _instance = this;

        // Ensure GameManager persists between scenes
        DontDestroyOnLoad(gameObject);

        levelManager = FindObjectOfType<LevelManager>();
    }

    void Start()
    {
        balls = new List<Ball>();
        balls.Add(FindObjectOfType<Ball>());

        // Load the previous score if it exists
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
        }

        EventManager.OnBallSpawned.AddListener(AddBallToList);
    }

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

    public void SetBallServed(bool value)
    {
        ballServed = value;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ballServed)
        {
            HandleBallServe();
        }
        else
        {
            foreach (Ball ball in balls)
            {
                // Check the ball's velocity
                ball.CheckVelocity();
            }
        }
    }

    private void HandleBallServe()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            ServeBall();
        }
    }

    private void ServeBall()
    {

        Vector3 targetPosition = GetMouseWorldPosition();
        foreach (Ball ball in balls)
        {
            // Example: Trigger playing the first sound in the list
            if (soundConfigurations.Count > 0)
            {
                soundNameToPlay = "Serve";

                PlaySound();
            }

            ball.Serve(targetPosition);
        }

        ballServed = true;
        HideCursor();
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y));
    }

    private void HideCursor()
    {
        Cursor.visible = false;
    }

    // Handle the event when the player loses the game
    public void OnLose()
    {
        soundNameToPlay = "Lose";

        if (balls.Count >= 1)
        {
            return;
        }
        else
        {
            PlaySound();
            levelManager.LoadCurrentLevel();
            Debug.Log("Loading scene 0 due to loss.");
        }
    }

    // Handle the event when the player wins the game
    public void OnWin()
    {
        soundNameToPlay = "Win";
        PlaySound();

        Debug.Log("Loading scene 0 due to win.");
        levelManager.LoadNextLevel();
        EventManager.OnScoreChanged.Invoke(score);
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();

        EventManager.OnScoreChanged.Invoke(score);
    }

    public void AddBallToList(Ball ball)
    {
        if(balls.Count >= 10)
        {
            return;
        }

        balls.Add(ball);
    }

    public void RemoveBallFromList(Ball ball)
    {
        balls.Remove(ball);
    }

    private void PlaySound()
    {
        SoundData soundToPlay = GameManager.Instance.soundConfigurations.Find(soundData => soundData.audioName == soundNameToPlay);

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
