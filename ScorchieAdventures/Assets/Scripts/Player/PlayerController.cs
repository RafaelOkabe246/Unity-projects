using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handles every input from the player 
*/

public class PlayerController : MonoBehaviour
{

    public ActivatableUI pauseUI;
    private PlayerMovement playerMovement;
    private PlayerCollisionsManager playerCollisionsManager;
    public bool isInteracting { get; private set; }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCollisionsManager = GetComponent<PlayerCollisionsManager>();
    }

    private void OnEnable()
    {
        GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
        PlayerActions.OnCheckInteracting += CheckInteract;
    }

    private void OnDisable()
    {
        GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
        PlayerActions.OnCheckInteracting -= CheckInteract;
    }

    private void Update()
    {
        PressPause();
        PressInteract();
    }

    #region Interact_Functions_Methods
    bool CheckInteract()
    {
        return isInteracting;
    }

    private void OnInteractInput(bool inputState)
    {
        isInteracting = inputState;
        Debug.Log(inputState);
    }

    private void PressInteract()
    {
        //Is trying to interact with an object
        if (Input.GetKeyDown(KeyCode.E) && !isInteracting)
        {
            OnInteractInput(true);
            //Check if there is an object to interact
            if (playerCollisionsManager.interactiveObject != null && isInteracting)
            {
                playerCollisionsManager.interactiveObject.TriggerEvent();
                OnInteractInput(false);
            }
        }
    }

    #endregion

    private void PressPause() {
        if (Input.GetButtonDown("Pause"))
        {

            //Trigger pause
            if (pauseUI == null)
                pauseUI = GameObject.Find("PauseScreen").GetComponent<ActivatableUI>();

            ScreenStack.instance.AddScreenOntoStack(pauseUI);
        }
    }

    private void OnGameStateChanged(GameState newGameState) 
    {
        playerMovement.enabled = newGameState == GameState.Gameplay;
        playerCollisionsManager.enabled = newGameState == GameState.Gameplay;        
    }
}


