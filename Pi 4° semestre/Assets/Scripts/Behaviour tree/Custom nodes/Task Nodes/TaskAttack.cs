using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class TaskAttack : NodeBehaviourtree
{
    private CharacterActions characterActions;
    private EnemyBT enemyBT;

    public TaskAttack(EnemyBT _enemyBT, CharacterActions _characterActions)
    {
        enemyBT = _enemyBT;
        characterActions = _characterActions;
    }

    public override NodeState Evaluate()
    {
        characterActions.OnTriggerAttack();

        state = NodeState.RUNNING;
        return state;
    }
}
