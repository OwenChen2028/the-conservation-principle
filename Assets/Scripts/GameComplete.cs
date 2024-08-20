using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameComplete : MonoBehaviour
{
    private void Awake()
    {
        for (int i = 0; i < LevelManager.completed.Length; i++)
        {
            if (LevelManager.completed[i])
            {
                return;
            }
        }
        GetComponent<TMP_Text>().text = "Congratulations on completing the game!";
    }
}
