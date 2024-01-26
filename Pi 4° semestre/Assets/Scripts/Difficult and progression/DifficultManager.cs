using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct DifficultStages
{
    public int minEnemyNumber;
    public int maxEnemyNumber;

    public int minObjectNumber;
    public int maxObjectNumber;

    public List<DestroyableObject> allowedUpgradesShop;
    public List<DestroyableObject> allowedObstacles;
    public List<Enemy> allowedEnemies;
}

public class DifficultManager : MonoBehaviour
{
    public static DifficultManager instance;

    public List<Level> levels;
    public Level currentLevel;
    public int levelsIndex;

    public List<DifficultStage> _difficultStages;
    public DifficultStage currentDifficultStage;
    public int maxStageRooms;
    public int currentStageRooms;
    public int difficultStageIndex;

    private void Awake()
    {
        instance = this; 
    }



    /// <summary>
    /// When finishing a level, after the cafeteria, checks if can increases the difficult level
    /// </summary>
    public void CheckCurrentStageRooms()
    {
        if(currentStageRooms > 1 )
        {
            currentStageRooms--;
        }
    }

    public void UpdateStage()
    {
        if (difficultStageIndex < _difficultStages.Count -1)
        {
            difficultStageIndex++;
            SetStage();
        }
        else 
        {
            //Level finished
            UpdateLevel();
        }

    }

    public void SetStage()
    {
        currentDifficultStage = _difficultStages[difficultStageIndex];
        maxStageRooms = _difficultStages[difficultStageIndex].roomsQuantity;
        currentStageRooms = maxStageRooms;
    }
    void SetNewLevelStages()
    {        

        //Clear previous level stages
        maxStageRooms = 0;
        currentStageRooms = 0;
        difficultStageIndex =0 ;
        _difficultStages.Clear();

        //Set new level
        currentLevel = levels[levelsIndex];
        foreach (DifficultStage _stage in currentLevel.stages)
        {
            _difficultStages.Add(_stage);
        }
        SetStage();
    }

    void UpdateLevel()
    {
        levelsIndex++;
        SetNewLevelStages();
    }



    public void SetLevelIndex(int newIndex)
    {
        levelsIndex = newIndex;
    }
}
