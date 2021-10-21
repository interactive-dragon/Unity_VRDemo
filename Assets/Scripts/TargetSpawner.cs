using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    float m_TimeBetweenSpawns = 2.0f; //Seconds
    float m_Elapsed = 0.0f; //seconds
    private bool m_GameStarted = false;

    [SerializeField]
    GameObject mTargetPrefab;

    private BoxCollider m_PlayArea;
    private AudioSource m_ErrorSound;
    // Start is called before the first frame update
    void Awake()
    {
        m_PlayArea = GetComponent<BoxCollider>();
        m_ErrorSound = GetComponent<AudioSource>();
        GameController.Instance.OnGameStarted += GameStarted;
        GameController.Instance.OnGameEnd += GameEnded;
    }

    private void OnDestroy()
    {
        GameController.Instance.OnGameStarted -= GameStarted;
        GameController.Instance.OnGameEnd -= GameEnded;
    }
    private void GameStarted()
    {
        m_GameStarted = true;
        m_Elapsed = m_TimeBetweenSpawns; //instant spawn
    }

    private void GameEnded()
    {
        m_GameStarted = false;
    }
    private void FixedUpdate()
    {
        if (!m_GameStarted)
            return;

        m_Elapsed += Time.deltaTime;
        if (m_Elapsed >= m_TimeBetweenSpawns)
            SpawnTarget();
    }

    private void SpawnTarget()
    {
        Vector3 spawnPos = RandomFloorPointInBounds(m_PlayArea.bounds);
        GameObject target = GameObject.Instantiate(mTargetPrefab, spawnPos, Quaternion.identity, null);
        target.GetComponent<Target>().Init(RandomTarget());
        m_Elapsed = 0;
    }

    private TargetProperties RandomTarget()
    {
        TargetProperties oProps = new TargetProperties(0.5f,2, Color.red);
        switch(Random.Range(1, 6))
        {
            case 1:
                return new TargetProperties(.5f, 2, Color.red);
            case 2:
                return new TargetProperties(1.5f, 10, Color.cyan);
            case 3:
                return new TargetProperties(2, 30, Color.blue);
            case 4:
                return new TargetProperties(2.5f, 50, Color.green);
            case 5:
                return new TargetProperties(4.5f, 100, Color.yellow);
        }

        return oProps;
    }
    private Vector3 RandomFloorPointInBounds(Bounds bounds)
    {
        return new Vector3(Random.Range(bounds.min.x, bounds.max.x),0, Random.Range(bounds.min.z, bounds.max.z));
    }

    private void OnTriggerExit(Collider other)
    {
        if (!m_GameStarted)
            return;

        m_ErrorSound.Play();

        Target t = other.gameObject.GetComponent<Target>();
        if (t != null)
        {
            GameController.Instance.AddScore((t.m_Properties.Points / 2) * -1);
         }
        GameObject.Destroy(other.gameObject);
    }
}
