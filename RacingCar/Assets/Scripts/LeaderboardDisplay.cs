using UnityEngine;
using TMPro;

public class LeaderboardDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] countTexts; 

    private void OnEnable()
    {
        UpdateLeaderboard();
    }

    public void UpdateLeaderboard()
    {
        for (int i = 0; i < 8; i++) 
        {
            int pos = i + 1;
            int count = PlayerPrefs.GetInt($"Position_{pos}", 0);
            countTexts[i].text = $"x{count}";
        }
    }
}
