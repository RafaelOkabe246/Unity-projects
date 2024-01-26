using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelType
{
    Tutorial = 0,
    Cafeteria = 1,
    Normal = 2,
    Final = 3
}
/// <summary>
/// Generates the level grid based on the difficult and progress 
/// </summary>
public class LevelGenerator : MonoBehaviour
{
    public LevelType currentLevelType;

    public SpawnerController spawnerController;
    public EnemiesManager enemiesManager;

    public Grid currentLevelGrid;

    DifficultStage _currentDifficultStage;

    [Header("Level background")]
    public GameObject normalBackground;
    public GameObject cafeteriaBackgroud;
    private GameObject currentBackground;

    void OnEnable()
    {
        spawnerController = FindObjectOfType<SpawnerController>();
        enemiesManager = FindObjectOfType<EnemiesManager>();
        enemiesManager.spawnerController = spawnerController;
    }
    void OnDisable()
    {
        spawnerController = null;
        enemiesManager = null;
    }

    public void StartNewRoom(DifficultStage difficultStage)
    {
        if (currentLevelGrid is not null)
        {
            //Cleaar previous level
            Destroy(currentLevelGrid.gameObject);
            currentLevelGrid = null;
            spawnerController.ResetSpawner();
            Destroy(currentBackground);
        }

        _currentDifficultStage = difficultStage;

        SetBackground();
        SetGrid();
    }

    void SetBackground()
    {
        GameObject newBackground = _currentDifficultStage.background;
        Transform backgroundPoint = GameObject.FindGameObjectWithTag("BackgroundPoint").transform;
        currentBackground = Instantiate(newBackground, backgroundPoint.position, Quaternion.identity);
    }

    void SetGrid()
    {
        Grid newGrid = Instantiate(_currentDifficultStage.stageGrid);
        currentLevelGrid = newGrid;
        currentLevelType = _currentDifficultStage.levelType;
        
        //Generate grid before spawning the characters and objecs
        StartCoroutine(currentLevelGrid.Generate());


        StartCoroutine(SetSpawn());
    }

    /// <summary>
    /// 1. Spawn the enemies first in the right side of the grid 
    /// 2. Spawn the objects around the center of the grid
    /// </summary>
    /// <returns></returns>
    private IEnumerator SetSpawn() 
    {
        //Clear objects from previous stage
        spawnerController.ClearObjects(); 

        //Needs to fix that line
        if (currentLevelType != LevelType.Tutorial && TutorialManager.instance.isTutorial)
            TutorialManager.instance.EndTutorial();

        yield return new WaitForSeconds(2f);
        
        //Update spawnercontroller grid
        spawnerController.leveGrid = currentLevelGrid;

        //Enemies and destroyable objects
        int enemiesNumber = Random.Range(_currentDifficultStage.minEnemyNumber, _currentDifficultStage.maxEnemyNumber);
        int destroyableObjectsNumber = Random.Range(_currentDifficultStage.minObjectNumber, _currentDifficultStage.maxObjectNumber);

        //Spawn the shit
        spawnerController.SpawnCharacters(enemiesNumber, _currentDifficultStage.enemies);
        spawnerController.SpawnObjects(destroyableObjectsNumber, _currentDifficultStage.destroyableObjects, currentLevelType);

    }


}
