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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            dinero -= 10;
            ActualizarUI();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Friendly"))
        {
            dinero += 10;
            ActualizarUI();
            Destroy(other.gameObject);
        }
    }

    void ActualizarUI()
    {
        moneyText.text = "Almas: " + dinero.ToString();
    }
}
