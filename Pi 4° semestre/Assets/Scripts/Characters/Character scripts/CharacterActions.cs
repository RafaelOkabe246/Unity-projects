using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class CharacterActions : MonoBehaviour
{
    public Action<CharacterStates> OnChangeCharacterState;
    public Func<Character> OnCheckCharacter;
    public Func<CharacterStatsComponent> OnGetCharacterStatsComponent;

    //Sounds
    public Action<string, bool> OnPlayAudio;

    //Game Manager
    public Action<bool> OnPause;

    
    public Action OnTryInteract;

    //Movement
    public Action<Vector2> OnMove;
    public Action<bool> ActivateAfterImage;
    public Action OnFlip;

    public Func<Grid> OnGetLevelGrid;
    public Func<BattleTile> OnCurrentTile;
    public Func<BattleTile> OnCheckLeftTile;
    public Func<BattleTile> OnCheckRightTile;
    public Func<bool> OnCheckCanMove;
    public Func<bool> OnCheckLookingDirection; //Used to check looking direction
    public Func<bool> OnCheckIsMoving;
    public Func<Vector2> OnCheckPositionInGrid;
    public Func<Transform> GetCharacterTransform;
    public Func<float> GetMoveSpeed;
    public Func<TextMeshPro> GetLifeText;

    //Animation
    public Action<bool> OnFlipSprite;
    public Func<CharacterAnimations> OnGetCharacterAnimations;

    public Func<bool> OnCheckSpriteFlip; //Used in Player Animation Manager
    public Func<SpriteRenderer> OnGetSpriteGFX; 

    public Func<List<SpriteRenderer>> OnGetRobotPieces; //Enemies only
    public Func<RobotPieces> OnGetRobotPiecesScript;

    //Death 
    public Action OnTriggerDeath;
    public Func<bool> IsDead;

    //Collisions
    public Action<bool> SetCharacterCollision;
    public Action<bool> IgnoreCollision;

    //Particles
    public Action<Vector2> CallMoveParticles;

    //Combat
    public Action OnTriggerRangeAttack; //Active skill manager attack
    public Action OnTriggerAttack; //Active skill manager attack
    public Action OnTakeDamage;
    public Action<bool> SetCanTakeDamage;
    public Action<int> OnHeal;
    public Action<BehaviourState> OnChangeBehaviourState;
    public Action<bool> OnKnockDown;
    public Func<float> KnockdownTime;

    public Func<float> OnCheckCurrentAnimationState;
    public Func<int> OnCheckComboStep; 
    public Func<Vector2> AttackLocation;
    public Func<bool> OnCheckKnockdown;
    public Func<bool> OnCanIgnoreTakeDamageAnim;
    public Func<bool> OnCanTakeDamage;
    public Func<bool> OnCheckIsAttacking;
    public Func<bool> OnCheckIsDead;
    public Func<List<Upgrade>> GetUpgrades;
    public Func<BehaviourState> OnCheckBehaviourState;

    public Func<EnemiesManager> OnGetEnemiesManager; //Enemies only

}
