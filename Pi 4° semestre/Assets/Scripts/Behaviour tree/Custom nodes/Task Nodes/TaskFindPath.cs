using BehaviourTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Called when it needs to find a new path
/// </summary>
public class TaskFindPath : NodeBehaviourtree
{
    private EnemyBT enemyBT;
    private CharacterActions characterActions;
    private Grid levelGrid;
    private Player player;
    private EnemiesManager enemiesManager;

    Graph g = new Graph();

    int retreatdTileIndex;
    float smallestDistance = 100;

    float smallestAttackPositionDistance = 100;
    int attackPositionIndex = 0;
    List<BattleTile> availableTiles = new List<BattleTile>();

    public TaskFindPath(EnemyBT _enemyBT, CharacterActions _characterActions, Grid _levelGrid, Player _player, EnemiesManager _enemiesManager): base()
    {
        levelGrid = _levelGrid;
        player = _player;
        enemyBT = _enemyBT;
        characterActions = _characterActions;
        enemiesManager = _enemiesManager;
    }


    public override NodeState Evaluate()
    {
        enemyBT.currentTile = characterActions.OnCurrentTile();

        if (characterActions.OnCheckKnockdown())
        {
            state = NodeState.FAILURE;
        }
        else if (enemyBT.needsToChangePath && enemyBT.currentBehaviourState == BehaviourState.Engage)
        {
            enemyBT.moveDelay = enemyBT.engageMoveDelay;
            ChooseAttackPos();            
        }
        else if (enemyBT.needsToChangePath && enemyBT.currentBehaviourState == BehaviourState.EngageRange)
        {
            enemyBT.moveDelay = enemyBT.engageMoveDelay;
            ChooseRangeAttackPos();
        }
        else if(enemyBT.needsToChangePath && enemyBT.currentBehaviourState == BehaviourState.Follow)
        {
            enemyBT.moveDelay = enemyBT.followMoveDelay;
            ChooseRetreatPos();
        }
        else if (enemyBT.currentBehaviourState == BehaviourState.Idle)
        {
            //Stay idle
            state = NodeState.FAILURE;
        }
        return state;
    }

    void ChooseRangeAttackPos()
    {
        availableTiles.Clear();
        List<BattleTile> possibleMovablePosition = player.characterActions.OnCurrentTile().retreatTiles;


        for (int i = 0; i < possibleMovablePosition.Count; i++)
        {
            if (possibleMovablePosition[i].gridPosition.y == player.characterActions.OnCheckPositionInGrid().y)
            {
                availableTiles.Add(possibleMovablePosition[i]);
            }
        }

        for (int i = 0; i < availableTiles.Count; i++)
        {
            if (Vector2.Distance(enemyBT.transform.position, availableTiles[i].transform.position) < smallestAttackPositionDistance
                && CheckAvailableTile(availableTiles[i])
                && availableTiles[i] is not null)
            {
                //Tile is available to move
                smallestAttackPositionDistance = Vector2.Distance(enemyBT.transform.position, availableTiles[i].transform.position);
                attackPositionIndex = i;
            }
        }

        if (attackPositionIndex >= availableTiles.Count)
        {
            state = NodeState.FAILURE;
            return;
        }

        try
        {
            enemyBT.selectedTargetTile = availableTiles[attackPositionIndex];
            enemyBT.targetTile = enemyBT.selectedTargetTile;
        }
        catch (ArgumentOutOfRangeException)
        {
            state = NodeState.FAILURE;
            return;
        }

        try
        {
            enemyBT.currentPath = g.AStar(levelGrid, enemyBT.currentTile, enemyBT.targetTile);
            enemyBT.currentPath.Reverse();
            state = NodeState.SUCCESS;
        }
        catch (NullReferenceException)
        {
            state = NodeState.FAILURE;
            return;
        }

        //enemyBT.currentPath.Reverse();
        smallestAttackPositionDistance = 100;
    }

    void ChooseRetreatPos()
    {
        List<BattleTile> possibleMovablePosition = player.characterActions.OnCurrentTile().retreatTiles;
        
        for (int i = 0; i < possibleMovablePosition.Count; i++)
        {
            if (Vector2.Distance(enemyBT.transform.position, possibleMovablePosition[i].transform.position) < smallestDistance
                && possibleMovablePosition[i] is not null
                && CheckAvailableTile(possibleMovablePosition[i])
                )
            {
                smallestDistance = Vector2.Distance(enemyBT.transform.position, possibleMovablePosition[i].transform.position);
                retreatdTileIndex = i;
            }
        }
        enemyBT.selectedTargetTile = possibleMovablePosition[retreatdTileIndex];
        enemyBT.targetTile = enemyBT.selectedTargetTile;


        try
        {
            enemyBT.currentPath = g.AStar(levelGrid, enemyBT.currentTile, enemyBT.targetTile);
            enemyBT.currentPath.Reverse();
            state = NodeState.SUCCESS;
        }
        catch (NullReferenceException)
        {
            state = NodeState.FAILURE;
            return;
        }

        //enemyBT.currentPath.Reverse();
        smallestDistance = 100;
    }


    void ChooseAttackPos()
    {
        availableTiles.Clear();

        if (player.characterActions.OnCheckLeftTile() is not null)// && !player.characterActions.OnCheckLeftTile().GetOccupyingObject())
            availableTiles.Add(player.characterActions.OnCheckLeftTile());

        if (player.characterActions.OnCheckRightTile() is not null) //&& !player.characterActions.OnCheckRightTile().GetOccupyingObject())
            availableTiles.Add(player.characterActions.OnCheckRightTile());

        
        for (int i = 0; i < availableTiles.Count;i++)
        {
            if (Vector2.Distance(enemyBT.transform.position, availableTiles[i].transform.position) < smallestAttackPositionDistance 
                && CheckAvailableTile(availableTiles[i]) 
                && availableTiles[i] is not null
                && !availableTiles[i].GetOccupyingObject())
            {
                //Tile is available to move
                smallestAttackPositionDistance = Vector2.Distance(enemyBT.transform.position, availableTiles[i].transform.position);
                attackPositionIndex = i;
            }
        }
        
        //if(attackPositionIndex >= availableTiles.Count)
        //{
          //  state = NodeState.FAILURE;
            //return;
        //}

        try
        {
            enemyBT.selectedTargetTile = availableTiles[attackPositionIndex];
            enemyBT.targetTile = enemyBT.selectedTargetTile;
        }
        catch (ArgumentOutOfRangeException)
        {
            state = NodeState.FAILURE;
        }


        try
        {
            enemyBT.currentPath = g.AStar(levelGrid, enemyBT.currentTile, enemyBT.targetTile);
            enemyBT.currentPath.Reverse();

            state = NodeState.SUCCESS;

        }
        catch (NullReferenceException)
        {
            state = NodeState.FAILURE;
        }

    }

    bool CheckAvailableTile(BattleTile _battleTile)
    {
        for (int i = 0; i < enemiesManager.enemiesInLevel.Count; i++)
        {
            if (enemiesManager.enemiesInLevel[i].enemyBehaviourTree.selectedTargetTile == _battleTile 
                && enemiesManager.enemiesInLevel[i].gameObject != enemyBT.gameObject) 
            {
                //The tile is targeted by another character 
                return false;
            }
        }
        return true;
    }
}
