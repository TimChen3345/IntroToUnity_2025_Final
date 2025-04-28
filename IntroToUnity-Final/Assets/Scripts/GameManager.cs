using UnityEngine;
using TMPro;
using UnityEngine.UI; // Import this for Button support

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance

    public TMP_Text countdownText; // TMP Text for countdown display
    public TMP_Text scoreTextP1;   // TMP Text for Player 1's score display
    public TMP_Text scoreTextP2;   // TMP Text for Player 2's score display
    public TMP_Text resultText;    // TMP Text to display the result
    public Button restartButton;   // Button to restart the game

    public float countdownTime = 90f; // Countdown duration in seconds

    private int scoreP1 = 0;  // Player 1's score
    private int scoreP2 = 0;  // Player 2's score
    private float timeRemaining; // Time remaining in the countdown

    public bool gameIsOver = false; // Flag to indicate if the game is over

    void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        timeRemaining = countdownTime;
        resultText.gameObject.SetActive(false); // Hide the result text initially
        restartButton.gameObject.SetActive(false); // Hide the restart button initially
    }

    void Update()
    {
        if (gameIsOver)
            return;

        // Countdown logic
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            EndGame();
        }

        // Update the countdown display
        countdownText.text = "Time: " + Mathf.Ceil(timeRemaining);

        // Update the score displays
        scoreTextP1.text = "Player 1: " + scoreP1;
        scoreTextP2.text = "Player 2: " + scoreP2;
    }

    // Method to increment Player 1's score
    public void Player1Scored()
    {
        if (!gameIsOver)
        {
            scoreP1++;
        }
    }

    // Method to increment Player 2's score
    public void Player2Scored()
    {
        if (!gameIsOver)
        {
            scoreP2++;
        }
    }
    
    // Call this when the enemy touches a player
    public void EnemyHitPlayer()
    {
        if (!gameIsOver)
        {
            EndGame();
        }
    }

    // End the game and display the result
    void EndGame()
    {
        gameIsOver = true;

        // Determine the winning player
        string winner = "Draw";
        if (scoreP1 > scoreP2)
            winner = "Player 1";
        else if (scoreP2 > scoreP1)
            winner = "Player 2";

        // Display the result message
        resultText.gameObject.SetActive(true);
        resultText.text = winner + " Wins!\nPlayer 1: " + scoreP1 + "\nPlayer 2: " + scoreP2;

        // Show the restart button
        restartButton.gameObject.SetActive(true);
    }
}
