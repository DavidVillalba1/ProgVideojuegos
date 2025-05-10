using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;        // el objeto a seguir (tu fantasma)
    public Vector3 offset = new Vector3(0f, 5f, -10f); // posición relativa a él
    public float smoothSpeed = 0.125f; // suavizado

    void Update()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
