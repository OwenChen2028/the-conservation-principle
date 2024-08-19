using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool paused;
    [SerializeField] private GameObject menu;

    public GameObject hintsText;
    public GameObject hintsButton;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
            if (!paused)
            {
                PauseGame();
            }
            else
            {
                UnpauseGame();
            }
        }
    }

    public void PauseGame()
    {
        paused = true;
        Time.timeScale = 0;
        menu.SetActive(true);
    }

    public void UnpauseGame()
    {
        paused = false;
        Time.timeScale = 1;
        menu.SetActive(false);
    }
    public void RestartGame()
    {
        UnpauseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void RevealHints()
    {
        for (int i = 0; i < LevelManager.levels.Length; i++)
        {
            if (LevelManager.levels[i] == SceneManager.GetActiveScene().name)
            {
                hintsText.GetComponent<TMP_Text>().text = "Hint: " + LevelManager.hints[i];
            }
        }
        hintsText.SetActive(true);
        hintsButton.SetActive(false);
    }
}
