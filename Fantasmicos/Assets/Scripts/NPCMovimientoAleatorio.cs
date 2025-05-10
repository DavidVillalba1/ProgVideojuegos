using UnityEngine;
using System.Collections;

public class NPCMovimientoAleatorio : MonoBehaviour
{
    public float moveRadius = 5f;
    public float speed = 2f;
    public float waitTime = 2f;

    private Vector3 targetPosition;
    private Vector3 centerPoint = new Vector3(-1f, 0.0f, -21f); // Establecemos el punto fijo aquí

    void Start()
    {
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            // Generamos una nueva posición aleatoria alrededor del centro
            targetPosition = centerPoint + new Vector3(
                Random.Range(-moveRadius, moveRadius),
                0,
                Random.Range(-moveRadius, moveRadius)
            );

            // Movemos el NPC hacia la posición objetivo
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                Vector3 dir = (targetPosition - transform.position).normalized;
                transform.position += dir * speed * Time.deltaTime;
                if (dir != Vector3.zero) transform.forward = dir;
                yield return null;
            }

            // Esperamos un poco antes de moverse a una nueva posición
            yield return new WaitForSeconds(waitTime);
        }
    }
}
