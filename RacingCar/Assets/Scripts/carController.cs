using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Control")]
    public KeyCode accelerateKey = KeyCode.W;
    public KeyCode brakeKey = KeyCode.S;
    public KeyCode reverseKey = KeyCode.Z;
    public KeyCode turnLeftKey = KeyCode.A;
    public KeyCode turnRightKey = KeyCode.D;

    [Header("Fuerzas")]
    public float acceleration = 20f;
    public float brakingDrag = 2f;
    public float normalDrag = 0.05f;
    public float turnSpeed = 50f;
    public float reverseSpeed = 10f;
    public float maxSpeed = 50f;
    public float accelerationSmoothing = 2f;

    [Header("Downforce")]
    public float downforce = 50f;

    [Header("Cámara")]
    public Camera followCamera;
    public float cameraOffsetZ = -5f;
    public float cameraOffsetY = 2f;
    public float cameraLookAtZ = 0f;
    public float cameraLookAtY = 1f;

    [Header("Audio")]
    [SerializeField] private AudioSource engineAudioSource;
    [SerializeField] private AudioClip accelerationClip;

    private Rigidbody rb;
    private float currentAcceleration = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (followCamera == null)
        {
            Debug.LogWarning("No camera assigned to follow the car.");
        }
    }

    void Update()
    {
        HandleMovement();
        HandleCamera();
        ApplyGrip();
    }
    

    void FixedUpdate()
    {
        ApplyDownforce();
    }

    private void ApplyDownforce()
    {
        rb.AddForce(-transform.up * downforce * rb.velocity.magnitude);
    }


    private void HandleMovement()
    {
        float currentSpeed = Vector3.Dot(rb.velocity, transform.forward);
        if (Input.GetKey(accelerateKey))
        {
            currentAcceleration = Mathf.Lerp(currentAcceleration, acceleration, Time.deltaTime * accelerationSmoothing);
            rb.AddForce(transform.forward * currentAcceleration, ForceMode.Acceleration);
        }
        else
        {
            currentAcceleration = 0f;
        }
        if (rb.velocity.magnitude > 0.5f) 
        {
            if (!engineAudioSource.isPlaying)
            {
                engineAudioSource.clip = accelerationClip;
                engineAudioSource.loop = true;
                engineAudioSource.Play();
            }
        }
        else
        {
            if (engineAudioSource.isPlaying)
            {
                engineAudioSource.Stop();
            }
        }

        // Frenar (drag)
        if (Input.GetKey(brakeKey))
        {
            rb.drag = brakingDrag;
        }
        else
        {
            rb.drag = normalDrag;
        }

        // Marcha atrás
        if (Input.GetKey(reverseKey))
        {
            rb.AddForce(-transform.forward * reverseSpeed, ForceMode.Acceleration);
        }

        // Girar
        float turn = 0f;
        if (Input.GetKey(turnLeftKey)) turn = -turnSpeed * Time.deltaTime;
        if (Input.GetKey(turnRightKey)) turn = turnSpeed * Time.deltaTime;

        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0, turn, 0));
        }

        // Limitar velocidad máxima
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void HandleCamera()
    {
        if (followCamera == null) return;

        followCamera.transform.position = transform.position + transform.forward * cameraOffsetZ + Vector3.up * cameraOffsetY;
        followCamera.transform.LookAt(transform.position + transform.forward * cameraLookAtZ + Vector3.up * cameraLookAtY);
    }

    private void ApplyGrip()
    {
        Vector3 lateralVelocity = Vector3.Dot(rb.velocity, transform.right) * transform.right;
        rb.AddForce(-lateralVelocity * 5f, ForceMode.Acceleration);
    }
}
