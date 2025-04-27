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
    }
}


