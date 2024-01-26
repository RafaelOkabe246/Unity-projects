using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class CheckRangeAttackArea : NodeBehaviourtree
{
    private CharacterActions characterActions;
    private Grid gridLevel;
    private Player player;

    public CheckRangeAttackArea(CharacterActions _characterActions, Grid _gridLevel, Player _player)
    {
        characterActions = _characterActions;
        gridLevel = _gridLevel;
        player = _player;
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
        else if (player.characterActions.OnCheckPositionInGrid().y == characterActions.AttackLocation().y
            && player.characterActions.OnCurrentTile().retreatTiles.Contains(characterActions.OnCurrentTile()))
        {
            state = NodeState.SUCCESS;
        }
        /*
        else if (!_battleTile.GetOccupyingObject())
        {
            state = NodeState.FAILURE;
        }
        else if (_battleTile.GetOccupyingObject().CompareTag("Player"))
        {
            state = NodeState.SUCCESS;
        }
        */
        else
        {
            state = NodeState.FAILURE;
        }

        return state;
    }
}
