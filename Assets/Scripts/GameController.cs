using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GState
{
    Menu,
    Gameplay,
    GameEnd
}

public class GameController : MonoBehaviour
{
    private static bool m_FirstInit = false;

    public static GameController Instance { get; protected set; }

    public bool SoundEnabled { get; protected set; }
    public Action<bool> SoundEnabledChanged;

    private int m_CurrentScore;
    public Action<int> ScoreChanged;

    public float m_TimeRemaining { get; protected set; }
    public Action<float> TimeChanged;

    private bool m_GameRunning;
    public Action OnGameStarted;
    public Action OnGameEnd;

    public GState State { get; protected set; }
    public Leaderboard m_Leaderboard { get; protected set; }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;

        if(!m_FirstInit)
        {
            SetSound(true);
            SceneManager.LoadScene("MainMenu");
            m_FirstInit = true;
        }

        if (m_Leaderboard == null)
            m_Leaderboard = new Leaderboard(Application.persistentDataPath + "/leaderboard.json");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }
    private void SceneManager_sceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        m_CurrentScore = 0;
        if(ScoreChanged != null)
            ScoreChanged(m_CurrentScore);
        m_TimeRemaining = 60.0f; //60 seconds per game
        if (TimeChanged != null)
            TimeChanged(m_TimeRemaining);

        m_GameRunning = false;

        switch (scene.name.ToUpper())
        {
            case "MAINMENU":
                State = GState.Menu;
                break;
            case "VRDEMO":
                State = GState.Gameplay;
                break;
        }
    }

    public void ToggleSound()
    {
        SoundEnabled = !SoundEnabled;
        if (SoundEnabledChanged != null)
            SoundEnabledChanged(SoundEnabled);
        AudioListener.volume = SoundEnabled ? 1 : 0;
    }

    private void SetSound(bool bSound)
    {
        SoundEnabled = bSound;
        if(SoundEnabledChanged != null)
            SoundEnabledChanged(SoundEnabled);
        AudioListener.volume = SoundEnabled ? 1 : 0;
    }

    private void Update()
    {
        switch (State)
        {
            case GState.Gameplay:
                DoGameplay();
                break;
        }
    }

    private void DoGameplay()
    {
        if (m_GameRunning)
            m_TimeRemaining -= Time.deltaTime;

        if (m_TimeRemaining <= 0)
        {
            m_TimeRemaining = 0;
            m_GameRunning = false;
            if (OnGameEnd != null)
                OnGameEnd();

            m_Leaderboard.RegisterScore(m_CurrentScore);
            //SceneManager.LoadScene("MainMenu");
            State = GState.GameEnd;
        }
        if (TimeChanged != null)
            TimeChanged(m_TimeRemaining);
    }

    public void GameStarted()
    {
        m_GameRunning = true;
        if(OnGameStarted != null)
            OnGameStarted();
    }

    public void AddScore(int score)
    {
        if (!m_GameRunning)
            return;

        m_CurrentScore += score;

        if (m_CurrentScore < 0)
            m_CurrentScore = 0;

        if (ScoreChanged != null)
            ScoreChanged(m_CurrentScore);
    }
}
