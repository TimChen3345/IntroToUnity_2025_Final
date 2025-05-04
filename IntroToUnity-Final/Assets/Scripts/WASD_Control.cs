using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WASD_Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public AudioClip walkingClip;

    private CharacterController controller;
    private AudioSource audioSource;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = walkingClip;
        audioSource.loop = true; // Loop the walking sound
    }

    void Update()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.W))
            moveZ += 1f;
        if (Input.GetKey(KeyCode.S))
            moveZ -= 1f;
        if (Input.GetKey(KeyCode.A))
            moveX -= 1f;
        if (Input.GetKey(KeyCode.D))
            moveX += 1f;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move.normalized * moveSpeed * Time.deltaTime);

        // Play or stop walking sound based on movement
        if (move.magnitude > 0.1f)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Pause();
        }
    }
}