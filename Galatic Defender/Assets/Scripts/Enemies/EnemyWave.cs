using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Wave", menuName = "Enemy/Enemy wave")]
public class EnemyWave : ScriptableObject
{
    public List<Enemy> enemies;

}
