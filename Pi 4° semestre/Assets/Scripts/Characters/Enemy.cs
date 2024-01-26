using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public string enemyName;

    public EnemiesManager enemiesManager;
    public EnemyBT enemyBehaviourTree;

    protected override void Start()
    {
        base.Start();
        enemyBehaviourTree = GetComponent<EnemyBT>();
    }

    public override void Initialize()
    {
        
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        characterActions.OnGetEnemiesManager += GetEnemiesManager;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        characterActions.OnGetEnemiesManager -= GetEnemiesManager;
    }

    #region Atalho_Dev
    public void DEV_Destroy()
    {
        OnDead();
    }
    #endregion

    #region FUNC_METHODS
    EnemiesManager GetEnemiesManager()
    {
        return enemiesManager;
    }
    #endregion

    protected override void OnDead()
    {
        base.OnDead();
        CoinsManager.instance.GainCoins();
        ProgressionManager.instance.currentRunInfo.enemiesDestroyed++;
        enemiesManager.CheckEnemiesAllDead();
    }

    public override void ChangeCharacterState(CharacterStates newCharacterState)
    {
        switch (newCharacterState)
        {
            case CharacterStates.Knocked:
                gameObject.layer = 7;
                characterActions.OnChangeBehaviourState(BehaviourState.Idle);
                break;
            case CharacterStates.Alive:
                gameObject.layer = 6;
                break;
            case CharacterStates.Dead:
                break;
            default:
                break;
        }
    }

    public override void OnChangeGameState(GameState newState)
    {
        switch (newState)
        {
            case (GameState.Paused):
                //Enter pause state
                characterActions.SetCanTakeDamage(false);
                characterMovement.enabled = false;
                enemyBehaviourTree.enabled = false;
                break;
            case (GameState.Gameplay):
                //Enter gameplay state
                characterMovement.enabled = true;
                characterActions.SetCanTakeDamage(true);
                enemyBehaviourTree.enabled = true;
                break;
        }
    }
}
