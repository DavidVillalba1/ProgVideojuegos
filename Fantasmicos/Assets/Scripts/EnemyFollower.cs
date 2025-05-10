using UnityEngine;

public class EnemyFollower : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public float stoppingDistance = 0f;

    void Start()
    {
        // Busca automáticamente el jugador si no se asignó
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                target = playerObj.transform;
            }
        }
    }

    void Update()
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;
        float distance = direction.magnitude;

        if (distance > stoppingDistance)
        {
            Vector3 moveDirection = direction.normalized;
            transform.position += moveDirection * speed * Time.deltaTime;

            // Opcional: el enemigo mira hacia el jugador
            transform.forward = moveDirection;
        }
    }
}
