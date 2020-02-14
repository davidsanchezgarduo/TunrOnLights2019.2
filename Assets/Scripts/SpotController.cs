using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotController : MonoBehaviour
{
    public float timeSpawn;
    public float maxEnemimesPerHorde;
    public bool isActive;
    private float currentTimeSpawn;
    private int currentEnemiesSpawn;
    public bool finishSpawn;
    private int currenMaxEnemies;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        //StartHorde();
        isPaused = false;
        maxEnemimesPerHorde = 5;
        GameManager.instance.AddSpot(this);
    }

    // Update is called once per frame
    public bool UpdateSpot()
    {
        
        if (isActive)
        {
            if (!finishSpawn)
            {
                //Debug.Log("time Spawn "+currentTimeSpawn);
                currentTimeSpawn += Time.deltaTime;
                if (currentTimeSpawn > timeSpawn)
                {
                    EnemyGenerator.instance.CreateEnemy(GetEnemyId(), transform.position);
                    currentEnemiesSpawn++;
                    currentTimeSpawn = 0;
                    if (currentEnemiesSpawn >= currenMaxEnemies)
                    {
                        finishSpawn = true;
                    }
                }
            }

            return finishSpawn;
        }
        else {
            return true;
        }

    }

    private int GetEnemyId() {
        int r = Random.Range(0,10);
        if (r < 7)
        {
            r = 0;
        }
        else {
            r = 1;
        }
        return r;
    }

    public void StartHorde(float plusDifficult) {
        maxEnemimesPerHorde += plusDifficult;
        currenMaxEnemies = Mathf.RoundToInt(maxEnemimesPerHorde);
        currentTimeSpawn = 0;
        currentEnemiesSpawn = 0;
        finishSpawn = false;
    }

}
