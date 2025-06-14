using UnityEngine;

public class CarRaceData : MonoBehaviour
{
    [Header("Solo lectura para RaceManager")]
    public int   currentLap             = 0;
    public int   currentCheckpointIndex = 0;
    public float distanceToNextCheckpoint = 0f;
    public bool  finished  = false;   // ponlo en true cuando complete las 2 vueltas

    [Header("Marca al jugador")]
    public bool  isPlayer = false;
}
