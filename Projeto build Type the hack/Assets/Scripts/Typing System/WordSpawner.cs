using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Creates the words' objects
*/
public class WordSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public Enemy wordPrefab;
    [Header("Special enemies")]
    public List<Enemy> specialEnemies;

    public Enemy bossEnemy;

    public float chanceSpecialEnemy;

    public Player playerRef;
    public WordManager wordManager;

    public bool isBossFight;

    public WordDisplay SpawnWord()
    {
        //Vector3 randomPosition = new Vector3(Random.Range(spawnPointA.position.x, spawnPointB.position.x), Random.Range(spawnPointA.position.y, spawnPointB.position.y));
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Vector3 randomPosition = new Vector3(Random.Range(spawnPoints[0].position.x, spawnPoints[1].position.x),
            Random.Range(spawnPoints[0].position.y, spawnPoints[1].position.y), 0f);

        Enemy wordObj;

        if (isBossFight)
        {
            wordObj = Instantiate(selectBoss(), randomPosition, Quaternion.identity);
        }
        else {
            wordObj = Instantiate(randomEnemy(), randomPosition, Quaternion.identity);
        }
        wordObj.player = playerRef;
        wordObj.wordManager = wordManager;
        
        WordDisplay wordDisplay = wordObj.transform.GetChild(0).GetComponent<WordDisplay>();
        wordDisplay.parentEnemy = wordObj;

        return wordDisplay;
    }

    int spawnChance;
    Enemy randomEnemy()
    {
        int specialEnemySpawn = Random.Range(0, 101);

        if (specialEnemySpawn <= chanceSpecialEnemy)
        {
            //Spawna inimigo especial
            spawnChance = Random.Range(0, specialEnemies.Count);
        }
        else
        {
            return wordPrefab;
        }

        return specialEnemies[spawnChance];
    }

    Enemy selectBoss() {
        return bossEnemy;
    }
    
}
