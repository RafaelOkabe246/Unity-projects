using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    [Header("Player info")]
    public int playerHp;
    public int playerMaxHp;
    public float playerMoveSpeed;
    public int playerDamage;

    [Header("Level info")]
    public int stagesPassed;
    public int currentLevelIndex;

    [Header("Other info")]
    public int coins;
    public int enemiesDestroyed;
    public int objectsDestroyed;

    public RunInfo saveInfo;
    public SaveData(RunInfo _runInfo)
    {
        saveInfo = _runInfo;
        Debug.Log("Run info saved" + _runInfo);

        playerHp = _runInfo.playerHp;
        playerMaxHp = _runInfo.playerMaxHp;
        playerMoveSpeed = _runInfo.playerMoveSpeed;
        playerDamage = _runInfo.playerDamage; 

        stagesPassed = _runInfo.currentStageIndex;
        currentLevelIndex = _runInfo.currentLevelIndex;

        coins = _runInfo.coins;
        enemiesDestroyed = _runInfo.enemiesDestroyed;
        objectsDestroyed = _runInfo.objectsDestroyed;

}


}
