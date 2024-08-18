using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void ViewControls()
    {
        SceneManager.LoadScene("ControlsScreen");
    }

    public void RollCredits()
    {
        SceneManager.LoadScene("CreditsScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
