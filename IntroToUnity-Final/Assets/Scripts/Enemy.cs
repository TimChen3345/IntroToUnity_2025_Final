using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player1; // Assign these in the Inspector
    public Transform player2;
    public float detectionRange = 0.5f; // Distance threshold to consider "touch"

    void Update()
    {
        if (GameManager.instance == null || GameManager.instance.gameIsOver) return;

        float distanceToP1 = Vector2.Distance(transform.position, player1.position);
        float distanceToP2 = Vector2.Distance(transform.position, player2.position);

        if (distanceToP1 <= detectionRange || distanceToP2 <= detectionRange)
        {
            GameManager.instance.EnemyHitPlayer();
        }
    }
}

