using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // Asegúrate de tener el paquete TextMeshPro instalado
using FWC; // Asegúrate de que este espacio de nombres sea correcto

public class TriggerFade : MonoBehaviour
{
    public GameObject finalTransition;
    public UnityEngine.UI.RawImage blackScreenImage;

    public Color startColorImage;
    public Color endColorImage;
    public float fadeDuration = 1f;
    public TMP_Text tmpText;

    public Color startColorText;
    public Color endColorText;
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player")) // Asegúrate que el jugador tenga el tag "Player"
        {
            Debug.Log("Trigger activated");
            hasTriggered = true;
            finalTransition.SetActive(true);
            blackScreenImage.color = Color.Lerp(startColorImage, endColorImage, 5f);
            tmpText.color = Color.Lerp(startColorText, endColorText, 5f);
            StartCoroutine(ChangeSceneAfterDelay(5f));
        }
    }

    private System.Collections.IEnumerator ChangeSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu");
    }
}
