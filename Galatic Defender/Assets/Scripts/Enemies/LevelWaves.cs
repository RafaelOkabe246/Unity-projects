using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Waves", menuName = "Enemy/Level waves")]
public class LevelWaves : ScriptableObject
{
    public List<EnemyWave> enemyWaves;
}
