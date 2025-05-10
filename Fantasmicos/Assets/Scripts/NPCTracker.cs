using UnityEngine;

public class NPCTracker : MonoBehaviour
{
    public NPCSpawner spawner;

    void OnDestroy()
    {
        if (spawner != null)
            spawner.NotifyNPCDestroyed();
    }
}
