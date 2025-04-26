using UnityEngine;
using UnityEngine.AI;

public class ChaseReturn : MonoBehaviour
{
    public Transform player1;           // Assign Player1's transform in the Inspector
    public Transform player2;           // Assign Player2's transform in the Inspector
    public float chaseRange = 20f;      // Distance within which the enemy starts chasing

    private NavMeshAgent agent;
    private Vector3 startPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position; // Store the initial position
    }

    void Update()
    {
        // Calculate distances to both players
        float distanceToPlayer1 = Vector3.Distance(transform.position, player1.position);
        float distanceToPlayer2 = Vector3.Distance(transform.position, player2.position);

        // Determine which player is within chase range
        if (distanceToPlayer1 <= chaseRange && distanceToPlayer2 <= chaseRange)
        {
            // Both players are within range; chase the closest one
            Transform closestPlayer = (distanceToPlayer1 < distanceToPlayer2) ? player1 : player2;
            agent.SetDestination(closestPlayer.position);
        }
        else if (distanceToPlayer1 <= chaseRange)
        {
            // Only Player1 is within range
            agent.SetDestination(player1.position);
        }
        else if (distanceToPlayer2 <= chaseRange)
        {
            // Only Player2 is within range
            agent.SetDestination(player2.position);
        }
        else
        {
            // Neither player is within range; return to the start position
            agent.SetDestination(startPosition);
        }
    }
}


