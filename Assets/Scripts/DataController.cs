using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData{
    public int points;
    public int zombieskilled;
    public int unitsDeployed;
    public int lastlevel;
}

[System.Serializable]
public class Unit {
    public string typeName;
    public int level;
}

[System.Serializable]
public class UnitsData {
    public Unit[] units; 
}

public class DataController : MonoBehaviour
{
    public static DataController instance;
    public string playerDataPath = "player.json";
    public PlayerData playerData;

    public string unitsDataPath = "units.json";
    public UnitsData unitsData;

    public string LastLevelSelected;


    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
        if (File.Exists(Application.persistentDataPath + "/" + playerDataPath))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/" + playerDataPath);
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }
        else
        {
            playerData = new PlayerData();
            playerData.points = 0;
            playerData.zombieskilled = 0;
            playerData.unitsDeployed = 0;
            playerData.lastlevel = 0;
            string json = JsonUtility.ToJson(playerData);
            File.WriteAllText(Application.persistentDataPath + "/" + playerDataPath, json);
        }

        if (File.Exists(Application.persistentDataPath + "/" + unitsDataPath))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/" + unitsDataPath);
            unitsData = JsonUtility.FromJson<UnitsData>(json);
        }
        else
        {
            unitsData = new UnitsData();
            unitsData.units = new Unit[2];
            unitsData.units[0] = new Unit();
            unitsData.units[0].typeName = "Pistolero";
            unitsData.units[0].level = 0;
            unitsData.units[1] = new Unit();
            unitsData.units[1].typeName = "Francotirador";
            unitsData.units[1].level = 0;
            string json = JsonUtility.ToJson(unitsData);
            File.WriteAllText(Application.persistentDataPath + "/" + unitsDataPath, json);

        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("MenuScene");
        }
    }

    public void SaveData() {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/" + playerDataPath, json);
    }

    public void PlusData(int _points, int _zombies, int _units, bool passLevel) {
        playerData.points += _points;
        playerData.zombieskilled += _zombies;
        playerData.unitsDeployed += _units;
        if (passLevel) {
            playerData.lastlevel++;
        }
        SaveData();

    }

    public void SaveUnitsData() {
        string json = JsonUtility.ToJson(unitsData);
        File.WriteAllText(Application.persistentDataPath + "/" + unitsDataPath, json);
    }

    public void LevelUpUnit(int id) {
        unitsData.units[id].level++;
    }


}
