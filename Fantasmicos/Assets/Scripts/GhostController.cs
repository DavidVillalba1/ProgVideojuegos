using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotationSpeed = 180f; // Grados por segundo
    private Rigidbody rb;

    public Transform playerStart;

    private float horizontal;
    private float vertical;

    void Start()
    {
        transform.position = playerStart.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Captura el input en Update (fluido y continuo)
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // Rotar con A/D usando transform (más estable que con Rigidbody)
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            float rotationAmount = horizontal * rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, rotationAmount, 0f);
        }
    }

    void FixedUpdate()
    {
        // Mover hacia adelante o atrás en la dirección del jugador
        Vector3 moveDirection = transform.forward * vertical;

        if (moveDirection.magnitude > 0.1f)
        {
            rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * moveDirection.normalized);
        }
        // Detener la inercia de rotación
        rb.angularVelocity = Vector3.zero;
    }
}
