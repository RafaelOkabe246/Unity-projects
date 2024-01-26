using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public PlayerControls inputActions;

    private void Awake()
    {
        inputActions = new PlayerControls();    
    }
    private void Start()
    {
    }

    private void OnEnable()
    {
        //inputActions.Player.Navigation.performed += _context => PlayerActions.instance.dirNavigation(_context.ReadValue<int>());
        //inputActions.Player.Select.performed += _context => PlayerActions.instance.select();

    }

    private void OnDisable()
    {
        //inputActions.Player.Navigation.performed -= _context => PlayerActions.instance.dirNavigation(_context.ReadValue<int>());
        //inputActions.Player.Select.performed -= _context => PlayerActions.instance.select();
    }

}
