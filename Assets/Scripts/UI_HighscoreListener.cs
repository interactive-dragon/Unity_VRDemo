using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HighscoreListener : MonoBehaviour
{
    TMPro.TextMeshProUGUI value;

    private void Awake()
    {
        GameController.Instance.m_Leaderboard.HighscoreChanged += HighscoreChanged;
        value = GetComponent<TMPro.TextMeshProUGUI>();
        value.text = GameController.Instance.m_Leaderboard.Highscore.ToString();
    }

    public void HighscoreChanged(int newScore)
    {
        value.text = newScore.ToString();
    }

    private void OnDestroy()
    {
        GameController.Instance.m_Leaderboard.HighscoreChanged -= HighscoreChanged;
    }
}
