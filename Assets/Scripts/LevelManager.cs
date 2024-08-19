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
        "LevelA6"
    };

    public static string[] hints = {
        "Right click to shrink objects, left click to enlarge them.", 
        "Shrinking objects can make them fit into smaller gaps.",
        "You can take mass from one object to give it to another.",
        "Enlarging objects can change their center of mass.",
        "Enlarging objects pushes them away from walls.",
        "Take all the mass you can get, leave nothing behind!"
    };

    public static bool[] completed = new bool[6]; // defaults to false
}
