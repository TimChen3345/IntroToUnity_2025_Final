using UnityEngine;

public class WASD_Movement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get input for movement
        float moveX = Input.GetAxis("Horizontal"); // A & D / Left & Right
        float moveZ = Input.GetAxis("Vertical");   // W & S / Up & Down

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}

