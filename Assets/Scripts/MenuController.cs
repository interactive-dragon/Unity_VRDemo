using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    private GameObject m_CurrentTarget = null;
    private float m_GazeTime = 0.0f;
    [SerializeField]
    private float m_TimeUntilExecute = 1.5f; //time to look at a button before executing behaviour
    [SerializeField]
    UI_Gazer m_Gazer;

    // Update is called once per frame
    void Update()
    {
        RaycastHit HitInfo;
        if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.TransformDirection(Vector3.forward),out HitInfo, Mathf.Infinity))
        {
            if (HitInfo.collider.gameObject.GetComponent<UI_Button>() == null)
                return; //not a valid target

            m_CurrentTarget = HitInfo.collider.gameObject;
            m_GazeTime += Time.deltaTime;
            m_Gazer.NotifyGazeTime(m_GazeTime, m_TimeUntilExecute);
            if(m_GazeTime >= m_TimeUntilExecute || Input.GetMouseButtonUp(0)) //stare or user taps
            {
                m_CurrentTarget.GetComponent<UI_Button>().Execute();
                ResetStare();
            }
        }
        else
        {
            ResetStare();
        }
    }

    private void ResetStare()
    {
        m_GazeTime = 0.0f;
        m_CurrentTarget = null;
        m_Gazer.NotifyGazeTime(m_GazeTime, m_TimeUntilExecute);
    }
}

