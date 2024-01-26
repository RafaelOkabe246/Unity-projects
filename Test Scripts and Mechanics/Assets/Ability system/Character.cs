using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


public class Character : MonoBehaviour
{
    public PlayerControls inputActions ;
    private void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Actions.Attack.performed += context =>
        {
            if(context.interaction is TapInteraction)
            {
                Debug.Log("Normal attack");
            }
            else if(context.interaction is HoldInteraction)
            {
                Debug.Log("Strong attack");
            }
        };

    }

    private void OnDisable()
    {
        inputActions.Disable();
        inputActions.Actions.Attack.performed -= context =>
        {
            if (context.interaction is PressInteraction)
            {
                Debug.Log("Normal attack");
            }
            else if (context.interaction is HoldInteraction)
            {
                Debug.Log("Strong attack");
            }
        };

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Attack(InputAction.CallbackContext _context)
    {

    }

    void HeavyAttack()
    {

    }
}
