using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class P2_Prize : MonoBehaviour
{
    public float triggerDistance = 1.5f; // Distance threshold for collection
    public AudioClip pickupSound;

    private GameObject player2;
    private AudioSource audioSource;

    // Define specific spawn positions
    private Vector3[] spawnPoints = new Vector3[]
    {
        new Vector3(2.82f, 0, 16.23f),
        new Vector3(-38.45f, 0, 18.26f),
        new Vector3(-28.59f, 0, 24.8f),
        new Vector3(-28.59f, 0, 82.17f),
        new Vector3(-38.47f, 0, 82.17f),
        new Vector3(-38.47f, 0, 88.83f),
        new Vector3(-31.85f, 0, 75.38f),
        new Vector3(-18.72f, 0, 71.95f),
        new Vector3(-12.08f, 0, 78.71f),
        new Vector3(-5.45f, 0, 82.02f),
        new Vector3(20.98f, 0, 92.22f),
        new Vector3(37.51f, 0, 78.77f),
        new Vector3(27.44f, 0, 78.77f),
        new Vector3(-5.27f, 0, 68.71f),
        new Vector3(11.15f, 0, 78.74f),
        new Vector3(29.93f, 0, 61.96f),
        new Vector3(-5.5f, 0, 55.04f),
        new Vector3(-5.5f, 0, 41.67f),
        new Vector3(-1.67f, 0, 38.4f),
        new Vector3(11.11f, 0, 38.4f),
        new Vector3(31.03f, 0, 44.92f),
        new Vector3(27.72f, 0, 28.27f),
        new Vector3(27.72f, 0, 24.99f),
        new Vector3(21.12f, 0, 21.34f),
        new Vector3(-25.26f, 0, 11.43f),
    };

    void Start()
    {
        player2 = GameObject.Find("Player2Cube");
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (player2 != null && Vector3.Distance(transform.position, player2.transform.position) < triggerDistance &&
            Input.GetKeyDown(KeyCode.RightShift))
        {
            audioSource.PlayOneShot(pickupSound); // Play pickup sound
            GameManager.instance.Player2Scored();  // Increment Player 1's score
            transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)];
        }
    }
}
