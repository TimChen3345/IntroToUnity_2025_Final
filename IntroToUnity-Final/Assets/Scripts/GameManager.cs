using UnityEngine;
using TMPro;
using UnityEngine.UI; // For Button

public class GameManager : MonoBehaviour
{
    public static GameManager instance;     // Singleton instance

    [Header("UI References")]
    public TMP_Text countdownText;          // Countdown display
    public TMP_Text scoreTextP1;            // Player 1 score display
    public TMP_Text scoreTextP2;            // Player 2 score display
    public TMP_Text resultText;             // Result display
    public Button restartButton;            // Restart button
    public Image  resultBackground;         // ResultBackground

    [Header("Game Settings")]
    public float countdownTime = 90f;       // Total time in seconds

    private int scoreP1 = 0;
    private int scoreP2 = 0;
    private float timeRemaining;
    public bool gameIsOver = false;

    void Awake()
    {
        // Singleton pattern
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        timeRemaining = countdownTime;
        resultText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        resultBackground.gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameIsOver)
            return;

        // Countdown
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            EndGameByScore();
        }

        // UI updates
        countdownText.text = "Time: " + Mathf.Ceil(timeRemaining);
        scoreTextP1.text = "Treasure: " + scoreP1;
        scoreTextP2.text = "Treasure: " + scoreP2;
    }

    // Called from other scripts when a player scores
    public void Player1Scored()
    {
        if (!gameIsOver)
            scoreP1++;
    }

    public void Player2Scored()
    {
        if (!gameIsOver)
            scoreP2++;
    }

    // Called by Enemy when it hits a player
    // playerHit: 1 = Player left was hit → Player Right wins
    //            2 = Player Right was hit → Player Left wins
    public void EnemyHitPlayer(int playerHit)
    {
        if (gameIsOver) return;

        gameIsOver = true;

        string winner = (playerHit == 1) ? "Player Right" : "Player Left";

        resultText.gameObject.SetActive(true);
        resultText.text = $"{winner} Wins by enemy hit!\n\nPlayer Left: {scoreP1}\nPlayer Right: {scoreP2}";
        restartButton.gameObject.SetActive(true);
        resultBackground.gameObject.SetActive(true);
    }

    // Fallback end‐game when time runs out
    private void EndGameByScore()
    {
        gameIsOver = true;

        string winner = "Draw";
        if (scoreP1 > scoreP2)
            winner = "Player Left";
        else if (scoreP2 > scoreP1)
            winner = "Player Right";

        resultText.gameObject.SetActive(true);
        resultText.text = $"{winner} Wins!\n\nPlayer Left: {scoreP1}\nPlayer Right: {scoreP2}";
        restartButton.gameObject.SetActive(true);
        resultBackground.gameObject.SetActive(true);
    }
}
