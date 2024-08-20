using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public static LevelManager instance;

	public static string[] levels = {
		"LevelA1",
		"LevelA2",
		"LevelA3",
		"LevelA4",
		"LevelA5",
		"LevelA6",
		"LevelB1",
		"LevelB2",
		"LevelB3",
		"LevelB4",
		"LevelB5",
		"LevelB6",
	};

	public static string[] hints = {
		"Stand on objects while enlarging them to gain height.", 
		"Shrinking objects can make them fit into smaller gaps.",
		"You can take mass from one object to give to another.",
		"Enlarging objects can change their center of mass.",
		"Enlarging objects pushes them away from walls.",
		"Take all your mass with you, leave nothing behind.",
		"You can use other objects to hold buttons in place.",
		"Enlarging an object can make it easier to reach.",
		"You can bounce your gun's beam off of mirrors.",
		"Shoot yourself using the mirror and see what happens.",
		"Larger buttons require heavier objects to press.",
		"Don't be afraid to point lasers at yourself.",
	};

	public static bool[] completed = new bool[levels.Length]; // defaults to false
}
