using UnityEngine;
using System;

/*
 * Delegate that is responsible by linking the player main scripts with Actions, such as the Player Movement and the Player Animations
*/

public static class PlayerActions
{

    public static Action<bool> OnGround;
    public static Func<bool> OnCheckGround;
    public static Action<bool> OnTurn;
    public static Func<bool> OnCheckPlayerDirection; //Used in Player Movement
    public static Func<bool> OnCheckSpriteFlip; //Used in Player Animation Manager
    public static Func<bool> OnCheckInteracting; //Used when is trying to interact with an object

    //Pause
    

    //Death 
    public static Action OnTriggerDeath;
    public static Func<bool> GetIsDead;

    //Movement
    public static Action<bool> OnMove;
    public static Action<bool> ActivateAfterImage;
    public static Action OnTriggerMove;

    //Jump
    public static Action<bool> OnJump;
    public static Func<bool> OnCheckJump;
    public static Action<float> SetVerticalVelocity;
    public static Action OnTriggerJump;
    public static Action<float> OnCallFeedbackJump;

    //Dash
    public static Action<bool> OnDash;
    public static Func<bool> OnCheckDash;
    public static Action OnTriggerDash;
    public static Action OnCancelDash;
    public static Action<Vector2> OnSetSpriteRotationByDashDir;
    public static Action CallDashHitScreenEffect;

    //Collisions
    public static Action<bool> SetPlayerCollision;
    public static Action<bool> IgnoreEnemyCollision;
    public static Func<bool> OnCheckSlope;
    public static Action<bool> IsPlayerOnSlope; //Used in Player Animation Manager

    //Particles
    public static Action<bool> CallMoveParticles;
    public static Action CallJumpParticles;
    public static Action<bool> CallDashParticles;
    public static Action<Vector2> CallDashHitParticles;
    public static Action CallDashStartParticles;

}
