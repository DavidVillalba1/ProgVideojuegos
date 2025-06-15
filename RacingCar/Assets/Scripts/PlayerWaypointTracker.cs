using System.Collections.Generic;
using UnityEngine;

public class PlayerWaypointTracker : MonoBehaviour
{
    public List<WaypointGroup> trackSections;
    public float threshold = 5f; // Distancia mínima para "pasar" un punto

    private int currentSection = 0;
    private int currentLap = 0;

    private CarRaceData raceData;
    private Transform currentTarget;
    private bool initialized = false;

    void Start()
    {
        raceData = GetComponent<CarRaceData>();

        if (trackSections.Count > 0 && trackSections[0].waypoints.Count > 0)
        {
            currentTarget = GetWaypointForSection(currentSection);
        }
        else
        {
            Debug.LogError("No hay waypoints asignados al jugador.");
        }
    }

    void Update()
    {
        if (!initialized)
        {
            if (trackSections != null && trackSections.Count > 0 && trackSections[0].waypoints.Count > 0)
            {
                currentTarget = GetWaypointForSection(currentSection);
                raceData = GetComponent<CarRaceData>();
                initialized = true;
            }
            else
            {
                return; 
            }
        }

        if (raceData == null || raceData.finished || currentTarget == null) return;

        float distance = Vector3.Distance(transform.position, currentTarget.position);

        raceData.currentCheckpointIndex = currentSection;
        raceData.distanceToNextCheckpoint = distance;
        raceData.currentLap = currentLap;

        if (distance < threshold)
        {
            AvanzarCheckpoint();
        }
    }

    private void AvanzarCheckpoint()
    {
        currentSection++;

        if (currentSection >= trackSections.Count)
        {
            currentSection = 0;
            currentLap++;

            if (currentLap >= 2)
            {
                raceData.finished = true;
                return;
            }
        }

        currentTarget = GetWaypointForSection(currentSection);
    }

    private Transform GetWaypointForSection(int sectionIndex)
    {
        var group = trackSections[sectionIndex];
        if (group.waypoints.Count > 0)
        {
            return group.waypoints[0]; // Elegimos el primero para mantener consistencia
        }

        return null;
    }
}
