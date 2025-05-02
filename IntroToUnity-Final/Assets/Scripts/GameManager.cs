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
        scoreTextP1.text = "Player 1: " + scoreP1;
        scoreTextP2.text = "Player 2: " + scoreP2;
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
    // playerHit: 1 = Player 1 was hit → Player 2 wins
    //            2 = Player 2 was hit → Player 1 wins
    public void EnemyHitPlayer(int playerHit)
    {
        if (gameIsOver) return;

        gameIsOver = true;

        string winner = (playerHit == 1) ? "Player 2" : "Player 1";

        resultText.gameObject.SetActive(true);
        resultText.text = $"{winner} Wins by enemy hit!\nPlayer 1: {scoreP1}\nPlayer 2: {scoreP2}";
        restartButton.gameObject.SetActive(true);
    }

    // Fallback end‐game when time runs out
    private void EndGameByScore()
    {
        gameIsOver = true;

        string winner = "Draw";
        if (scoreP1 > scoreP2)
            winner = "Player 1";
        else if (scoreP2 > scoreP1)
            winner = "Player 2";

        resultText.gameObject.SetActive(true);
        resultText.text = $"{winner} Wins!\nPlayer 1: {scoreP1}\nPlayer 2: {scoreP2}";
        restartButton.gameObject.SetActive(true);
    }
}
