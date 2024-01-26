using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private GameSystem GS;
    [SerializeField] private float spawnWaveRate;

    public static int EnemiesInCurrentLevel;
    public int _EnemiesInCurrentLevel;

    public Transform point1, point2;

    private void Update()
    {
        _EnemiesInCurrentLevel = EnemiesInCurrentLevel;
    }

    public void StartWaves()
    {
        SelectLevel();
    }

    #region Spawn_enemies
    public void SelectLevel()
    {
        //Play this level's waves
        StartCoroutine(SpawnEnemyWave(GS.levelWaves[GS.CurrentLevel]));
        
    }

    IEnumerator SpawnEnemyWave(LevelWaves levelWave)
    {
        CountWaveEnemies(levelWave);
        foreach (EnemyWave enemyWave in levelWave.enemyWaves)
        {
            //Spawn the enemies in the wave
            foreach (Enemy enemy in enemyWave.enemies)
            {
                //Spawn the enemies in the wave
                SpawnEnemy(enemy);
            }
            yield return new WaitForSeconds(spawnWaveRate);
        }
    }

    public void SpawnEnemy(Enemy enemy)
    {
        //Enemy spawn
        switch (enemy.enemyType)
        {
            case (EnemyType.Projectile):
                Enemy enemySpawned = enemy;
                enemySpawned.transform.position = new Vector2(Random.Range(point1.position.x, point2.position.x), transform.position.y);
                //Set missle target
                enemySpawned.GetComponent<EnemyMissle>().target = SelectEnemyTarget();
                Instantiate(enemySpawned);
                break;

            case (EnemyType.Spaceship):
                break;
                
        }
    }
    #endregion

    public void DecreaseEnemyCount()
    {
        EnemiesInCurrentLevel -= 1;
    }

    public void CountWaveEnemies(LevelWaves levelWave)
    {
        for(int waveIndex = 0; waveIndex < levelWave.enemyWaves.Count; waveIndex++)
        {
            for (int enemyIndex = 0; enemyIndex < levelWave.enemyWaves[waveIndex].enemies.Count; enemyIndex++)
            {
                EnemiesInCurrentLevel += 1;
            }
        }
    }

    public Transform SelectEnemyTarget()
    {
        int BaseOrCity = Random.Range(0, 2);
        int buildIndex;
        Transform enemyTarget = null; 
        
        if (BaseOrCity == 0)
        {
            //Base target
            buildIndex = Random.Range(0, GS.buildingsManager.Bases.Length);
            enemyTarget = GS.buildingsManager.Bases[buildIndex].transform;
        }
        else if (BaseOrCity == 1)
        {
            //City target
            buildIndex = Random.Range(0, GS.buildingsManager.Cidades.Length);
            enemyTarget = GS.buildingsManager.Cidades[buildIndex].transform;
        }

        return enemyTarget;
    }
    
}
