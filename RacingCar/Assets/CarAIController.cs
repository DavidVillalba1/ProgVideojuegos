// Script: CarAIController.cs (asignado al coche IA)
using UnityEngine;
using UnityEngine.AI;

public class CarAIController : MonoBehaviour
{
    public WaypointCircuit circuit;
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;

    public float waypointThreshold = 5.0f; // distancia mínima para cambiar de objetivo

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (circuit != null && circuit.waypoints.Length > 0)
        {
            agent.SetDestination(circuit.waypoints[0].position);
        }
    }

    void Update()
    {
        if (circuit == null || circuit.waypoints.Length == 0) return;

        float distance = Vector3.Distance(transform.position, circuit.waypoints[currentWaypointIndex].position);
        if (distance < waypointThreshold)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % circuit.waypoints.Length;
            agent.SetDestination(circuit.waypoints[currentWaypointIndex].position);
        }
    }
}
