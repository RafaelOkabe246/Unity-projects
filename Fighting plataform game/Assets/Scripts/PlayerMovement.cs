using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInputScript playerInput;

    public float movementInputDirection;


    [SerializeField] private float interactAreaRadius;
    public LayerMask interactive;

    [Header("Animations")]
    [SerializeField] Animator anim;

    [Header("Movement")]
    public Rigidbody2D rig;

    public int amountOfJumps = 1;
    public int amountJumpsLeft;
    public int facingDirection = 1;
    public int lastWallJumpingDirection;

    public bool canMove;
    public bool isRunning;
    [SerializeField] private bool isFacingRight = true;
    public bool canFlip;
    public bool isTouchingWall;
    [SerializeField] private bool isWallSliding;
    public bool groundCheck;
    public bool isAttemtingToJump;
    public bool checkJumpMultiplier;

    public float speed;
    public float movementForceInAir;

    public Transform groundCheckPos;

    public LayerMask plataform;


    [Header("Wall jump and ledge climb variables")]

    [Header("Wall variables")]
    public Transform wallCheck;

    public Vector2 walllHopDirection;
    public Vector2 wallJumpDirection;

    public bool canNormalJump;
    public bool canWallJump;

    private float wallJumpTimer;
    public float wallJumpTimerSet = 0.5f;

    public float turnTimer;
    public float turnTimerSet = 0.1f;

    public float wallHopForce;
    public float wallJumpForce;
    public float wallCheckDistante;
    public float wallSlideSpeed;
    public float airDragMultiplier = 0.95f;
    public float jumpHeightMultiplier = 0.5f;
    public float jumpTimer;
    public float jumpTimerSet = 0.15f;
    public float jumpForce = 5f;

    [Header("Ledge variables")]
    public Transform ledgeCheck;

    private Vector2 ledgePosBotton;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;

    public float ledgeClimbX_Offset1 = 0f;
    public float ledgeClimbY_Offset1 = 0f;
    public float ledgeClimbX_Offset2 = 0f;
    public float ledgeClimbY_Offset2 = 0f;

    //Climb bools
    private bool canClimbLedge = false;
    private bool ledgeDetected;
    private bool hasWallJumped;
    public bool isTouchingLedge;


    delegate void JumpDelegate();
    JumpDelegate jumpDelegate;

    delegate void WallLedgeDelegate();
    WallLedgeDelegate wallLedgeDelegate;

    private void Start()
    {
        jumpDelegate += CheckIfCanJump;
        jumpDelegate += CheckJump;

        wallLedgeDelegate += CheckLedgeClimb;
        wallLedgeDelegate += CheckIfWallSliding;

        walllHopDirection.Normalize();
        wallJumpDirection.Normalize();
        amountJumpsLeft = amountOfJumps;
    }

    private void FixedUpdate()
    {
        Movement();
        CheckingSurroundings();
    }


    private void Update()
    {
        CheckInput();
        wallLedgeDelegate();
        MoveDir();
        jumpDelegate();
        UpdateAnimations();
    }
    void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if (groundCheck)
            {
                NormalJump();
            }
            else if (amountJumpsLeft > 0 && isTouchingWall)
            {
                WallJump();
            }
            else
            {
                jumpTimer = jumpTimerSet;
                isAttemtingToJump = true;
            }
        }

        //Player is touching wall
        if (Input.GetButtonDown("Horizontal") && isTouchingWall)
        {
            if (!groundCheck && movementInputDirection != facingDirection)
            {
                canMove = false;
                canFlip = false;
                turnTimer = turnTimerSet;
            }
        }

        if (turnTimer >= 0)
        {
            turnTimer -= Time.deltaTime;

            if (turnTimer < 0)
            {
                canMove = true;
                canFlip = true;
            }
        }

        //Avoids the player to climb a wall just jumping
        if (checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            checkJumpMultiplier = false;
            rig.velocity = new Vector2(rig.velocity.x, rig.velocity.y * jumpHeightMultiplier);
        }
    }
    void CheckingSurroundings()
    {
        groundCheck = Physics2D.OverlapCircle(groundCheckPos.position, 0.1f);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistante, plataform);
        isTouchingLedge = Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistante, plataform);

        if (isTouchingWall && !isTouchingLedge && !ledgeDetected)
        {
            ledgeDetected = true;
            ledgePosBotton = wallCheck.position;
        }
    }

    #region Movement
    void Movement()
    {
        if (!groundCheck && !isWallSliding && playerInput.movementInputDirection == 0 )
        {
            //Moving air
            rig.velocity = new Vector2(rig.velocity.x * airDragMultiplier, rig.velocity.y);

        }
        else
        {
            //Moving ground
            rig.velocity = new Vector2(speed * playerInput.movementInputDirection, rig.velocity.y);
            
        }
        if (isWallSliding)
        {
            if (rig.velocity.y < -wallSlideSpeed)
            {
                rig.velocity = new Vector2(rig.velocity.x, -wallSlideSpeed);
            }
        }


    }
    void MoveDir()
    {
        if (isFacingRight && playerInput.movementInputDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && playerInput.movementInputDirection > 0)
        {
            Flip();
        }

        if (rig.velocity.x != 0 && groundCheck)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }


    void Flip()
    {
        if (!isWallSliding && canFlip)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0, 180f, 0);
        }

    }

    #endregion

    #region Jump

    void CheckIfCanJump()
    {
        if(groundCheck && rig.velocity.y <= 0.01f)        
        {
            amountJumpsLeft = amountOfJumps;
        }

        if (isTouchingWall)
        {
            canWallJump = true;
        }

        if(amountJumpsLeft <= 0)
        {
            canNormalJump = false;
        }
        else
        {
            canNormalJump = true;
        }
    }

    public void CheckJump()
    {
        if(jumpTimer > 0)
        {
            //wall jump
            if(!groundCheck && isTouchingWall && playerInput.movementInputDirection != 0 && playerInput.movementInputDirection != facingDirection)
            {
                WallJump();
            }
            else if (groundCheck)
            {
                NormalJump();
            }
        }

        if(isAttemtingToJump)
        {
            jumpTimer -= Time.deltaTime;
        }

        //Time to wall jump
        if(wallJumpTimer > 0)
        {
            if(hasWallJumped && playerInput.movementInputDirection == -lastWallJumpingDirection)
            {
                rig.velocity = new Vector2(rig.velocity.x, 0f);
                hasWallJumped = false;
            }
            else if (wallJumpTimer <= 0)
            {
                hasWallJumped = false;
            }
            else
            {
                wallJumpTimer -= Time.deltaTime;
            }
        }
    }

    public void NormalJump()
    {
        if (canNormalJump)
        {
            rig.velocity = new Vector2(rig.velocity.x, jumpForce);
            amountJumpsLeft--;
            jumpTimer = 0;
            isAttemtingToJump = false;
            checkJumpMultiplier = true;

        }
    }

    public void WallJump()
    {
        if (canWallJump) //Wall jump
        {
            rig.velocity = new Vector2(rig.velocity.x, 0.0f);
            isWallSliding = false;
            amountJumpsLeft = amountOfJumps;
            amountJumpsLeft--;
            Vector2 forceToAdd = new Vector2(wallJumpForce * wallJumpDirection.x * playerInput.movementInputDirection, wallJumpForce * wallJumpDirection.y);
            rig.AddForce(forceToAdd, ForceMode2D.Impulse);
            jumpTimer = 0;
            isAttemtingToJump = false;
            checkJumpMultiplier = true;
            turnTimer = 0;
            canMove = true;
            canFlip = true;
            hasWallJumped = true;
            wallJumpTimer = wallJumpTimerSet;
            lastWallJumpingDirection = -facingDirection;
        }
    }

    #endregion

    #region climb and wall

    void CheckIfWallSliding()
    {
        if (isTouchingWall && playerInput.movementInputDirection == facingDirection  && rig.velocity.y < 0 && !canClimbLedge)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }


    void CheckLedgeClimb()
    {
        if(ledgeDetected && !canClimbLedge)
        {
            canClimbLedge = true;

            if (isFacingRight)
            {
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBotton.x + wallCheckDistante) - ledgeClimbX_Offset1, Mathf.Floor(ledgePosBotton.y) + ledgeClimbY_Offset1);
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBotton.x + wallCheckDistante) + ledgeClimbX_Offset2, Mathf.Floor(ledgePosBotton.y) + ledgeClimbY_Offset2);
            }
            else
            {
                ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBotton.x - wallCheckDistante) + ledgeClimbX_Offset1, Mathf.Floor(ledgePosBotton.y) + ledgeClimbY_Offset1);
                ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBotton.x - wallCheckDistante) - ledgeClimbX_Offset2, Mathf.Floor(ledgePosBotton.y) + ledgeClimbY_Offset2);
            }

            canMove = false;
            canFlip = false;

            anim.SetBool("canClimbLedge", canClimbLedge);
        }

        if (canClimbLedge)
        {
            transform.position = ledgePos1;
        }
    }

    public void FinishLedgeClimb()
    {
        canClimbLedge = false;
        transform.position = ledgePos2;
        canMove = true;
        canFlip = true;
        ledgeDetected = false;
        anim.SetBool("canClimbLedge", canClimbLedge);
    }


    #endregion

    #region Animations
    void UpdateAnimations()
    {
        anim.SetBool("isGrounded", groundCheck);
        anim.SetBool("isRunning", isRunning);
        anim.SetFloat("velocityY", rig.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(ledgePos1, ledgePos2);

        //Wall check
        //Gizmos.DrawLine( wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistante * facingDirection, wallCheck.position.y));
        //Gizmos.DrawLine(ledgePos1, ledgePos2);
    }
}
