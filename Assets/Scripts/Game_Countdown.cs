using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Countdown : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Animator>().SetBool("bStartGame", true);
    }
    public void GameStarted()
    {
        GameController.Instance.GameStarted();
    }
}
