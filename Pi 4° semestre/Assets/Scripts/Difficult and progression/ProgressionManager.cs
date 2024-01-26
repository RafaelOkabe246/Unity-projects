using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public struct RunInfo
{
    [Header("Player info")]
    public int playerHp;
    public int playerMaxHp;
    public float playerMoveSpeed;
    public int playerDamage;

    [Header("Level info")]
    public int currentStageIndex;
    public int currentLevelIndex;

    [Header("Other info")]
    public int coins;
    public int enemiesDestroyed;
    public int objectsDestroyed;
}

public class ProgressionManager : MonoBehaviour, IHandlesGameState
{
    public bool hasStartedGame;
    public SpawnerController spawnerController;
    public static ProgressionManager instance;
    LevelType newLevelType = LevelType.Normal;

    public LevelGenerator levelGenerator;

    private bool isLoadingNextStage;
    public bool canSpawnEnemies;

    public float timerCount;

    [Header("Current run info")]
    public RunInfo currentRunInfo;
    public GameEvent updateLevelTextEvent;
    public Action OnLevelTrigger;

   // [Header("ATALHOS DEV")]

    private void Awake()
    {
        instance = this;
    }
    IEnumerator Start()
    {
        hasStartedGame = true;
        if(SaveSystem.LoadGame() != null)
        {
            LoadSaveData();
        }

        GameManager.instance.SetState(GameState.Gameplay);

        yield return new WaitForSeconds(0);
        StartLevelTrigger();
        hasStartedGame = false;
    }

    private void OnEnable()
    {
        GameManager.instance.OnGameStateChanged += OnChangeGameState;
        OnLevelTrigger += SaveRunData;
        OnLevelTrigger += TriggerNewStage;
    }

    private void OnDisable()
    {
        GameManager.instance.OnGameStateChanged -= OnChangeGameState;
        OnLevelTrigger -= SaveRunData;
        OnLevelTrigger -= TriggerNewStage;
    }

    void LoadSaveData()
    {
        currentRunInfo = SaveSystem.LoadGame().saveInfo;
        DifficultManager.instance.levelsIndex = currentRunInfo.currentLevelIndex;
        DifficultManager.instance.difficultStageIndex = currentRunInfo.currentStageIndex;
    }

    public void EndGame()
    {
        SceneLoader.instance.LoadLevel(0);
    }

    public void StartLevelTrigger()
    {
        if(!isLoadingNextStage)
            OnLevelTrigger?.Invoke();
    }

    public void SaveRunData()
    {
        if (hasStartedGame)
            return;

        //Save data from the level results
        //Level and stages
        currentRunInfo.currentStageIndex = DifficultManager.instance.difficultStageIndex;
        currentRunInfo.currentLevelIndex = DifficultManager.instance.levelsIndex;

        //Player
        currentRunInfo.playerMaxHp = spawnerController.playerReference.statsComponent.maxHP;
        currentRunInfo.playerHp = spawnerController.playerReference.statsComponent.HP;
        currentRunInfo.playerDamage = spawnerController.playerReference.statsComponent.lightHitDamage;
        currentRunInfo.playerMoveSpeed = spawnerController.playerReference.statsComponent.moveSpeed;
        
        //Money, objects and enemies
        currentRunInfo.coins = CoinsManager.instance.GetCoins();
        
        //Deactivate player (optional)
        spawnerController.playerReference.gameObject.SetActive(false);

        //Save the changes at the save system
        SaveSystem.SaveTheGame(currentRunInfo);

    }

    public void TriggerNewStage()
    {
        isLoadingNextStage = true;
        updateLevelTextEvent.Raise(this, currentRunInfo);
        DifficultManager.instance.UpdateStage();

        //Tells the level generator to create a new level, for each "x" levels, based on difficult, it will generate a cafeteria level
        StartCoroutine(nameof(StartNextRoom));
    }

    

    public void OnChangeGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case (GameState.Gameplay):
                
                break;
            case (GameState.Paused):

                break;
        }
    }

    

    #region Runs_methods

    IEnumerator StartNextRoom()
    {
        levelGenerator.gameObject.SetActive(false);
        yield return new WaitForSeconds(0);
        isLoadingNextStage = false;
        levelGenerator.gameObject.SetActive(true);
        DifficultManager.instance.CheckCurrentStageRooms();

        levelGenerator.StartNewRoom(DifficultManager.instance.currentDifficultStage); 
    }

    #endregion
}
