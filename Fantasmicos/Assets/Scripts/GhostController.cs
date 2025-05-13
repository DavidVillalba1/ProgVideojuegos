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
    }

    void FixedUpdate()
    {
        // ROTAR con A/D
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            float rotationAmount = horizontal * rotationSpeed * Time.fixedDeltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0f, rotationAmount, 0f);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        // MOVER hacia adelante o atrás en la dirección del jugador
        Vector3 moveDirection = transform.forward * vertical;
        if (moveDirection.magnitude > 0.1f)
        {
            rb.MovePosition(rb.position + moveDirection.normalized * speed * Time.fixedDeltaTime);
        }
    }
}
