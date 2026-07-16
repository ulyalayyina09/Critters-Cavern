using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;

    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float startingTime = 60f;
    private float timeRemaining;
    private bool timerRunning = true;

    [SerializeField] private GameObject endScreenPanel;
    [SerializeField] private TMPro.TextMeshProUGUI finalScoreText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        timeRemaining = startingTime;
        UpdateScoreText();
        UpdateTimerText();
    }

    void Update()
    {
        if (!timerRunning) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            timerRunning = false;
            TimeUp();
        }
        UpdateTimerText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60f);
            int seconds = Mathf.FloorToInt(timeRemaining % 60f);
            timerText.text = string.Format("Time left: {0:00}:{1:00}", minutes, seconds);
        }
    }

    private void TimeUp()
    {
        Debug.Log("Time's up!");
        timerRunning = false; // Hentikan timer kamu
        Time.timeScale = 0f;  // Bekukan pergerakan game
        
        if (endScreenPanel != null)
        {
            FinalScore();
            endScreenPanel.SetActive(true); // Munculin panel Game Over di Unity
        }
    }

    public void PlayerDied()
    {
        timerRunning = false; // Hentikan timer kamu
        Time.timeScale = 0f;  // Bekukan pergerakan game
        
        if (endScreenPanel != null)
        {
            FinalScore();
            endScreenPanel.SetActive(true); // Munculin panel Game Over di Unity
        }
        
    }

    public void FinalScore()
    {
        if (finalScoreText != null)
        {
            finalScoreText.text = score.ToString();
        }
    }
}
