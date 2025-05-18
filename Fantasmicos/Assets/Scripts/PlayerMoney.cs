using UnityEngine;
using UnityEngine.UI; // Si usas TextMeshPro, usa TMPro en su lugar
using TMPro; // Si usas TextMeshPro, descomenta esta línea
using FWC;
using UnityEngine.EventSystems;

public class PlayerMoney : MonoBehaviour
{
    public int dinero = 0;
    public TMP_Text moneyText; // Si usas TextMeshProUGUI, cámbialo por "TMP_Text"

    private AudioSource sfxSource;
    public AudioClip clipBadGhost, clipGoodGhost;


    void Start()
    {
        ActualizarUI();
        var manager = FindObjectOfType<MenuManagerPropio>();
        if (manager != null)
        {
            sfxSource = manager.sfxSource;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            dinero -= 10;
            ActualizarUI();
            sfxSource.PlayOneShot(clipBadGhost);
            //Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Friendly"))
        {
            dinero += 10;
            ActualizarUI();
            sfxSource.PlayOneShot(clipGoodGhost);
            Destroy(collision.gameObject);
        }
    }

    void ActualizarUI()
    {
        moneyText.text = "Almas: " + dinero.ToString();
    }
}
