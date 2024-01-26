using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "BattleWave/EnemyWave")]

public class BattleWave : ScriptableObject
{
    [SerializeField] private GameObject[] enemiesList; //list of enemies from the wave

    public GameObject[] waveEnemies { get => enemiesList; private set => enemiesList = value;}
}
