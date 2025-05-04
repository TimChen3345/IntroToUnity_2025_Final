using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Arrow_Movement : MonoBehaviour
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
        audioSource.loop = true;
    }

    void Update()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
            moveZ += 1f;
        if (Input.GetKey(KeyCode.DownArrow))
            moveZ -= 1f;
        if (Input.GetKey(KeyCode.LeftArrow))
            moveX -= 1f;
        if (Input.GetKey(KeyCode.RightArrow))
            moveX += 1f;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move.normalized * moveSpeed * Time.deltaTime);

        // Handle walking sound
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