using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordTimer : MonoBehaviour
{
    public bool canSpawnWords;

    public WordManager wordManager;

    public int lastEnemiesCount;
    public int enemiesCountInWave = 3;
    public int enemiesSpawned;

    public float spawnRate = 1.5f;
    private float nextWordTime = 0f;

    private void Update()
    {
        if (enemiesSpawned < enemiesCountInWave)
            SpawnWave();
    }


    void SpawnWave()
    {
        if (canSpawnWords)
        {
            for (int i = enemiesCountInWave; i > 0; i--)
            {
                SpawnEnemy();
            }
        }

        wordManager.isWaveFinish = false;
    }

    void SpawnEnemy()
    {
        if (Time.time >= nextWordTime)
        {
            if (canSpawnWords)
            {
                wordManager.AddWord();
                nextWordTime = Time.time + spawnRate;
                spawnRate *= .999f;
                enemiesSpawned += 1;
            }
        }
    }
}

