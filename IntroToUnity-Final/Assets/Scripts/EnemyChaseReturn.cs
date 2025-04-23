using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseReturn : MonoBehaviour
{
    public Transform player;           // Assign the player's transform in the Inspector
    public float chaseRange = 20f;     // Distance within which the enemy starts chasing

    private NavMeshAgent agent;
    private Vector3 startPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        startPosition = transform.position; // Store the initial position
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange)
        {
            // Chase the player
            agent.SetDestination(player.position);
        }
        else // Return to the start position if not already there
        { 
            agent.SetDestination(startPosition);
        }
    } 
}

