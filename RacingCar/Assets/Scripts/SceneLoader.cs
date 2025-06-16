using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("complete_track_demo");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
