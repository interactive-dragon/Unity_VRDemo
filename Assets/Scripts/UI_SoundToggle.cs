using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SoundToggle : MonoBehaviour
{
    [SerializeField]
    Sprite[] toggleStates;
    private Image Image;
    private bool bSoundEnabled;


    private void Awake()
    {
        Image = GetComponent<Image>();
        SoundStateChanged(GameController.Instance.SoundEnabled);
        GameController.Instance.SoundEnabledChanged += SoundStateChanged;
    }

    private void SoundStateChanged(bool newState)
    {
        bSoundEnabled = newState;
        Image.sprite = toggleStates[Convert.ToInt32(bSoundEnabled)];
    }

    private void OnDestroy()
    {
        GameController.Instance.SoundEnabledChanged -= SoundStateChanged;
    }
}
