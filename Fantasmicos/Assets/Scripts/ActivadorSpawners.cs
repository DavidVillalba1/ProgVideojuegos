using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivadorSpawners : MonoBehaviour
{
    public GameObject[] spawners; // Array de spawners a activar/desactivar
    public bool done = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !done)
        {
            done = true; // Evitar que se active varias veces
            foreach (GameObject spawner in spawners)
            {
                spawner.SetActive(true); // Activar el spawner
            }
        }
    }
}
