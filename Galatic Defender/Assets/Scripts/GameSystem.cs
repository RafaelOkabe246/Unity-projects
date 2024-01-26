using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public enum GAMESTATE { JOGO, PAUSE, MENU}

public class GameSystem : MonoBehaviour
{
    public GAMESTATE currentGameState;
    public EnemiesManager enemiesManager;
    public BuildingsManager buildingsManager;
    public PlayerController playerController;

    [Header("Player weapons")]
    public Missle allyMissle;

    [Header("Enemies")]
    public Missle enemyMissle;

    [Header("Levels")]
    public List<LevelWaves> levelWaves;
    public int CurrentLevel;

    private void Start()
    {
        StartLevel();
    }

    public void Update()
    {
        if (enemiesManager._EnemiesInCurrentLevel == 0)
        {
            //Terminou o level
            LevelFinished();
        }
    }

    #region Level_manager

    void LevelFinished()
    {
        PassLevel();
    }

    void StartLevel()
    {
        enemiesManager.StartWaves();
    }

    public void PassLevel()
    {
        if (CurrentLevel < levelWaves.Count)
        {
            CurrentLevel += 1;
            currentGameState = GAMESTATE.MENU;
        }
        else
        {
            //Acabaram os levels
        }
    }
    #endregion


    #region Game_state

    #endregion
}
