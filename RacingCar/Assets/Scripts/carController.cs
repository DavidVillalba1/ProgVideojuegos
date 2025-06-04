using UnityEngine;

public class carController : MonoBehaviour
{
    public float acceleration = 20f;
    public float brakingForce = 30f;
    public float turnSpeed = 50f;
    public float reverseSpeed = 10f;

    public Camera followCamera;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (followCamera == null)
        {
            Debug.LogError("No camera assigned to follow the car!");
        }
    }

    void Update()
    {
        HandleMovement();
        HandleCamera();
        ApplyGrip();
    }

    private void HandleMovement()
    {
        // Velocidad actual del coche
        float currentSpeed = Vector3.Dot(rb.velocity, transform.forward);

        // Acelerar
        if (Input.GetMouseButton(1)) // Botón derecho del ratón
        {
            rb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);
        }

        // Frenar
        if (Input.GetMouseButton(0)) // Botón izquierdo del ratón
        {
            rb.AddForce(-transform.forward * brakingForce, ForceMode.Acceleration);
        }

        // Marcha atrás
        if (Input.GetKey(KeyCode.Z))
        {
            rb.AddForce(-transform.forward * reverseSpeed, ForceMode.Acceleration);
        }

        // Girar
        float turn = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            turn = -turnSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            turn = turnSpeed * Time.deltaTime;
        }

        // Aplicar giro solo si el coche está en movimiento
        if (currentSpeed > 0.1f || currentSpeed < -0.1f)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turn, 0));
        }

        // Limitar la velocidad máxima
        float maxSpeed = 50f; // Ajusta este valor según lo necesites
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
    private void HandleCamera()
    {
        if (followCamera != null)
        {
            // Make the camera follow the car
            followCamera.transform.position = transform.position + transform.forward * 7f + Vector3.up * 3f;

            // Make the camera look at the car's forward direction
            followCamera.transform.LookAt(transform.position + transform.forward * 5f + Vector3.up * 2f);
        }
    }
        private void ApplyGrip()
    {
        Vector3 lateralVelocity = Vector3.Dot(rb.velocity, transform.right) * transform.right;
        rb.AddForce(-lateralVelocity * 5f, ForceMode.Acceleration); // Ajusta el factor 5f según sea necesario
    }
}