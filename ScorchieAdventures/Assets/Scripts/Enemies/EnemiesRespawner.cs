using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesRespawner : MonoBehaviour
{
    public Queue<Enemy> enemiesQueue;
    public float timeToRespawn;
    private float timeToRespawnCount;

    private void Awake()
    {
        enemiesQueue = new Queue<Enemy>();
    }

    private void OnEnable()
    {
        StageBlocksHandler.OnStageBlockTransitionStarted += OnStageBlockTransitionStarted;
    }

    private void OnDisable()
    {
        StageBlocksHandler.OnStageBlockTransitionStarted -= OnStageBlockTransitionStarted;
    }

    private void Update()
    {
        timeToRespawnCount += Time.deltaTime;
        if (timeToRespawnCount >= timeToRespawn) 
        {
            timeToRespawnCount = 0;

            if (enemiesQueue.TryDequeue(out Enemy _enemy))
            {
                if (_enemy.TryGetComponent(out EnemyPatrol enemyPatrol))
                    return;
                _enemy.gameObject.SetActive(true);
                //Debug.Log("Try respawn");
                //_enemy.Respawn();
            }
        }
    }

    public void RestartCounter() 
    {
        timeToRespawnCount = 0;
    }

    public void ClearEnemyQueue() 
    {
        RestartCounter();
        enemiesQueue.Clear();
        enemiesQueue = new Queue<Enemy>();
    }

    private void OnStageBlockTransitionStarted() 
    {
        ClearEnemyQueue();
    }
}
