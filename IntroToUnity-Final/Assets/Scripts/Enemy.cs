// using UnityEngine;
//
// public class Enemy : MonoBehaviour
// {
//     [Header("Player Transforms")]
//     public Transform player1;
//     public Transform player2;
//
//     [Header("Detection Settings")]
//     public float detectionRange = 0.5f;
//
//     private Collider enemyCollider;
//     private Collider player1Collider;
//     private Collider player2Collider;
//
//     void Start()
//     {
//         // Cache references to all colliders
//         enemyCollider   = GetComponent<Collider>();
//         player1Collider = player1.GetComponent<Collider>();
//         player2Collider = player2.GetComponent<Collider>();
//     }
//
//     void Update()
//     {
//         // Do nothing if the game is over or GameManager is missing
//         if (GameManager.instance == null || GameManager.instance.gameIsOver)
//             return;
//
//         // Compute the closest points between this enemy and each player
//         Vector3 closestToP1       = enemyCollider.ClosestPoint(player1.position);
//         Vector3 player1ToEnemy    = player1Collider.ClosestPoint(transform.position);
//         Vector3 closestToP2       = enemyCollider.ClosestPoint(player2.position);
//         Vector3 player2ToEnemy    = player2Collider.ClosestPoint(transform.position);
//
//         // Calculate actual distances
//         float dist1              = Vector3.Distance(closestToP1, player1.position);
//         float dist1Reverse       = Vector3.Distance(player1ToEnemy, transform.position);
//         float dist2              = Vector3.Distance(closestToP2, player2.position);
//         float dist2Reverse       = Vector3.Distance(player2ToEnemy, transform.position);
//
//         // Check for collision with Player 1
//         if (dist1 <= detectionRange || dist1Reverse <= detectionRange)
//         {
//             Debug.Log("Enemy touched Player 1. Player 2 wins.");
//             GameManager.instance.EnemyHitPlayer(1);
//         }
//         // Otherwise check for collision with Player 2
//         else if (dist2 <= detectionRange || dist2Reverse <= detectionRange)
//         {
//             Debug.Log("Enemy touched Player 2. Player 1 wins.");
//             GameManager.instance.EnemyHitPlayer(2);
//         }
//     }
// }

using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Player Transforms")]
    public Transform player1;
    public Transform player2;

    [Header("Detection Settings")]
    public float detectionRange = 0.5f;
    public AudioClip detectionSound; // Sound to play when player is in range

    private Collider enemyCollider;
    private Collider player1Collider;
    private Collider player2Collider;
    private AudioSource audioSource; // AudioSource to play the sound

    void Start()
    {
        // Cache references to all colliders
        enemyCollider   = GetComponent<Collider>();
        player1Collider = player1.GetComponent<Collider>();
        player2Collider = player2.GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component

        // Ensure an AudioSource exists
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Add one if missing
        }
    }

    void Update()
    {
        // Do nothing if the game is over or GameManager is missing
        if (GameManager.instance == null || GameManager.instance.gameIsOver)
            return;

        // Compute the closest points between this enemy and each player
        Vector3 closestToP1       = enemyCollider.ClosestPoint(player1.position);
        Vector3 player1ToEnemy    = player1Collider.ClosestPoint(transform.position);
        Vector3 closestToP2       = enemyCollider.ClosestPoint(player2.position);
        Vector3 player2ToEnemy    = player2Collider.ClosestPoint(transform.position);

        // Calculate actual distances
        float dist1              = Vector3.Distance(closestToP1, player1.position);
        float dist1Reverse       = Vector3.Distance(player1ToEnemy, transform.position);
        float dist2              = Vector3.Distance(closestToP2, player2.position);
        float dist2Reverse       = Vector3.Distance(player2ToEnemy, transform.position);

        // Check for collision with Player 1
        if (dist1 <= detectionRange || dist1Reverse <= detectionRange)
        {
            Debug.Log("Enemy touched Player 1. Respawning...");
            GameManager.instance.RespawnPlayer(1); // Trigger respawn
            PlayDetectionSound(); // Play sound when Player 1 is detected
        }
        // Otherwise check for collision with Player 2
        else if (dist2 <= detectionRange || dist2Reverse <= detectionRange)
        {
            Debug.Log("Enemy touched Player 2. Respawning...");
            GameManager.instance.RespawnPlayer(2); 
            PlayDetectionSound(); // Play sound when Player 2 is detected
        }
    }

    // Play the detection sound
    private void PlayDetectionSound()
    {
        if (audioSource != null && detectionSound != null)
        {
            audioSource.PlayOneShot(detectionSound);
        }
    }
}