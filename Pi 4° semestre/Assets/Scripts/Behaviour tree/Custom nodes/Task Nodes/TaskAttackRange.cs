using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class TaskAttackRange : NodeBehaviourtree
{
    private CharacterActions characterActions;
    public TaskAttackRange(CharacterActions _characterActions)
    {
        characterActions = _characterActions;
    }

    public override NodeState Evaluate()
    {
        characterActions.OnTriggerRangeAttack(); 

        state = NodeState.RUNNING;
        return state;
    }
}
