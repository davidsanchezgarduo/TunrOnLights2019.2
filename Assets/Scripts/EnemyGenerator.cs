using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public static EnemyGenerator instance;
    public GameObject[] enemyPrefabs;
    private List<EnemyController> enemies;
    private bool gameIsPause;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameIsPause = false;
        enemies = new List<EnemyController>();
    }

    /*// Update is called once per frame
    void Update()
    {
        
    }*/

    public void CreateEnemy(int type, Vector3 pos) {
        GameObject enemy = Instantiate(enemyPrefabs[type], pos, Quaternion.identity);
        enemies.Add(enemy.GetComponent<EnemyController>());
        
    }

    public void RemoveEnemy(EnemyController e, bool killed) {
        enemies.Remove(e);
        if (killed)
        {
            GameManager.instance.zombiesKilled++;
        }
        //Debug.Log("Restant zombies "+ enemies.Count+" "+GameManager.instance.hasFinishSpawn);
        if (GameManager.instance.hasFinishSpawn && enemies.Count == 0) {
            GameManager.instance.FinishHorde();
        }
    }

    public void ActivateEnd() {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].ActivateEnd();
        }
    }

    public EnemyController CheckEnemyInRange(Vector3 pos, float range) {
        for (int i = 0; i < enemies.Count; i++)
        {
            float dis = Vector3.Distance(enemies[i].transform.position, pos);
            if (dis < range)
            {
                return enemies[i];
            }

        }
        return null;
    }

    public void PausedGame(bool p)
    {
        gameIsPause = true;
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].Paused(p);
        }
    }
}
