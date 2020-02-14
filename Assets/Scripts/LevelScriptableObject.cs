using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Level {
    public int level;
    public string name;
    public int unitsToHorde;
    public string sceneName;
    public Vector2[] civilPositions;
}

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelScriptableObject", order = 1)]
public class LevelScriptableObject : ScriptableObject
{
    public Level[] levels;
}
