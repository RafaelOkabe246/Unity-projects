using UnityEngine;

/*
 * Script to handle all player's animations and vfx (after images, shaders and particles)
*/
[RequireComponent(typeof(Animator))]
public class PlayerAnimationManager : MonoBehaviour
{
    private Animator anim;
    public SpriteRenderer gfx;
    public PlayerMovement playerMovement;
    public PlayerCollisionsManager playerCollisionsManager;

    private bool isMoving;

    private Animator screenEffectAnim;

    public Color dashRefillColor;
    public float dashRefillColorSpeed;

    [Space(10)]

    [Header("After Image")]
    private Transform playerTrans;
    public float distanceBetweenImages;
    private bool isAfterImageOn = false;
    private Vector3 lastImagePos;

    [Space(10)]

    [Header("Particles Systems")]
    private bool moveParticleOn;
    private bool dashParticleOn;
    public ParticleSystem moveParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem dashParticle;
    public ParticleSystem dashHitParticle;
    public ParticleSystem dashStartParticle;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        playerTrans = GetComponent<Transform>();
        lastImagePos = playerTrans.position;

        screenEffectAnim = GameObject.Find("ScreenEffects").GetComponent<Animator>();
    }

    private void OnEnable()
    {
        #region ANIMATOR EVENTS
        PlayerActions.OnTurn += OnTurnPlayer;

        PlayerActions.OnGround += OnPlayerGrounded;

        PlayerActions.OnMove += OnPlayerMove;
        PlayerActions.ActivateAfterImage += OnAfterImage;

        PlayerActions.OnJump += OnPlayerJump;
        PlayerActions.SetVerticalVelocity += OnSetVerticalVelocity;

        PlayerActions.OnDash += OnPlayerDash;
        PlayerActions.OnSetSpriteRotationByDashDir += SetSpriteRotationByDashDir;

        PlayerActions.OnCheckSpriteFlip += OnCheckSpriteFlip;

        PlayerActions.OnTriggerMove += TriggerMove;
        PlayerActions.OnTriggerJump += TriggerJump;
        PlayerActions.OnTriggerDash += TriggerDash;

        PlayerActions.IsPlayerOnSlope += IsPlayerOnSlope;

        PlayerActions.OnTriggerDeath += TriggerPlayerDeath;
        #endregion

        #region PARTICLES
        PlayerActions.CallMoveParticles += PlayMoveParticle;
        PlayerActions.CallJumpParticles += PlayJumpParticle;
        PlayerActions.CallDashParticles += PlayDashParticle;
        PlayerActions.CallDashHitParticles += PlayDashHitParticle;
        PlayerActions.CallDashStartParticles += PlayDashStartParticle;
        #endregion

        #region DELEGATES
        StageBlocksHandler.OnStageBlockTransitionStarted += OnStageBlockTransitionStarted;
        StageBlocksHandler.OnStageBlockTransitionEnded += OnStageBlockTransitionEnded;
        #endregion

        #region SCREEN EFFECTS
        PlayerActions.CallDashHitScreenEffect += CallDashHitScreenEffect;
        #endregion
    }

    private void OnDisable()
    {
        #region ANIMATOR EVENTS
        PlayerActions.OnTurn -= OnTurnPlayer;

        PlayerActions.OnGround -= OnPlayerGrounded;

        PlayerActions.OnMove -= OnPlayerMove;
        PlayerActions.ActivateAfterImage -= OnAfterImage;

        PlayerActions.OnJump -= OnPlayerJump;
        PlayerActions.SetVerticalVelocity -= OnSetVerticalVelocity;

        PlayerActions.OnDash -= OnPlayerDash;
        PlayerActions.OnSetSpriteRotationByDashDir -= SetSpriteRotationByDashDir;

        PlayerActions.OnCheckSpriteFlip -= OnCheckSpriteFlip;

        PlayerActions.OnTriggerMove -= TriggerMove;
        PlayerActions.OnTriggerJump -= TriggerJump;
        PlayerActions.OnTriggerDash -= TriggerDash;

        PlayerActions.IsPlayerOnSlope -= IsPlayerOnSlope;

        PlayerActions.OnTriggerDeath -= TriggerPlayerDeath;
        #endregion

        #region PARTICLES
        PlayerActions.CallMoveParticles -= PlayMoveParticle;
        PlayerActions.CallJumpParticles -= PlayJumpParticle;
        PlayerActions.CallDashParticles -= PlayDashParticle;
        PlayerActions.CallDashHitParticles -= PlayDashHitParticle;
        PlayerActions.CallDashStartParticles -= PlayDashStartParticle;
        #endregion

        #region DELEGATES
        StageBlocksHandler.OnStageBlockTransitionStarted -= OnStageBlockTransitionStarted;
        StageBlocksHandler.OnStageBlockTransitionEnded -= OnStageBlockTransitionEnded;
        #endregion

        #region SCREEN EFFECTS
        PlayerActions.CallDashHitScreenEffect -= CallDashHitScreenEffect;
        #endregion
    }

    void Update()
    {
        if (isAfterImageOn)
        {
            if (Mathf.Abs(Vector3.Distance(playerTrans.position, lastImagePos)) > distanceBetweenImages)
            {
                CallAfterImage();
            }
        }

        if (playerMovement.statsManager.currentDashAmount <= 0)
        {
            gfx.color = Color.Lerp(Color.white, dashRefillColor, dashRefillColorSpeed * Time.deltaTime);
        }
        else
        {
            gfx.color = Color.white;
        }
    }

    #region ANIMATOR AND AFTER IMAGE
    void OnTurnPlayer(bool flip)
    {
        gfx.flipX = flip;
    }

    void OnPlayerGrounded(bool grounded)
    {
        anim.SetBool("isGrounded", grounded);
    }

    void OnPlayerMove(bool moving)
    {
        anim.SetBool("isMoving", moving);
    }

    void OnAfterImage(bool boolean)
    {
        isAfterImageOn = boolean;
    }

    void OnPlayerJump(bool isJumping)
    {
        anim.SetBool("isJumping", isJumping);
    }

    void OnPlayerDash(bool isDashing)
    {
        anim.SetBool("isDashing", isDashing);
    }


    private void SetSpriteRotationByDashDir(Vector2 dashDir) 
    {
        if (dashDir == Vector2.zero)
        {
            SetSpriteRotation(0f);
            return;
        }

        float dirX = Mathf.Abs(dashDir.x);

        bool upRight = (dirX == 1 && dashDir.y == 1);
        if (upRight) 
        {
            float rotation = (transform.localScale.x > 0) ? 45f : -45f;
            SetSpriteRotation(rotation);
            return;
        }

        bool up = (dashDir.y == 1) && dirX == 0;
        if (up) 
        {
            float rotation = (transform.localScale.x > 0) ? 90f : -90f;
            SetSpriteRotation(rotation);
            return;
        }

        bool downRight = (dirX == 1 && dashDir.y == -1);
        if (downRight)
        {
            float rotation = (transform.localScale.x > 0) ? -45f : 45f;
            SetSpriteRotation(rotation);
            return;
        }
        bool down = (dashDir.y == -1) && dirX == 0;
        if (down) 
        {
            float rotation = (transform.localScale.x > 0) ? -90 : 90f;
            SetSpriteRotation(rotation);
            return;
        }
    }

    private void SetSpriteRotation(float rotationZ) 
    {
        gfx.transform.rotation = Quaternion.Euler(new Vector3(gfx.transform.rotation.x, gfx.transform.rotation.y, rotationZ));
        playerCollisionsManager.dashCollisionPoint.rotation = gfx.transform.rotation;
        
    }

    void OnSetVerticalVelocity(float yVelocity)
    {
        anim.SetFloat("VerticalVelocity", yVelocity);
    }

    void IsPlayerOnSlope(bool isOnSlope)
    {
        anim.SetBool("isOnSlope", isOnSlope);
    }

    bool OnCheckSpriteFlip()
    {
        return gfx.flipX;
    }

    void CallAfterImage()
    {
        lastImagePos = playerTrans.position;

        ObjectPooler.Instance.SpawnFromPool("AfterImage", lastImagePos, Quaternion.identity);
    }

    void TriggerPlayerDeath()
    {
        anim.SetTrigger("dead");
    }

    void TriggerMove()
    {
        anim.SetTrigger("move");
    }

    void TriggerJump()
    {
        anim.SetTrigger("jump");
    }

    void TriggerDash()
    {
        anim.SetTrigger("dash");
    }
    #endregion

    #region PARTICLES
    private void PlayMoveParticle(bool result)
    {
        if (!moveParticleOn && result)
        {
            moveParticleOn = true;
            moveParticle.Play();
        }
        else if (moveParticleOn && !result)
        {
            moveParticleOn = false;
            moveParticle.Stop();
        }

    }

    private void PlayJumpParticle()
    {
        jumpParticle.Play();
    }

    private void PlayDashParticle(bool result)
    {
        if (!dashParticleOn && result)
        {
            dashParticleOn = true;
            dashParticle.Play();
        }
        else if (dashParticleOn && !result)
        {
            dashParticleOn = false;
            dashParticle.Stop();
        }
    }

    private void PlayDashHitParticle(Vector2 positionToPlay) 
    {
        dashHitParticle.transform.position = positionToPlay;
        dashHitParticle.Play();
    }

    private void PlayDashStartParticle() 
    {
        dashStartParticle.Play();
    }
    #endregion

    private void CallDashHitScreenEffect() 
    {
        screenEffectAnim.SetTrigger("Hit");
    }

    private void OnStageBlockTransitionStarted() 
    {
        anim.speed = 0f;
        SetSpriteRotationByDashDir(Vector2.zero);
        isAfterImageOn = false;
    }

    private void OnStageBlockTransitionEnded() 
    {
        anim.speed = 1f;
        playerMovement.canDashRefill = true;
    }
}
