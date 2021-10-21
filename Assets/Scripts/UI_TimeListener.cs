using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_TimeListener : MonoBehaviour
{
    TMPro.TextMeshProUGUI value;

    private void Awake()
    {
        GameController.Instance.TimeChanged += TimeChanged;
        value = GetComponent<TMPro.TextMeshProUGUI>();
        value.text = GameController.Instance.m_TimeRemaining.ToString();
    }

    public void TimeChanged(float newTime)
    {
        value.text = Mathf.CeilToInt(newTime).ToString();
    }

    private void OnDestroy()
    {
        GameController.Instance.TimeChanged -= TimeChanged;
    }
}
