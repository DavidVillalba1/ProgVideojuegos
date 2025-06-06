using System.Collections;
using UnityEngine;

public class CameraFollowere : MonoBehaviour
{
    public Transform target; // Asigna el objeto a seguir desde el inspector
    public Vector3 offset = new Vector3(0, 5, -10); // Offset para vista en tercera persona
    public float smoothSpeed = 0.125f; // Suavizado del movimiento

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + target.TransformDirection(offset);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}