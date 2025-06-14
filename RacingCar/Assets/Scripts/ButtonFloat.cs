using UnityEngine;

public class ButtonFloat : MonoBehaviour
{
    public float amplitude = 10f; // qué tanto se mueve
    public float speed = 2f;      // qué tan rápido se mueve

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * speed) * amplitude;
        transform.localPosition = startPos + new Vector3(0, y, 0);
    }
}
