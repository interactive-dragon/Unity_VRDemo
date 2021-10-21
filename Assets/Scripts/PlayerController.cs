using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float m_Elapsed;
    public float m_TimeBetweenShots;
    private bool m_GameStarted = false;

    float m_Distance = 1000.0f;
    public GameObject m_shotPrefab;
    private AudioSource m_shotSound;


    private void Awake()
    {
        m_shotSound = GetComponent<AudioSource>();
        GameController.Instance.OnGameStarted += GameStarted;
        GameController.Instance.OnGameEnd += OnGameEnd;
    }

    private void OnDestroy()
    {
        GameController.Instance.OnGameEnd -= OnGameEnd;
    }
    private void GameStarted()
    {
        m_GameStarted = true;
    }

    private void OnGameEnd()
    {
        m_GameStarted = false;
    }
    void Update()
    {
        if (!m_GameStarted)
            return;

        m_Elapsed += Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            if (m_Elapsed > m_TimeBetweenShots)
            {
                ShootRay();
                m_Elapsed = 0;
            }
        }
    }

    private void ShootRay()
    {
        GameObject laser = GameObject.Instantiate(m_shotPrefab, Camera.main.transform.position, Camera.main.transform.rotation * m_shotPrefab.transform.rotation) as GameObject;
        laser.GetComponent<ShotBehavior>().setTarget(Camera.main.transform.forward * m_Distance); 
        m_shotSound.Play();
        GameObject.Destroy(laser, 2f);
            
    }
}
