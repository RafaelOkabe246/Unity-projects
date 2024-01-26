using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class TaskMovePatrol : NodeBehaviourtree
{
    private CharacterActions characterActions;
    private Grid levelGrid;
    private EnemyBT enemyBT;
    //List<Vector2> pathPositions = new List<Vector2>();

    public TaskMovePatrol(EnemyBT _enemyBT, CharacterActions _characterActions, Grid _levelGrid)
    {
        enemyBT = _enemyBT;
        levelGrid = _levelGrid;
        characterActions = _characterActions;
    }
    public override NodeState Evaluate()
    {

        if (!characterActions.OnCheckCanMove() || characterActions.OnCurrentTile() == enemyBT.targetTile)//enemyBT.pathIndex == enemyBT.currentPath.Count)
        {
            //Reached final destination
            state = NodeState.FAILURE;
            return state;
        }
        
        //Updates list index
        if (enemyBT.pathIndex < enemyBT.currentPath.Count 
            && characterActions.OnCheckPositionInGrid() == enemyBT.currentPath[enemyBT.pathIndex].gridPosition) //The position is equal to the current path position
        {

            enemyBT.pathIndex++;
            enemyBT.needsToChangePath = true;
        }
        
        state = NodeState.RUNNING;



        try
        {
            characterActions.OnMove(enemyBT.currentPath[enemyBT.pathIndex].gridPosition - characterActions.OnCheckPositionInGrid());
            CheckLookingDir();

        }
        catch (ArgumentOutOfRangeException)
        {
            state = NodeState.FAILURE;
        }

        return state;
    }
    
    void CheckLookingDir()
    {
        if (!enemyBT.characterActions.OnCheckLookingDirection() 
            && enemyBT.player.transform.position.x - enemyBT.transform.position.x > 0) //look right
        {
            characterActions.OnFlip();
        }
        else if (enemyBT.characterActions.OnCheckLookingDirection() 
            && enemyBT.player.transform.position.x - enemyBT.transform.position.x < 0) //look left
        {
            characterActions.OnFlip();
        }
    }
}

