using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public abstract class Character : ObjectTile, IHandlesGameState
{
    public CharacterStates currentCharState;

    public CharacterStats characterStats;

    public CharacterMovement characterMovement;
    public CharacterActions characterActions;
    public CharacterCollision characterCollision;

    public SpawnerController spawnerController;

    public CharacterStatsComponent statsComponent;

    public bool isDead;

    [SerializeField] private AudioSource audioSource;

    protected virtual void Start()
    {
        statsComponent.maxHP = characterStats.maxHP;
        statsComponent.HP = statsComponent.maxHP;
        statsComponent.lightHitDamage = characterStats.normalHitDamage;
        statsComponent.moveSpeed = characterStats.minMoveSpeed;
    }
    public override void Initialize()
    {
        
    }

    private void Update()
    {
        statsComponent.lifeText.text = $"{statsComponent.HP} / {statsComponent.maxHP}";
    }

    protected virtual void OnEnable()
    {
        characterActions.OnCheckCharacter += CheckCharacter;
        GameManager.instance.OnGameStateChanged += OnChangeGameState;
        characterActions.OnTriggerDeath += OnDead;
        characterActions.OnCheckIsDead += CheckIsDead;
        characterActions.OnChangeCharacterState += ChangeCharacterState;
        characterActions.OnPlayAudio += PlayCharacterAudio;
    }

    protected virtual void OnDisable()
    {
        characterActions.OnCheckCharacter -= CheckCharacter;
        GameManager.instance.OnGameStateChanged -= OnChangeGameState;
        characterActions.OnTriggerDeath -= OnDead;
        characterActions.OnCheckIsDead -= CheckIsDead;
        characterActions.OnChangeCharacterState -= ChangeCharacterState;
        characterActions.OnPlayAudio -= PlayCharacterAudio;
    }

    public virtual void OnChangeGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case (GameState.Paused):
                //Enter pause state
                characterActions.SetCanTakeDamage(false);
                characterMovement.enabled = false;
                break;
            case (GameState.Gameplay):
                //Enter gameplay state
                characterMovement.enabled = true;
                characterActions.SetCanTakeDamage(true);
                break;
        }
    }

    public virtual void ChangeCharacterState(CharacterStates newCharacterState)
    {
        switch (newCharacterState)
        {
            case CharacterStates.Knocked:
                //gameObject.layer = 7;
                break;
            case CharacterStates.Alive:
                //gameObject.layer = 6;
                break;
            case CharacterStates.Dead:
                break;
            default:
                break;
        }
    }

    public void PlayCharacterAudio(string audioReference, bool hasAudioSource)
    {
        if(hasAudioSource)
            SoundManager.instance.PlayAudio(audioReference, AudioType.CHARACTER, audioSource);
        else 
            SoundManager.instance.PlayAudio(audioReference, AudioType.CHARACTER, null);
    }

    protected virtual void OnDead()
    {
        CameraController.instance.ChangeOrthosizeTemporally(3f);
        PostProcessingManager.instance.SetTemporaryChromaticAberration(1f, 0.25f);
        PostProcessingManager.instance.SetTemporaryBloom(0.9f, 1f, 0.7f, 0.25f);
        characterActions.OnCurrentTile().ClearTile();
        ChangeCharacterState(CharacterStates.Dead);
        isDead = true;
        gameObject.SetActive(false);
    }

    #region FUNC_METHODS
    bool CheckIsDead()
    {
        return isDead;
    }

    Character CheckCharacter()
    {
        return this;
    }

    #endregion


}
