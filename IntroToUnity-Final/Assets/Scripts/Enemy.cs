using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float detectionRange = 0.5f;

    private Collider enemyCollider;
    private Collider player1Collider;
    private Collider player2Collider;

    void Start()
    {
        enemyCollider = GetComponent<Collider>();
        player1Collider = player1.GetComponent<Collider>();
        player2Collider = player2.GetComponent<Collider>();
    }

    void Update()
    {
        if (GameManager.instance == null || GameManager.instance.gameIsOver) return;

        Vector3 closestPointToP1 = enemyCollider.ClosestPoint(player1.position);
        Vector3 closestPointOnP1 = player1Collider.ClosestPoint(transform.position);

        Vector3 closestPointToP2 = enemyCollider.ClosestPoint(player2.position);
        Vector3 closestPointOnP2 = player2Collider.ClosestPoint(transform.position);

        float realDistanceP1 = Vector3.Distance(closestPointToP1, player1.position);
        float realDistanceP1Reverse = Vector3.Distance(closestPointOnP1, transform.position);

        float realDistanceP2 = Vector3.Distance(closestPointToP2, player2.position);
        float realDistanceP2Reverse = Vector3.Distance(closestPointOnP2, transform.position);

        if (realDistanceP1 <= detectionRange || realDistanceP1Reverse <= detectionRange ||
            realDistanceP2 <= detectionRange || realDistanceP2Reverse <= detectionRange)
        {
            Debug.Log("Enemy touched a player (3D Collider)! Ending game.");
            GameManager.instance.EnemyHitPlayer();
        }
    }
}



