using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckAttackArea : NodeBehaviourtree
{
    private CharacterActions characterActions;
    private Grid gridLevel;
    private EnemyBT enemyBT;

    public CheckAttackArea(EnemyBT _enemyBT, CharacterActions _characterActions, Grid _gridLevel)
    {
        enemyBT = _enemyBT;
        characterActions = _characterActions;
        gridLevel = _gridLevel;
    }

    public override NodeState Evaluate()
    {
        if (characterActions.OnCheckIsMoving())
        {
            state = NodeState.FAILURE;
            return state;

        }


        //Check if the player is in the attack range
        if (!gridLevel.battleTiles.TryGetValue(characterActions.AttackLocation(), out BattleTile _battleTile))
        {
            state = NodeState.FAILURE;
        }
        else if (!_battleTile.GetOccupyingObject()) 
        {
            state = NodeState.FAILURE;
        }
        else if (_battleTile.GetOccupyingObject().CompareTag("Player"))
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
