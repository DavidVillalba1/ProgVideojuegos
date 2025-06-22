using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIWaypointNavigator : MonoBehaviour
{
    [Header("Waypoints agrupados por tramo del circuito")]
    public List<WaypointGroup> trackSections;

    [Header("Distancia mínima para cambiar de punto")]
    public float reachThreshold = 3f;

    [Header("Vueltas")]
    [SerializeField] private int totalLaps = 2;   // ← Nº de vueltas a completar

    private NavMeshAgent agent;
    private CarRaceData  raceData;

    private System.Random rng;  // generador de números aleatorios

    private int currentSection = 0;  // índice del checkpoint/sección
    private int currentLap     = 0;  // vueltas completas
    private Transform currentTarget;

    /* -------------------- INICIALIZACIÓN -------------------- */
    private void Awake()
    {
        agent    = GetComponent<NavMeshAgent>();
        raceData = GetComponent<CarRaceData>();

        if (raceData == null)
            Debug.LogError("Falta el componente CarRaceData en " + name);
    }

    private void Start()
    {
        rng = new System.Random(GetInstanceID());
        SetNextTarget();
        agent.updateRotation = false;   // rotación manual tipo coche
    }

    /* -------------------- BUCLE DE JUEGO -------------------- */
    private void Update()
    {
        if (raceData != null && raceData.finished)
            return;   // nada más que hacer

        if (agent.pathPending || currentTarget == null)
            return;

        // Distancia al destino → útil para RaceManager
        float distance = Vector3.Distance(transform.position, currentTarget.position);

        // Actualizar CarRaceData cada frame
        if (raceData != null)
        {
            raceData.currentLap             = currentLap;
            raceData.currentCheckpointIndex = currentSection;
            raceData.distanceToNextCheckpoint = distance;
        }

        // ¿Llegó al waypoint?
        if (distance < reachThreshold)
        {
            AvanzarSeccionOlap();
            SetNextTarget();
        }

        /* Rotación suave hacia la dirección de movimiento */
        Vector3 direction = agent.desiredVelocity.normalized;
        if (direction.sqrMagnitude > 0.1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    lookRotation,
                                                    Time.deltaTime * 3f);
        }
    }

    /* -------------------- LÓGICA DE CHECKPOINTS -------------------- */
    private void AvanzarSeccionOlap()
    {
        currentSection++;

        if (currentSection >= trackSections.Count)
        {
            currentSection = 0;
            currentLap++;

            // ¿Terminó la carrera?
            if (currentLap >= totalLaps && raceData != null)
            {
                raceData.finished = true;
                agent.isStopped   = true;   // Opcional: detener el NPC
                return;
            }
        }
    }

    private void SetNextTarget()
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

        int index = rng.Next(options.Count);
        currentTarget = options[index];
        if (agent.isOnNavMesh)
            agent.SetDestination(currentTarget.position);
    }

    /* -------------------- UTILIDAD -------------------- */
    public void SetSpeed(float speed)
    {
        if (agent != null)
        {
            agent.speed = speed;
            Debug.Log($"Velocidad del NPC ajustada a {speed}");
        }
    }
}

