using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum GameState
{
    GenerateGrid,
    SpawnUnits,
    PlayerTurn,
    UnitSelected,
    EnemyTurn
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState GameState;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ChangeState(GameState.GenerateGrid);
    }

    private void Update()
    {
        StateManager();
    }

    public void StateManager()
    {
        switch (GameState)
        {
            case GameState.GenerateGrid:
                GridManager.instance.GeneratorGrid();
                break;
            case GameState.EnemyTurn:
                //Play enemy stuff
                Debug.Log("2 player turn turn");


                //Reset unity actions
                UnitManager.instance.ResetPlayerActions();
                ChangeState(GameState.PlayerTurn);
                break;
        }
    }


    public void ChangeState(GameState newState)
    {
        GameState = newState;

    }


    public void EndTurn()
    {
        //Check if all the enemies are dead
        bool allEnemiesDead = UnitManager.instance._enemyUnits.All(enemies => enemies._HP <= 0);
        if (allEnemiesDead == transform)
        {
            Debug.Log("The game is over");
            //Play the animation
        }
            ChangeState(GameState.EnemyTurn);
    }
}
