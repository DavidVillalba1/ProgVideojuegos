using UnityEngine;

public class SpeedModifierTrigger : MonoBehaviour
{
    [Header("Nueva velocidad para el NPC al entrar en esta zona")]
    public float newSpeed = 5f;

    private void OnTriggerEnter(Collider other)
    {
        AIWaypointNavigator npc = other.GetComponent<AIWaypointNavigator>();
        if (npc != null)
        {
            npc.SetSpeed(newSpeed);
        }
    }
}
