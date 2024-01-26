using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager instance;

    public PlayerStats stats;
    public PlayerAudioSources playerAudios;

    public float currentHP;
    public float currentDashAmount;

    private bool isDead = false;

    private void OnEnable()
    {
        PlayerActions.GetIsDead += GetIsDead;
    }

    private void OnDisable()
    {
        PlayerActions.GetIsDead -= GetIsDead;
    }

    private void Awake()
    {
        instance = this;
        currentHP = stats.maxHP;
        currentDashAmount = stats.dashAmount;
    }
    public void SpendHP(float valueToSpend)
    {
        currentHP -= valueToSpend;
        if (currentHP <= 0)
        {
            //Sound call
            SoundsManager.instance.PlayAudio(AudiosReference.playerDeath, AudioType.PLAYER, playerAudios.audioSource1);
            currentHP = 0;
            //Trigger respawn event
            PlayerActions.OnTriggerDeath();
            StartCoroutine(CameraController.CameraShake(3,3, 0.5f));
            isDead = true;

            StageBlocksHandler.RestoreCollectedFruits();
            StageBlocksHandler.RestoreCollectedCrystals();
            EnemiesRespawner enemiesRespawner = FindObjectOfType<EnemiesRespawner>();
            enemiesRespawner.ClearEnemyQueue();

            PlayerMovement playerMovement = GetComponent<PlayerMovement>();
            playerMovement.canMove = false;
        }
        else if (currentHP >= stats.maxHP)
            currentHP = stats.maxHP;
    }

    private void Death()
    {
        LevelLoader.instance.ReloadLevel();
    }

    private bool GetIsDead() 
    {
        return isDead;
    }
}
