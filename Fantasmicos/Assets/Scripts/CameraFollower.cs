using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 4, 6); // Z positivo para restarlo y quedar detrás
    public float smoothSpeed = 0.1f;

    void FixedUpdate()
    {
        if (player == null) return;

        // Separar rotación solo en el eje horizontal
        Vector3 flatOffset = new Vector3(offset.x, 0f, offset.z);
        Vector3 rotatedFlatOffset = player.rotation * flatOffset;

        // La posición deseada mantiene la altura original del offset
        Vector3 desiredPosition = player.position - rotatedFlatOffset + Vector3.up * offset.y;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}
