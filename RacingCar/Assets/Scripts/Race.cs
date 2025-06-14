using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RaceManager : MonoBehaviour
{
    [Header("Coches y posiciones de salida")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject playerCar;
    [SerializeField] private GameObject[] npcCars;

    [Header("Cámara principal")]
    [SerializeField] private Camera mainCamera;         // ← Cámara normal
    [SerializeField] private Vector3 cameraOffset;       // ← Posición relativa al jugador

    [Header("Inicio de carrera")]
    [SerializeField] private KeyCode startKey = KeyCode.Space;
    [SerializeField] private GameObject pressKeyPanel;
    [SerializeField] private TextMeshProUGUI countdownText;

    [Header("UI carrera")]
    [SerializeField] private TextMeshProUGUI positionText;

    private List<CarRaceData> allCars = new();
    private bool raceStarted = false;
    private bool raceEnded   = false;

    private void Start()
    {
        // 1. Mezclar coches y posiciones
        List<GameObject> cars = new() { playerCar };
        cars.AddRange(npcCars);

        List<Transform> shuffled = spawnPoints.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < cars.Count; i++)
        {
            cars[i].transform.position = shuffled[i].position;
            cars[i].transform.rotation = Quaternion.Euler(0f, -90f, 0f); // orientación fija
        }

        // 2. Colocar cámara sobre el jugador (antes de la cuenta atrás)
        if (mainCamera != null && playerCar != null)
        {
            mainCamera.transform.position = playerCar.transform.position + cameraOffset;
            mainCamera.transform.LookAt(playerCar.transform.position);
        }

        // 3. Desactivar movimiento
        playerCar.GetComponent<CarController>().enabled = false;
        foreach (var npc in npcCars)
            npc.GetComponent<AIWaypointNavigator>().enabled = false;

        // 4. Cachear CarRaceData
        foreach (var obj in cars)
            allCars.Add(obj.GetComponent<CarRaceData>());

        StartCoroutine(WaitForKeyToStart());
    }

    private IEnumerator WaitForKeyToStart()
    {
        pressKeyPanel.SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(startKey));
        pressKeyPanel.SetActive(false);

        yield return StartCoroutine(StartCountdown());
        StartRace();
    }

    private IEnumerator StartCountdown()
    {
        countdownText.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        countdownText.text = "¡GO!";
        yield return new WaitForSeconds(1f);
        countdownText.gameObject.SetActive(false);
    }

    private void StartRace()
    {
        playerCar.GetComponent<CarController>().enabled = true;
        foreach (var npc in npcCars)
            npc.GetComponent<AIWaypointNavigator>().enabled = true;
        raceStarted = true;
    }

    private void Update()
    {
        if (!raceStarted || raceEnded) return;

        UpdatePlayerPosition();
        UpdateCamera();
        CheckIfPlayerFinished();
    }

    private void UpdatePlayerPosition()
    {
        var sorted = allCars
            .OrderByDescending(c => c.currentLap)
            .ThenByDescending(c => c.currentCheckpointIndex)
            .ThenBy(c => c.distanceToNextCheckpoint)
            .ToList();

        int pos = sorted.FindIndex(c => c.isPlayer) + 1;
        positionText.text = $"{pos}º";
    }

    private void UpdateCamera()
    {
        if (mainCamera != null && playerCar != null)
        {
            mainCamera.transform.position = playerCar.transform.position + cameraOffset;
            mainCamera.transform.LookAt(playerCar.transform.position + Vector3.forward * 5f);
        }
    }

    private void CheckIfPlayerFinished()
    {
        CarRaceData player = allCars.Find(c => c.isPlayer);
        if (player.currentLap >= 2 && player.finished)
        {
            raceEnded = true;
            int finalPos = GetPlayerFinalPosition();
            SaveResult(finalPos);
            SceneManager.LoadScene("Menu");
        }
    }

    private int GetPlayerFinalPosition()
    {
        var sorted = allCars
            .OrderByDescending(c => c.currentLap)
            .ThenByDescending(c => c.currentCheckpointIndex)
            .ThenBy(c => c.distanceToNextCheckpoint)
            .ToList();

        return sorted.FindIndex(c => c.isPlayer) + 1;
    }

    private void SaveResult(int pos)
    {
        string key = $"Position_{pos}";
        int prev   = PlayerPrefs.GetInt(key, 0);
        PlayerPrefs.SetInt(key, prev + 1);
        PlayerPrefs.Save();
    }
}
