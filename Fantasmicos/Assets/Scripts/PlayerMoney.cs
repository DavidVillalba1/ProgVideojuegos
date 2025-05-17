using UnityEngine;
using UnityEngine.UI; // Si usas TextMeshPro, usa TMPro en su lugar
using TMPro; // Si usas TextMeshPro, descomenta esta línea
public class PlayerMoney : MonoBehaviour
{
    public int dinero = 0;
    public TMP_Text moneyText; // Si usas TextMeshProUGUI, cámbialo por "TMP_Text"

    void Start()
    {
        ActualizarUI();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            dinero -= 10;
            ActualizarUI();
            //Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Friendly"))
        {
            dinero += 10;
            ActualizarUI();
            Destroy(collision.gameObject);
        }
    }

    void ActualizarUI()
    {
        moneyText.text = "Almas: " + dinero.ToString();
    }
}
