using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public PlayerInputs playerInputs;
    public GameEvent playerDeadEvent;

    protected override void Start()
    {
    
    }

    public override void Initialize()
    {
        statsComponent.maxHP = characterStats.maxHP;
        statsComponent.HP = statsComponent.maxHP;
        statsComponent.lightHitDamage = characterStats.normalHitDamage;
        statsComponent.moveSpeed = characterStats.minMoveSpeed;
    }

    public override void OnChangeGameState(GameState newState)
    {
        switch (newState)
        {
            case (GameState.Paused):
                //Enter pause state
                playerInputs.enabled = false;
                characterActions.SetCanTakeDamage(false);
                characterMovement.enabled = false;
                break;
            case (GameState.Gameplay):
                //Enter gameplay state
                playerInputs.enabled = true;
                characterMovement.enabled = true;
                characterActions.SetCanTakeDamage(true);
                break;
        }
    }

    protected override void OnDead()
    {
        base.OnDead();
        //Call to end level and show defeat screen
        PostProcessingManager.instance.SetFilmGrain(1f);
        PostProcessingManager.instance.SetColorAdjustment(-100f);

        playerDeadEvent.Raise(this, null);
        //GameManager.instance.SetState(GameState.Paused);
        //ScreenStack.instance.AddScreenOntoStack(GameplayUIsContainer.instance.GetDefeatScreen());

    }
}
