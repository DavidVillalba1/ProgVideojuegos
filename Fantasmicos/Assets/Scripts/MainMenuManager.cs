using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Jugar() {
        // Load the game scene
        SceneManager.LoadScene("SampleScene");
    }

    public void Salir() {
        // Exit the application
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}
