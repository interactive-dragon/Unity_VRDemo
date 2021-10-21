using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ScoreListener : MonoBehaviour
{
    TMPro.TextMeshProUGUI value;

    private void Awake()
    {
        GameController.Instance.ScoreChanged += ScoreChanged;
        value = GetComponent<TMPro.TextMeshProUGUI>();
    }

    public void ScoreChanged(int newScore)
    {
        value.text = newScore.ToString();
    }

    private void OnDestroy()
    {
        GameController.Instance.ScoreChanged -= ScoreChanged;
    }
}
