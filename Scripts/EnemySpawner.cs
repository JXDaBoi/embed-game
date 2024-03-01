using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] Enemy;
    public GameObject[] EnemySpawnpoints;
    public int MaximumEnemyCount;
    public float spawnTimer;
    public bool isBossFight;

    private float spawnTimerCounter;

    void Start()
    {
        EnemySpawnpoints = GameObject.FindGameObjectsWithTag("enemySpawnpoint");

        spawnTimerCounter = spawnTimer;
    }

    void Update()
    {
        spawnTimerCounter -= Time.deltaTime;

        if (spawnTimerCounter <= 0 && !isBossFight)
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length < 100)
            {
                SpawnEnemy();
            }

            spawnTimer = 20 * (1 - ((PlayerStats.instance.elapsedTime) / 1200));
            if (spawnTimer <= 10)
            {
                spawnTimer = 10;
            }

            spawnTimerCounter = spawnTimer;
        }
    }

    public void SpawnEnemy()
    {
        foreach (var enemySpawnpoint in EnemySpawnpoints)
        {
            int enemyCount = Random.Range(1, MaximumEnemyCount + 1);

            if (Vector3.Distance(enemySpawnpoint.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 15f)
            {
                for (int i = 0; i < enemyCount; i++)
                {
                    int enemyType = Random.Range(0, Enemy.Length);

                    Instantiate(Enemy[enemyType], enemySpawnpoint.transform.position, Quaternion.identity);
                }
            }
        }
    }
}