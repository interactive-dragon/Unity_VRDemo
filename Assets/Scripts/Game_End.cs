using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_End : MonoBehaviour
{
    private AudioSource m_GameOverSnd;
    private Animator m_Animator;
    private void Awake()
    {
        m_GameOverSnd = GetComponent<AudioSource>();
        m_Animator = GetComponent<Animator>();
        GameController.Instance.OnGameEnd += OnGameOver;
    }

    private void OnGameOver()
    {
        m_GameOverSnd.Play();
        m_Animator.SetBool("bGameOver", true);

    }

    public void GameOverFinished()
    {
        StartCoroutine("FinishGame");
    }

    IEnumerator FinishGame()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("MainMenu");
        yield return null;
    }

    private void OnDestroy()
    {
        GameController.Instance.OnGameEnd -= OnGameOver;
    }
}
