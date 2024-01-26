using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CharacterActions : MonoBehaviour
{
    public Action<bool> OnGround;
    public Func<bool> OnCheckGround;

    public Func<bool> OnCheckPlayerDirection; //Used in Player Movement
    public Func<bool> OnCheckSpriteFlip; //Used in Player Animation Manager
    public Func<bool> OnCheckInteracting; //Used when is trying to interact with an object

    //Battle
    public Func<bool> IsAttackig;

    //Death 
    public Action OnTriggerDeath;
    public Func<bool> GetIsDead;

    //Movement
    public Action<bool> OnMove;
    public Func<bool> OnCheckCanMove;
    public Action OnTriggerMove;
    public Func<bool> CheckCanTurn;
    public Action<bool> OnTurn;

    //Jump
    public Action CallJump;
    public Func<bool> isJumping;

    //Dash

    //Collisions
    public Action TakeDamage;
    public Func<bool> isKnockdown;
    public Action<bool> OnKnockDown;
    public Func<bool> isInvencible;

    //Particles
}
