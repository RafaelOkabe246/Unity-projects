using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(CharacterActions))]
public class PlayerInputs : MonoBehaviour
{
    public PlayerControls inputActions;
    public CharacterActions characterActions;

    private void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.Player.Pause.performed += _context =>
        {
            if (!ScreenStack.instance.stack.Contains(GameplayUIsContainer.instance.GetPauseScreen()))
            {
                ScreenStack.instance.AddScreenOntoStack(GameplayUIsContainer.instance.GetPauseScreen());
            }
        };
        inputActions.Player.Move.performed += _context => characterActions.OnMove(_context.ReadValue<Vector2>());
        inputActions.Player.Attack.performed += _context =>
        {
            if (_context.interaction is PressInteraction)
            {
                characterActions.OnTriggerAttack();
            }
            else if (_context.interaction is HoldInteraction)
            {
                //For now, its empty
                //
            }
        }; 
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Pause.performed -= _context =>
        {
            GameManager.instance.SetState(GameState.Paused);
            ScreenStack.instance.AddScreenOntoStack(GameplayUIsContainer.instance.GetPauseScreen());
        };
        inputActions.Player.Move.performed -= _context => characterActions.OnMove(_context.ReadValue<Vector2>());
        inputActions.Player.Attack.performed -= _context =>
        {
            if (_context.interaction is PressInteraction)
            {
                characterActions.OnTriggerAttack();
            }
            else if (_context.interaction is HoldInteraction)
            {
                //For now, its empty
            }
        }; 
        inputActions.Disable();
    }

}
