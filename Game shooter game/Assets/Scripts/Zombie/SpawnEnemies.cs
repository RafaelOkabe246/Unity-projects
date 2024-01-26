using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    /*
    public static int EnemiesAlive = 0;

    public Wave[] waves;

    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    public float countdown = 2f;

    //public float timeBetweenEnemies = 0.5f;

    //public Text waveCountdownText;
    //public Text wavesText;

    private int waveIndex = 0;
    public int[] EnemiesWaves;

    private void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        //waveCountdownText.text = string.Format("{0:00.00}", countdown);

        //wavesText.text = (waveIndex + 1) + "/" + waves.Length;
    }

    IEnumerator SpawnWave()
    {
        //Debug.Log("Wave Incoming");
        Wave wave = waves[waveIndex];
        //EnemyBlueprint eb = enemies[enemyIndex];

        for (int a = 0; a < wave.waveCount; a++)
        {
            yield return new WaitForSeconds(1f / wave.waveRate);

            for (int i = 0; i < enemies.enemyCount; i++)
            {
                SpawnEnemy(enemies.enemy);
                yield return new WaitForSeconds(1f / enemies.enemyRate);
            }
        }

        waveIndex++;
        //PlayerStats.Rounds++;

        if (waveIndex == waves.Length)
        {
            Debug.Log("LEVEL WON!");
            this.enabled = false;
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
    */
}
