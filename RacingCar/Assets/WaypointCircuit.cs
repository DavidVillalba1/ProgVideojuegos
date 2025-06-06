// Script: WaypointCircuit.cs (asignado a WaypointManager)
using UnityEngine;

public class WaypointCircuit : MonoBehaviour
{
    public Transform[] waypoints;

    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        for (int i = 0; i < waypoints.Length; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(waypoints[i].position, 0.5f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(waypoints[i].position, waypoints[(i + 1) % waypoints.Length].position);
        }
    }
}
