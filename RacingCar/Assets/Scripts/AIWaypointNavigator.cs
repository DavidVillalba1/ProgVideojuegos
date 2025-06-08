using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIWaypointNavigator : MonoBehaviour
{
    [Header("Waypoints agrupados por tramo del circuito")]
    public List<WaypointGroup> trackSections;

    [Header("Distancia mínima para cambiar de punto")]
    public float reachThreshold = 3f;

    private NavMeshAgent agent;
    private int currentSection = 0;
    private Transform currentTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetNextTarget();
        agent.updateRotation = false;

    }

    void Update()
    {
        if (agent.pathPending || currentTarget == null)
            return;

        float distance = Vector3.Distance(transform.position, currentTarget.position);
        if (distance < reachThreshold)
        {
            currentSection = (currentSection + 1) % trackSections.Count;
            SetNextTarget();
        }

        // Movimiento estilo coche: rotación suave hacia la dirección de movimiento
        Vector3 direction = agent.desiredVelocity.normalized;
        if (direction.sqrMagnitude > 0.1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
        }
    }


    void SetNextTarget()
    {
        if (trackSections.Count == 0)
        {
            Debug.LogWarning("No hay secciones de waypoints asignadas.");
            return;
        }

        List<Transform> options = trackSections[currentSection].waypoints;
        if (options == null || options.Count == 0)
        {
            Debug.LogWarning($"La sección {currentSection} no tiene waypoints.");
            return;
        }

        currentTarget = options[Random.Range(0, options.Count)];
        if (agent.isOnNavMesh)
            agent.SetDestination(currentTarget.position);
    }
}
