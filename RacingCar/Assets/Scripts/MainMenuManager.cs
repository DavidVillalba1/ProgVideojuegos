using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject statisticsMenu;
    public GameObject creditsMenu;

    public AudioSource clickSound;

    void Start()
    {
        // Asegurarse de que los menús estén ocultos al inicio
        statisticsMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

    public void ShowstatisticsMenu()
    {
        bool isActive = statisticsMenu.activeSelf;
        statisticsMenu.SetActive(!isActive);
        creditsMenu.SetActive(false);
        PlayClickSound();
    }

    public void ShowCreditsMenu()
    {
        bool isActive = creditsMenu.activeSelf;
        statisticsMenu.SetActive(false);
        creditsMenu.SetActive(!isActive);
        PlayClickSound();
    }

    public void QuitGame()
    {
        PlayClickSound();
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void StartGame()
    {   
        PlayClickSound();
        Debug.Log("Start Game");
        SceneManager.LoadScene("complete_track_demo");
    } 
    
    private void PlayClickSound()
    {
        if (clickSound != null)
            clickSound.Play();
    }
}
