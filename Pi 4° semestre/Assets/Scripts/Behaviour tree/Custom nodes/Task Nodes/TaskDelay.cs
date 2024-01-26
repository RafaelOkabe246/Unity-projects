using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class TaskDelay : NodeBehaviourtree
{
    private bool waiting;
    private float currentTime = 0f;
    private float waitTime;
    private EnemyBT enemyBT;
    public TaskDelay(EnemyBT _enemyBT, float _waitTime)
    {
        waitTime = _waitTime;
        enemyBT = _enemyBT;
    }

    public override NodeState Evaluate()
    {
        if (waiting)
        {
            currentTime += Time.deltaTime;
            if (currentTime > waitTime)
                waiting = false;
            state = NodeState.FAILURE;
        }
        else
        {
            currentTime = 0;
            waiting = true;
            state = NodeState.SUCCESS;
        }

        return state;
    }
}
