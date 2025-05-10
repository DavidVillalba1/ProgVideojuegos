using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float velocidad = 5.0f;
    private Rigidbody rb;

    public Transform playerStart;

    void Start()
    {
        // Set the initial position of the ghost to the player's start position
        transform.position = playerStart.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (movimiento.magnitude > 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(-movimiento, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
        }
        rb.MovePosition(transform.position + movimiento.normalized * velocidad * Time.deltaTime * -1);
    }
}
