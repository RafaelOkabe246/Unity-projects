using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordTimer : MonoBehaviour
{
    public bool canSpawnWords;

    public WordManager wordManager;

    public int originalWaveNumber;
    public int enemiesWave;
    public int enemiesSpawned;

    public float spawnRate = 1.5f;
    private float nextWordTime = 0f;

    private void Update()
    {
        if (enemiesSpawned < enemiesWave)
            SpawnWave();
    }


    void SpawnWave()
    {
        if (canSpawnWords)
        {
            for (int i = enemiesWave; 0 < i; i--)
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

