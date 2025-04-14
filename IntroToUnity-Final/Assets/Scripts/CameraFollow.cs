using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 cameraOffset;
    public float smoothTime = 0.3f;
    
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + cameraOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothTime * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
