using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitLevelData {
    public int priceLevelUp;
    public int forceAttack;
    public float lightRange;
    public float speedAttack;
    public int lives;
}

[System.Serializable]
public class UnitContext
{
    public Sprite image;
    public string typeName;
    public UnitLevelData[] levelsDescription;
}

[CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObjects/UnitsScriptableObject", order = 1)]
public class UnitScriptableObject : ScriptableObject
{
    public UnitContext[] units;
}

