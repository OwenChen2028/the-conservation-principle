using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private string level;

    public void LoadPuzzle(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => LoadPuzzle(level));

        for (int i = 0; i < LevelManager.levels.Length; i++)
        {
            if (LevelManager.levels[i] == level && LevelManager.completed[i])
            {
                GetComponent<Image>().color = new Color(186f / 255, 255f / 255, 157f / 255);
            }
        }
    }
}
