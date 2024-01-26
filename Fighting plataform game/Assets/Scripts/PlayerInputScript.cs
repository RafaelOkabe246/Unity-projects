using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    public float movementInputDirection;


    [SerializeField] private float interactAreaRadius;
    public LayerMask interactive;

    //Has the inputs player control
    private void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        #region Movement
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (playerMovement.groundCheck)
            {
                playerMovement.NormalJump();
            }
            else if (playerMovement.amountJumpsLeft > 0 && playerMovement.isTouchingWall)
            {
                playerMovement.WallJump();
            }
            else
            {
                playerMovement.jumpTimer = playerMovement.jumpTimerSet;
                playerMovement.isAttemtingToJump = true;
            }
        }

        //Player is touching wall
        if (Input.GetButtonDown("Horizontal") && playerMovement.isTouchingWall)
        {
            if(!playerMovement.groundCheck && movementInputDirection != playerMovement.facingDirection)
            {
                playerMovement.canMove = false;
                playerMovement.canFlip = false;
                playerMovement.turnTimer = playerMovement.turnTimerSet;
            }
        }

        if (playerMovement.turnTimer >= 0)
        {
            playerMovement.turnTimer -= Time.deltaTime;

            if(playerMovement.turnTimer < 0)
            {
                playerMovement.canMove = true;
                playerMovement.canFlip = true;
            }
        }

        //Avoids the player to climb a wall just jumping
        if (playerMovement.checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            playerMovement.checkJumpMultiplier = false;
            playerMovement.rig.velocity = new Vector2(playerMovement.rig.velocity.x, playerMovement.rig.velocity.y * playerMovement.jumpHeightMultiplier);
        }
        #endregion

        #region Interact
        
        //Try to interact
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Instantiate a collider that will detect the surroundings
            bool isDetecting = Physics2D.OverlapCircle(transform.position, interactAreaRadius, interactive);
            Debug.Log(isDetecting);
        }

        #endregion
    }

}
