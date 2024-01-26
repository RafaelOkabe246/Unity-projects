using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerControlsNamespace;

/*
 * Handles player's input methods
*/

public class Player : Character
{
    

    private PlayerControls playerControls;

    private void Awake()
    {
        if(playerControls == null)
            playerControls = new PlayerControls();

    }


    private void Start()
    {

    }

    private void OnEnable()
    {
        
        playerControls.CharacterControl.MoveCharacter.performed += _context => characterMovement.Move(_context.ReadValue<Vector2>());
        playerControls.CharacterControl.MoveCharacter.performed += _context => characterMovement.CheckFacingDirection(_context.ReadValue<Vector2>());
        playerControls.CharacterControl.MoveCharacter.canceled += _context => characterMovement.StopMove();

        playerControls.CharacterControl.Jump.started += _context => characterActions.CallJump();

        playerControls.CharacterControl.Attack.performed += _context => characterMovement.OnAttackAction();
        playerControls.Enable();
    }

    private void OnDisable()
    {
       
        playerControls.CharacterControl.MoveCharacter.performed -= _context => characterMovement.Move(_context.ReadValue<Vector2>());
        playerControls.CharacterControl.MoveCharacter.performed -= _context => characterMovement.CheckFacingDirection(_context.ReadValue<Vector2>());
        playerControls.CharacterControl.MoveCharacter.canceled -= _context => characterMovement.StopMove();

        playerControls.CharacterControl.Jump.started -= _context => characterActions.CallJump();

        playerControls.CharacterControl.Attack.performed -= _context => characterMovement.OnAttackAction();
        playerControls.Disable();
    }


    
}

