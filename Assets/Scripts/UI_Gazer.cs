using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Gazer : MonoBehaviour
{
    Image m_Image;
    private void Awake()
    {
        m_Image = GetComponent<Image>();
        m_Image.fillAmount = 0.0f;
    }

    public void NotifyGazeTime(float timeGazed, float maxTime)
    {
        m_Image.fillAmount = timeGazed / maxTime;
    }
}
