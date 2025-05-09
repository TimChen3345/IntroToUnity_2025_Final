using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Camera References")]
    public RawImage mainCamera1;
    public RawImage mainCamera2;

    [Header("Audio")]
    public AudioSource bgmSource;
    public AudioClip[] bgmClips;
    public AudioSource resultSfxSource;
    public AudioClip resultClip;
    public AudioClip countdownClip;
    public AudioSource sfxSource;

    [Header("UI References")] 
    public TMP_Text controlP1;
    public TMP_Text controlP2;
    public TMP_Text countdownText;
    public TMP_Text scoreTextP1;
    public TMP_Text scoreTextP2;
    public TMP_Text resultText;
    public Button restartButton;
    public Image resultBackground;
    public TMP_Text respawnCountdownTextP1;
    public TMP_Text respawnCountdownTextP2;
    public Image respawnBackgroundP1;
    public Image respawnBackgroundP2;
    public RawImage logo;

    [Header("Game Settings")]
    public float countdownTime = 90f;

    private int scoreP1 = 0;
    private int scoreP2 = 0;
    private float timeRemaining;
    public bool gameIsOver = false;
    private bool countdownSoundPlayed = false;

    [Header("Player Respawn Settings")]
    public GameObject player1;
    public GameObject player2;

    private Vector3 respawnLocationP1 = new Vector3(-2.17f, 0.507f, 92.53f);
    private Vector3 respawnLocationP2 = new Vector3(1.049f, 0.507f, 5.008f);

    void Awake()
    {
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
        respawnCountdownTextP1.gameObject.SetActive(false);
        respawnCountdownTextP2.gameObject.SetActive(false);
        respawnBackgroundP1.gameObject.SetActive(false);
        respawnBackgroundP2.gameObject.SetActive(false);
        logo.gameObject.SetActive(false);
        PlayRandomBGM();
    }

    void Update()
    {
        if (gameIsOver) return;

        timeRemaining -= Time.deltaTime;
        if (!countdownSoundPlayed && timeRemaining <= 10f)
        {
            countdownSoundPlayed = true;
            PlayCountdownSound();
        }

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            EndGameByScore();
        }

        countdownText.text = "Time: " + Mathf.Ceil(timeRemaining);
        scoreTextP1.text = "Treasure: " + scoreP1;
        scoreTextP2.text = "Treasure: " + scoreP2;
    }

    void PlayRandomBGM()
    {
        if (bgmClips.Length == 0 || bgmSource == null) return;
        int randomIndex = Random.Range(0, bgmClips.Length);
        bgmSource.clip = bgmClips[randomIndex];
        bgmSource.Play();
    }

    void PlayCountdownSound()
    {
        if (countdownClip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(countdownClip);
        }
    }

    public void RespawnPlayer(int playerNumber)
    {
        if (playerNumber == 1)
            StartCoroutine(RespawnPlayer1());
        else if (playerNumber == 2)
            StartCoroutine(RespawnPlayer2());
    }

    private IEnumerator RespawnPlayer1()
    {
        player1.SetActive(false);
        player1.transform.position = respawnLocationP1;
        respawnBackgroundP1.gameObject.SetActive(true);
        scoreTextP1.gameObject.SetActive(false);
        mainCamera1.gameObject.SetActive(false);
        controlP1.gameObject.SetActive(false);
        
        yield return StartCoroutine(ShowRespawnCountdown(respawnCountdownTextP1));
        
        controlP1.gameObject.SetActive(true);
        mainCamera1.gameObject.SetActive(true);
        respawnBackgroundP1.gameObject.SetActive(false);
        scoreTextP1.gameObject.SetActive(true);
        player1.SetActive(true);
    }

    private IEnumerator RespawnPlayer2()
    {
        player2.SetActive(false);
        player2.transform.position = respawnLocationP2;
        respawnBackgroundP2.gameObject.SetActive(true);
        scoreTextP2.gameObject.SetActive(false);
        mainCamera2.gameObject.SetActive(false);
        controlP2.gameObject.SetActive(false);
        
        yield return StartCoroutine(ShowRespawnCountdown(respawnCountdownTextP2));
        
        controlP2.gameObject.SetActive(true);
        mainCamera2.gameObject.SetActive(true);
        respawnBackgroundP2.gameObject.SetActive(false);
        scoreTextP2.gameObject.SetActive(true);
        player2.SetActive(true);
    }

    private IEnumerator ShowRespawnCountdown(TMP_Text countdownText)
    {
        countdownText.gameObject.SetActive(true);
        float countdown = 5f;
        while (countdown > 0f)
        {
            countdownText.text = $"Respawn...\n{countdown:F1}";
            countdown -= Time.deltaTime;
            yield return null;
        }
        countdownText.gameObject.SetActive(false);
    }

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

    private void EndGameByScore()
    {
        gameIsOver = true;

        string winner = "Draw";
        if (scoreP1 > scoreP2)
            winner = "Player Left";
        else if (scoreP2 > scoreP1)
            winner = "Player Right";

        mainCamera1.gameObject.SetActive(false);
        controlP1.gameObject.SetActive(false);
        mainCamera2.gameObject.SetActive(false);
        controlP2.gameObject.SetActive(false);
        scoreTextP1.gameObject.SetActive(false);
        scoreTextP2.gameObject.SetActive(false);
        countdownText.gameObject.SetActive(false);
        resultText.gameObject.SetActive(true);
        logo.gameObject.SetActive(true);
        resultText.text = $"{winner} Wins!\n\nPlayer Left: {scoreP1}\nPlayer Right: {scoreP2}";
        restartButton.gameObject.SetActive(true);
        resultBackground.gameObject.SetActive(true);
        PlayResultSound();
    }

    void PlayResultSound()
    {
        if (bgmSource.isPlaying)
            bgmSource.Stop();

        if (resultClip != null)
            resultSfxSource.PlayOneShot(resultClip);
    }
}


