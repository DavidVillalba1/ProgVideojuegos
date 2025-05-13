using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reja : MonoBehaviour
{
    public int requiredMoney = 100;
    private GameObject reja;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMoney playerMoney = other.GetComponent<PlayerMoney>();
            if (playerMoney != null && playerMoney.dinero >= requiredMoney)
            {
                reja.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
