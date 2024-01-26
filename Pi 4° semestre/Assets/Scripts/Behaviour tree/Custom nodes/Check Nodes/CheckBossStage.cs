using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckBossStage : NodeBehaviourtree
{
    private EnemyStage enemyStage; 
    private EnemyBT enemyBT;

    public CheckBossStage(EnemyBT _enemyBT, EnemyStage _enemyStage)
    {
        enemyBT = _enemyBT;
        enemyStage = _enemyStage;
    }

    public override NodeState Evaluate()
    {
        if (enemyBT.currentEnemyStage >= enemyStage)
        {
            state = NodeState.SUCCESS;
        }
        else
        {
            state = NodeState.FAILURE;
        }


        return state;
    }
}
