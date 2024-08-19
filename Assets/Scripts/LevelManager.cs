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
    };

    public static string[] hints = {
        "Stand on objects while enlarging them to gain height.", 
        "Shrinking objects can make them fit into smaller gaps.",
        "You can take mass from one object to give it to another.",
        "Enlarging objects can change their center of mass.",
        "Enlarging objects pushes them away from walls.",
        "Take all your mass with you, leave nothing behind.",
        "You can use other objects to hold buttons in place.",
        "Enlarging an object can make it easier to reach."
    };

    public static bool[] completed = new bool[levels.Length]; // defaults to false
}
