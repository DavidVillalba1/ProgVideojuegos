using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public float velocidad = 5.0f;
    private Rigidbody rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if(movimiento.magnitude > 0.1f)
        {
            anim.SetBool("Run", true);

            Quaternion toRotation = Quaternion.LookRotation(movimiento, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
        }
        else
        {
            anim.SetBool("Run", false);
        }
        rb.MovePosition(transform.position + movimiento.normalized * velocidad * Time.deltaTime);
    }
}
