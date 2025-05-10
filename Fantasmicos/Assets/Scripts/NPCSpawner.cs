using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab;
    public Transform centerPoint;
    public float spawnRadius = 5f;
    public float spawnInterval = 5f;
    public int maxNPCs = 10;

    private int currentNPCs = 0;

    void Start()
    {
        InvokeRepeating(nameof(SpawnNPC), 0f, spawnInterval);
    }

    void SpawnNPC()
    {
        if (currentNPCs >= maxNPCs) return;

        Vector3 randomOffset = new Vector3(
            Random.Range(-spawnRadius, spawnRadius),
            0,
            Random.Range(-spawnRadius, spawnRadius)
        );

        Vector3 spawnPosition = centerPoint.position + randomOffset;

        GameObject npc = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
        currentNPCs++;

        npc.AddComponent<NPCTracker>().spawner = this;
    }

    public void NotifyNPCDestroyed()
    {
        currentNPCs--;
    }
}
