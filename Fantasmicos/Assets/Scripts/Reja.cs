using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reja : MonoBehaviour
{
    public int requiredMoney = 100;
    public GameObject reja;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMoney playerMoney = collision.gameObject.GetComponent<PlayerMoney>();
            if (playerMoney != null && playerMoney.dinero >= requiredMoney)
            {
                Physics.IgnoreCollision(collision.collider, reja.GetComponent<MeshCollider>());
            }
        }
    }
}
