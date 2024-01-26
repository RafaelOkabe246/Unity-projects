using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;


[CreateAssetMenu(fileName = "Level - Stage", menuName = "Difficult Stage")]
public class DifficultStage : ScriptableObject
{
    public LevelType levelType;

    public Grid stageGrid;
    [SerializeField]private int gridWidth;
    [SerializeField]private int gridHeight;

    public int minEnemyNumber;
    public int maxEnemyNumber;

    public int minObjectNumber;
    public int maxObjectNumber;

    [Range(1, 10)]
    public int roomsQuantity;

    public List<DestroyableObject> buyableItems;
    public List<DestroyableObject> destroyableObjects;
    public List<Enemy> enemies;

    public List<TriggerObject> triggerEventObjects;

    [Header("Scenario layout")]
    public GameObject background;

    private void OnValidate()
    {
        gridWidth = stageGrid.gridWidth;
        gridHeight = stageGrid.gridHeight;

        foreach (TriggerObject tObj in triggerEventObjects)
        {
            switch (tObj.spawnAnchor)
            {
                case SpawnAnchor.Center:
                    tObj.gridPos = new Vector2(Mathf.Round(stageGrid.gridWidth / 2) + tObj.offsetX, Mathf.Round(stageGrid.gridHeight / 2) + tObj.offsetY);
                    break;
                case SpawnAnchor.Botton_Left:
                    tObj.gridPos = new Vector2(0 + tObj.offsetX, 0 + tObj.offsetY);
                    break;
                case SpawnAnchor.Top_Left:
                    tObj.gridPos = new Vector2(0 + tObj.offsetX, Mathf.Round(stageGrid.gridHeight - 1) + tObj.offsetY);
                    break;
                case SpawnAnchor.Left:
                    tObj.gridPos = new Vector2(0 + tObj.offsetX, Mathf.Round(stageGrid.gridHeight / 2) + tObj.offsetY);
                    break;
                case SpawnAnchor.Botton_Right:
                    tObj.gridPos = new Vector2(stageGrid.gridWidth - 1 + tObj.offsetX, 0 + tObj.offsetY);
                    break;
                case SpawnAnchor.Top_Right:
                    tObj.gridPos = new Vector2(stageGrid.gridWidth - 1 + tObj.offsetX, stageGrid.gridHeight - 1 + tObj.offsetY);
                    break;
                case SpawnAnchor.Right:
                    tObj.gridPos = new Vector2(stageGrid.gridWidth - 1 + tObj.offsetX, Mathf.Round(stageGrid.gridHeight / 2) + tObj.offsetY);
                    break;
                case SpawnAnchor.Top:
                    tObj.gridPos = new Vector2(Mathf.Round(stageGrid.gridWidth / 2) + tObj.offsetX, stageGrid.gridHeight - 1 + tObj.offsetY);
                    break;
                case SpawnAnchor.Botton:
                    tObj.gridPos = new Vector2(Mathf.Round(stageGrid.gridWidth / 2) + tObj.offsetX, 0 + tObj.offsetY);
                    break;
                
            }
        }
        
    }
}
    [System.Serializable]
public class TriggerObject
{
    public TriggerEventObject triggerObjectPrefab;
    public float offsetX, offsetY;
    public Vector2 gridPos;
    public ConditionToSpawn conditionToSpawn;
    public SpawnAnchor spawnAnchor;
}
public enum SpawnAnchor
{
    Center,
    Botton_Left,
    Top_Left,
    Left,
    Botton_Right,
    Top_Right,
    Right,
    Top,
    Botton,
}
public enum ConditionToSpawn
{
    AllEnemiesDead = 0,
    StartStage = 1,
}
