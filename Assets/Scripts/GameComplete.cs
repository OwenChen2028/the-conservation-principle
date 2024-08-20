using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameComplete : MonoBehaviour
{
	[SerializeField] private GameObject[] ConfettiLaunchers;
	private void Awake()
	{
		for (int i = 0; i < LevelManager.completed.Length; i++)
		{
			if (!LevelManager.completed[i])
			{
				return;
			}
		}
		
		for (int i = 0; i < ConfettiLaunchers.Length; i++)
		{
			ConfettiLaunchers[i].SetActive(true);
		}
		
		GetComponent<TMP_Text>().text = "Congratulations on completing the game!";
	}
}
