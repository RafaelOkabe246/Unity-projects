using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

/// <summary>
/// Controls the active enemies in the current level.
/// </summary>
public class EnemiesManager : MonoBehaviour
{
    public SpawnerController spawnerController;
    public List<Enemy> enemiesInLevel = new List<Enemy>();
    public GameEvent allEnemiesDead;

    /// <summary>
    /// Atalho dev
    /// </summary>
    public void DestroAllEnemies()
    {
        if (enemiesInLevel.Count <= 0)
            return;

        foreach (Enemy enemy in enemiesInLevel)
        {
            enemy.DEV_Destroy();
        }
    }

    public void CheckEnemiesAllDead()
    {
        if (enemiesInLevel.All(n => n.isDead)) 
        {
            allEnemiesDead.Raise(this, null);
        }
    }

}
