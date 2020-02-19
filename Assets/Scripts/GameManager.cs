using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int levelId;
    public LevelScriptableObject levelData;
    public int horde;
    public int unitsToNextHorde;
    public float unitsPerHorde = 2;
    public bool inHorde;
    public float plusSpawnDifficult = 0.5f;
    public int lives = 5; 

    private List<SpotController> spots;
    public bool hasFinishSpawn;
    
    public GameObject civilPrefab;
    public Transform civilParent;

    public int unitsDeployed;
    public int zombiesKilled;
    public int points;
    private int pointsPerZombie = 50;
    private int restantCivils;
    private int initialCivils;

    public AudioSource hordeSource;
    public bool gamePaused;


    private List<int> keysIds;

    private void Awake()
    {
        instance = this;
        spots = new List<SpotController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        keysIds = new List<int>();
        gamePaused = false;
        unitsDeployed = 0;
        zombiesKilled = 0;
        points = 0;
        horde = 1;
        inHorde = false;
        unitsToNextHorde = levelData.levels[levelId].unitsToHorde;
        unitsPerHorde = unitsToNextHorde;
        restantCivils = levelData.levels[levelId].civilPositions.Length;
        initialCivils = restantCivils;
        UIController.instance.TotalCivils = restantCivils;
        UIController.instance.SetLivesText(lives);
        UIController.instance.SetCivils(0);
        UIController.instance.SetUnits(unitsToNextHorde);

        for(var i =0; i < levelData.levels[levelId].civilPositions.Length; i++) {
            GameObject civil = Instantiate(civilPrefab, new Vector3(levelData.levels[levelId].civilPositions[i].x,civilPrefab.transform.position.y, levelData.levels[levelId].civilPositions[i].y),Quaternion.identity);
            civil.transform.parent = civilParent;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (gamePaused) {
            return;
        }
        if (inHorde) {
            if (!hasFinishSpawn)
            {
                bool tempFinish = true;
                for (int i = 0; i < spots.Count; i++)
                {
                    bool finish = spots[i].UpdateSpot();
                    //Debug.Log("Has finish spawn " + tempFinish);
                    if (!finish && tempFinish) {
                        tempFinish = false;
                    }
                }
                
                hasFinishSpawn = tempFinish;
            }
        }
    }

    public void AddSpot(SpotController s) {
        spots.Add(s);
    }

    public void SetUnit() {
        unitsDeployed++;
        unitsToNextHorde--;
        UIController.instance.SetUnits(unitsToNextHorde);
        //Debug.Log("SetUnit");
        if (unitsToNextHorde == 0) {
            //Debug.Log("StartHorde");

            if (hordeSource.GetComponent<AuidoControl>().enabled) {
                hordeSource.GetComponent<AuidoControl>().ForceChange();
            }

            hordeSource.Play();
            
            inHorde = true;
            UnitiesManager.instance.inHorde = true;
            hasFinishSpawn = false;
            int active = 0;
            for (int i = 0; i < spots.Count; i++)
            {
                int enemies = spots[i].StartHorde(plusSpawnDifficult);
                if (enemies != -1) {
                    active+=enemies;
                }
            }
            UIController.instance.SetHorde(horde,active);



        }
    }

    public void FinishHorde() {
        horde++;
        inHorde = false;
        unitsPerHorde += plusSpawnDifficult;
        unitsToNextHorde = Mathf.RoundToInt(unitsPerHorde);
        UnitiesManager.instance.inHorde = false;
        UIController.instance.FinishHorde();
        UIController.instance.SetUnits(unitsToNextHorde);
    }

    public void LoseLive() {
        lives--;
        UIController.instance.SetLivesText(lives);
        if(lives <= 0)
        {
            FinishLevel(false);
        }
    }

    public void FinishLevel(bool pass) {
        points = zombiesKilled * pointsPerZombie;
        DataController.instance.PlusData(points,zombiesKilled,unitsDeployed,pass);
        UIController.instance.FinishGame(pass);
    }

    public void RescueCivil() {
        restantCivils--;
        UIController.instance.SetCivils(initialCivils-restantCivils);
        if (restantCivils == 0) {
            FinishLevel(true);
        }
    }

    public void AddKey(int id) {
        keysIds.Add(id);
    }

    public bool CheckKey(int id) {
        return keysIds.Contains(id);
    }


}
