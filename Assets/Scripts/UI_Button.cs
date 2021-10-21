using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Button : MonoBehaviour
{
    public string id;

    //could probably do with a refactor but for functionality purposes
    [SerializeField]
    GameObject HelpMenu;
    [SerializeField]
    GameObject HelpScene;
    [SerializeField]
    GameObject MainMenu;

    public void Execute()
    {
        switch (id.ToUpper().Trim())
        {
            case "PLAYGAME":
                SceneManager.LoadScene("VRDemo");
                break;
            case "HELP":
                MainMenu.SetActive(false);
                HelpMenu.SetActive(true);
                HelpScene.SetActive(true);
                break;
            case "TOGGLESOUND":
                GameController.Instance.ToggleSound();
                break;
            case "BACKTOMENU":
                MainMenu.SetActive(true);
                HelpMenu.SetActive(false);
                HelpScene.SetActive(false);
                break;
        }
    }
}
