using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerControlsNamespace;

/*
 * Handles character's interactions with the game state manager and other systems
 * Also contains the stats
*/
public class Character : MonoBehaviour
{
    public CharacterMovement characterMovement;
    public CharacterAnimationManager characterAnimationManager;
    public CharacterActions characterActions;

    public int Damage;
    public int HP;
    public int maxHP;

    private void Start()
    {
        HP = maxHP;
    }
}
