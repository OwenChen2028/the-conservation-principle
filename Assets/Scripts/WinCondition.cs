using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < LevelManager.levels.Length; i++)
            {
                if (LevelManager.levels[i] == SceneManager.GetActiveScene().name)
                {
                    LevelManager.completed[i] = true;
                }
            }
            SceneManager.LoadScene("LevelSelect");
        }
    }
}
