using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform player;          // El jugador
    public Vector3 offset = new Vector3(0, 5, -10); // Desplazamiento de la cámara
    public float rotationSpeed = 5f;  // Velocidad de rotación de la cámara

    void LateUpdate()
    {
        if (player == null) return;

        // Rota la cámara alrededor del jugador
        transform.position = player.position + offset;

        // Hace que la cámara mire hacia el jugador
        transform.LookAt(player);

        // Controla la rotación de la cámara (opcional si quieres rotarla con el mouse)
        if (Input.GetMouseButton(1)) // Solo rotar si el botón derecho está presionado
        {
            float horizontalInput = Input.GetAxis("Mouse X");

            // Rota el jugador (y la cámara seguirá esa rotación)
            player.Rotate(0, horizontalInput * rotationSpeed, 0);
        }
    }
}
