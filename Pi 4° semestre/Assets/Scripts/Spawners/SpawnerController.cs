using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Spawn the characters in the active scene.
/// </summary>
public class SpawnerController : MonoBehaviour
{
    [SerializeField]
    private Player playerPrefab;
    public Player playerReference;

    public List<Enemy> enemies;

    public EnemiesManager enemiesManager;

    public Grid leveGrid;

    public List<BattleTile> openTiles = new List<BattleTile>(); //Tiles that are not occupied
    public List<BattleTile> closedTiles = new List<BattleTile>(); //Tiles that are already occupied

    public List<DestroyableObject> destroyableObjects = new List<DestroyableObject>();

    public TriggerEventObject triggerEndGamePrefab;
    public TriggerEventObject triggerEventObjectPrefab;

    public List<TriggerEventObject> triggerEventObjects = new List<TriggerEventObject>();

    public void ResetSpawner()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Destroy(enemies[i].gameObject);
        }
        enemies.Clear();
        //enemiesManager.ResetEnemiesManager();
    }

    public void AddEnemy(Enemy _enemy)
    {
        enemies.Add(_enemy);
    }



    #region SPAWN_CHARACTERS
    public void SpawnCharacters(int _enemiesNumber, List<Enemy> _enemiesAllowed)
    {
        openTiles.Clear();

        Player newPlayer = Instantiate(playerPrefab);
        if (playerReference)
        {
            //Update the stats based on the current run info
            DefinePlayerStats(newPlayer);
            Destroy(playerReference.gameObject);
        }
        else
        {
            newPlayer.Initialize();
        }
        newPlayer.spawnerController = this;
        newPlayer.characterMovement.levelGrid = leveGrid;
        newPlayer.characterMovement.currentGridPosition = new Vector2(0, Mathf.Round(leveGrid.gridHeight/2));
        newPlayer.gameObject.SetActive(true);
        newPlayer.characterMovement.SetInitialPosition();
        playerReference = newPlayer;
        

        #region Spawn_enemies
        //Add open tiles
        foreach (BattleTile battleTile in leveGrid.battleTiles.Values)
        {
            if (battleTile.gridPosition.x == leveGrid.gridWidth 
                || battleTile.gridPosition.x == leveGrid.gridWidth - 1)
            {
                openTiles.Add(battleTile);
            }
        }

        int enemiesQuantity = _enemiesNumber;

        int openTileIndex = 0;
        int maxOpenTiles = openTiles.Count;

        for (int i = 0; i < enemiesQuantity; i++)
        {
            for (int x = 0; x < openTiles.Count; x++)
            {
                if (openTiles[x].GetOccupyingObject())
                {
                    openTiles.RemoveAt(x);
                }
            }

            maxOpenTiles = openTiles.Count;
            
            openTileIndex = Random.Range(0, maxOpenTiles);
            
            //Spawn enemy
            int enemyIndex = Random.Range(0, _enemiesAllowed.Count);
            EnemySpawn(openTiles[openTileIndex].gridPosition, _enemiesAllowed[enemyIndex]);
            openTiles.Remove(openTiles[openTileIndex]);
        }
        #endregion

    }

    void DefinePlayerStats(Player newPlayer)
    {
        newPlayer.statsComponent.HP = ProgressionManager.instance.currentRunInfo.playerHp;
        newPlayer.statsComponent.maxHP = ProgressionManager.instance.currentRunInfo.playerMaxHp;
        newPlayer.statsComponent.moveSpeed = ProgressionManager.instance.currentRunInfo.playerMoveSpeed;
        newPlayer.statsComponent.lightHitDamage = ProgressionManager.instance.currentRunInfo.playerDamage;
    }

    void EnemySpawn(Vector2 enemySpawnPos, Enemy spawnedEnemy)
    {
        Enemy spawnedEnemy1 = Instantiate(spawnedEnemy);
        AddEnemy(spawnedEnemy1);
        spawnedEnemy1.enemiesManager = enemiesManager;
        spawnedEnemy.enemyName = spawnedEnemy.gameObject.name;
        spawnedEnemy1.name = spawnedEnemy1.name + $" {enemySpawnPos}";
        spawnedEnemy1.characterMovement.currentGridPosition = enemySpawnPos;
        spawnedEnemy1.characterMovement.levelGrid = leveGrid;
        spawnedEnemy1.spawnerController = this;
        spawnedEnemy1.characterMovement.SetInitialPosition();

        EnemyBT enemyBT = spawnedEnemy1.GetComponent<EnemyBT>();
        enemyBT.player = playerReference;
        enemiesManager.enemiesInLevel.Add(spawnedEnemy1);
        //enemiesManager.enemiesAttackQueue.Enqueue(spawnedEnemy1);
    }
    #endregion

    #region SPAWN_OBJECTS

    public void ClearObjects()
    {

        foreach (DestroyableObject destroyableObject in destroyableObjects)
        {
            Destroy(destroyableObject.gameObject);
        }
        destroyableObjects.Clear();

        foreach (TriggerEventObject triggerEventObject in triggerEventObjects)
        {
            Destroy(triggerEventObject.gameObject);
        }
        triggerEventObjects.Clear();

        IEnumerable<Colectable> colectables = FindObjectsOfType<Colectable>();
        foreach (Colectable upgradeColectable in colectables)
        {
            Destroy(upgradeColectable.gameObject);
        }
    }

    int openObjectTileIndex = 0;
    int maxObjectOpenTiles = 0;
    public void SpawnObjects(int _objectsNumber, List<DestroyableObject> _objectsList, LevelType _levelType)
    {
        openTiles.Clear();

        //Spawn Trigger Objects
        SpawnTriggerObjects(1);


        //Add open tiles
        foreach (BattleTile battleTile in leveGrid.battleTiles.Values)
        {
            if (battleTile.gridPosition.x == 2 || battleTile.gridPosition.x == 3 || battleTile.gridPosition.x == 4)
            {
                if(!battleTile.GetInteractableObject())
                    openTiles.Add(battleTile);
            }
        }


        //Spawn destroyable objects
        if(_objectsList.Count > 0)
        {
            for (int i = 0; i < _objectsNumber; i++)
            {
                for (int y = 0; y < openTiles.Count; y++)
                {
                    if (openTiles[y].GetOccupyingObject())
                    {
                        openTiles.RemoveAt(y);
                    }
                }

                maxObjectOpenTiles = openTiles.Count;

                openObjectTileIndex = Random.Range(0, maxObjectOpenTiles);

                //Spawn object
                int objectIndex = Random.Range(0, _objectsList.Count);
                SpawnDestroyableObjects(openTiles[openObjectTileIndex].gridPosition, _objectsList[objectIndex]);
                openTiles.Remove(openTiles[openObjectTileIndex]);
            }
        }

    }

    void SpawnDestroyableObjects(Vector2 _spawnPos, DestroyableObject _object)
    {
        DestroyableObject destroyableObject = Instantiate(_object);
        destroyableObject.levelGrid = leveGrid;
        destroyableObject.currentGridPosition = _spawnPos;
        destroyableObjects.Add(destroyableObject);
    }

    /// <summary>
    /// Digite o valor int que indique a condição desejada para o spawn
    /// AllEnemiesDead = 0,
    /// StartStage = 1,
    /// </summary>
    /// <param name="_conditionToSpawn"></param>
    public void SpawnTriggerObjects(int _conditionToSpawn)
    {
        DifficultStage difficultStage = DifficultManager.instance.currentDifficultStage;
        foreach (TriggerObject _triggerObject in difficultStage.triggerEventObjects)
        {
            if((int)_triggerObject.conditionToSpawn == _conditionToSpawn)
            {
                SpawnTriggerObject(_triggerObject.gridPos, _triggerObject.triggerObjectPrefab);
            }

        }
    }

    void SpawnTriggerObject(Vector2 _spawnPos, TriggerEventObject triggerPrefab)
    {
        TriggerEventObject triggerNewLevelObject = Instantiate(triggerPrefab);
        triggerNewLevelObject.levelGrid = leveGrid;
        triggerNewLevelObject.currentGridPosition = _spawnPos;
        triggerNewLevelObject.Initialize();
        triggerEventObjects.Add(triggerNewLevelObject);

    }

    #endregion
}
