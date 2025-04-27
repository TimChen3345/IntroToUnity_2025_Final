using UnityEngine;

public class Arrow_Movement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get WASD input
        float moveX = 0f;
        float moveZ = 0f;

        // Check for WASD keys
        if (Input.GetKey(KeyCode.UpArrow))
            moveZ += 1f;
        if (Input.GetKey(KeyCode.DownArrow))
            moveZ -= 1f;
        if (Input.GetKey(KeyCode.LeftArrow))
            moveX -= 1f;
        if (Input.GetKey(KeyCode.RightArrow))
            moveX += 1f;

        // Create the movement vector
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        
        // Apply the movement
        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}

