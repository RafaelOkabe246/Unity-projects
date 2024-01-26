using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckCanEngage : NodeBehaviourtree
{
    private EnemyBT enemyBT;
    private Player player;
    private bool canEngage;

    public CheckCanEngage(EnemyBT _enemyBT, Player _player)
    {
        enemyBT = _enemyBT;
        player = _player;
    }

    public override NodeState Evaluate()
    {
        if (CanEngagePlayer())
        {
            enemyBT.currentBehaviourState = BehaviourState.Engage;
            state = NodeState.FAILURE;
        }
        else
        {
            enemyBT.currentBehaviourState = BehaviourState.Follow;
            state = NodeState.SUCCESS;
        }
        return state;
    }

    bool CanEngagePlayer()
    {
        //if (EnemiesManager.enemiesEngaged >= 2) //Max number of enemies engaging player
        //  canEngage = false;

        if (player.characterActions.OnCheckLeftTile() is null && player.characterActions.OnCheckRightTile() is null)
        {
            canEngage = false;
        }
        
        if (player.characterActions.OnCheckLeftTile().CompareStateWith(TileState.Occupied)
            && player.characterActions.OnCheckRightTile().CompareStateWith(TileState.Occupied))
        {
            //Left tile not available
            canEngage = false;
        }
        else if (player.characterActions.OnCheckLeftTile().CompareStateWith(TileState.Empty))
        {
            //EnemiesManager.enemiesEngaged++;
            canEngage = true;
        }
        else if (player.characterActions.OnCheckRightTile().CompareStateWith(TileState.Empty))
        {
            //EnemiesManager.enemiesEngaged++;
            canEngage = true;
        }
        return canEngage;
    }
}
