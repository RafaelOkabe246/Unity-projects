using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Responsible for managing the player general inputs for a platformer game (Move, Jump and Dash)
*/

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

	#region COMPONENTS
	public Rigidbody2D rig { get; private set; }
	public PlayerAnimationManager animManager { get; private set; }

	public PlayerStatsManager statsManager;

	public PlayerCollisionsManager playerCol;

	public PlayerAudioSources playerAudio;
	#endregion

	#region STATE PARAMETERS
	public bool isFacingRight { get; private set; }
	public bool isJumping { get; private set; }
	public bool isDashing { get; private set; }
	public bool isGrounded { get; private set; }
	public bool isOnTopOfFloatingPlatform { get; private set; }
	[SerializeField] public float lastOnGroundTime { get; private set; }

	public bool canMove;

	//Jump
	private bool _isJumpCut;
	private bool _isJumpFalling;
	private int jumpCount;

	//Dash
	private int _dashesLeft;
	private bool _dashRefilling;
	public bool canDashRefill;
	private Vector2 _lastDashDir;
	private bool _isDashAttacking;

	#endregion

	#region INPUT PARAMETERS
	private Vector2 _moveInput;
	private float currentMoveInputX;

	public float lastPressedJumpTime { get; private set; }
	public float lastPressedDashTime { get; private set; }
	public float lastPressedSprintTime { get; private set; }
	#endregion

	#region CHECK PARAMETERS
	[Header("Checks")]
	[SerializeField] private Transform groundCheckPoint;
	[SerializeField] private Vector2 groundCheckSize = new Vector2(0.49f, 0.03f);
	#endregion

	#region LAYERS & TAGS
	[Header("Layers & Tags")]
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private LayerMask floatingPlatformLayer;
	#endregion

	private void Awake()
	{
		rig = GetComponent<Rigidbody2D>();
		animManager = GetComponent<PlayerAnimationManager>();
	}

	private void Start()
	{
		statsManager = PlayerStatsManager.instance;
		SetGravityScale(statsManager.stats.gravityScale);
		isFacingRight = true;
	}

	private void OnEnable()
	{
		GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;

		PlayerActions.OnCheckGround += CheckGround;
		PlayerActions.OnCheckJump += CheckJump;
		PlayerActions.OnCheckDash += CheckDash;
		PlayerActions.OnCheckPlayerDirection += CheckDirection;
		PlayerActions.OnCallFeedbackJump += CallFeedbackJump;
		PlayerActions.OnCancelDash += CancelDash;
		PlayerActions.OnTriggerDeath += DamageImpulseFeedback;

		canMove = true;
	}

	private void OnDisable()
	{
		GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;

		PlayerActions.OnCheckGround -= CheckGround;
		PlayerActions.OnCheckJump -= CheckJump;
		PlayerActions.OnCheckDash -= CheckDash;
		PlayerActions.OnCheckPlayerDirection -= CheckDirection;
		PlayerActions.OnCallFeedbackJump -= CallFeedbackJump;
		PlayerActions.OnCancelDash -= CancelDash;
		PlayerActions.OnTriggerDeath -= DamageImpulseFeedback;
	}

	#region FUNCS



	bool CheckGround()
	{
		return isGrounded;
	}

	bool CheckJump()
	{
		return isJumping;
	}

	bool CheckDash()
	{
		return isDashing;
	}
	#endregion

	void ApplyForceForward(Vector2 force)
	{
		if (isFacingRight)
			rig.AddForce(Vector2.right * force);
		else
			rig.AddForce(Vector2.left * force);
	}

	void ApplyForceUpward(Vector2 force)
	{
		rig.AddForce(Vector2.up * force);
	}

	bool CheckDirection()
	{
		return isFacingRight;
	}

	private void Update()
	{
		#region TIMERS
		lastOnGroundTime -= Time.deltaTime;

		lastPressedJumpTime -= Time.deltaTime;
		lastPressedDashTime -= Time.deltaTime;
		lastPressedSprintTime -= Time.deltaTime;
		#endregion

		#region ACTIONS
		PlayerActions.OnGround(isGrounded);

		if(!isOnTopOfFloatingPlatform)
			PlayerActions.SetVerticalVelocity(rig.velocity.y);
		else
			PlayerActions.SetVerticalVelocity(0);

		PlayerActions.OnJump(isJumping);
		//PlayerActions.OnDash(isDashing);

		if (isGrounded && _moveInput.x != 0)
			PlayerActions.CallMoveParticles(true);
		else
			PlayerActions.CallMoveParticles(false);

		if (isDashing)
			PlayerActions.CallDashParticles(true);
		else
			PlayerActions.CallDashParticles(false);

		#endregion

		#region INPUT HANDLER

		_moveInput.x = Input.GetAxisRaw("Horizontal");
		_moveInput.y = Input.GetAxisRaw("Vertical");

		if (_moveInput.x != currentMoveInputX)
		{
			currentMoveInputX = _moveInput.x;
			PlayerActions.OnTriggerMove();
		}


		if (!CanMove())
			_moveInput.x = 0;

		if (_moveInput.x != 0)
		{
			CheckDirectionToFace(_moveInput.x > 0);
			PlayerActions.OnMove(true);
		}
		else
		{
			PlayerActions.OnMove(false);
		}

		if (Input.GetButtonDown("Jump"))
		{
			OnJumpInput();
		}

		if (Input.GetButtonUp("Jump"))
		{
			OnJumpUpInput();
		}

		if (Input.GetButtonDown("Dash"))
		{
			OnDashInput();
		}

		if (Input.GetButtonUp("Dash"))
		{
			OnDashUpInput();
		}



		#endregion

		#region COLLISION CHECKS
		if (!isDashing && (!isJumping))
		{
			float colliderAngle = playerCol.slopeDownAngle;

			bool collidingWithGroundLayer = (Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, colliderAngle, groundLayer));
			bool collidingWithFloatingPlatform = (Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, colliderAngle, floatingPlatformLayer));

			bool collidingWithGround = (collidingWithGroundLayer || collidingWithFloatingPlatform);

			isOnTopOfFloatingPlatform = collidingWithFloatingPlatform;

			//Ground Check
			if ((collidingWithGround && !isJumping) || PlayerActions.OnCheckSlope())
			{
				if (lastOnGroundTime < -0.1f)
				{
					PlayerActions.CallJumpParticles();
					#region SOUND PLAY
					float landInt = Random.Range(0, 100);
					if (landInt <= 50)
						SoundsManager.instance.PlayAudio(AudiosReference.land0, AudioType.PLAYER, playerAudio.audioSource1);
					else if (landInt > 50 && landInt <= 100)
						SoundsManager.instance.PlayAudio(AudiosReference.land1, AudioType.PLAYER, playerAudio.audioSource1);
					#endregion
				}

				lastOnGroundTime = statsManager.stats.coyoteTime; //if it's colliding with the ground, sets the lastOnGroundTime to coyoteTime
				isGrounded = true;
				jumpCount = statsManager.stats.jumpsAmount;
			}
			else
			{
				isGrounded = false;
			}
		}
		#endregion

		#region JUMP CHECKS
		if (isJumping && rig.velocity.y < 0)
		{
			isJumping = false;

			_isJumpFalling = true;
		}

		if ((lastOnGroundTime > 0 || PlayerActions.OnCheckSlope()) && !isJumping)
		{
			_isJumpCut = false;

			if (!isJumping)
				_isJumpFalling = false;
		}

		//Jump
		if (CanJump() && lastPressedJumpTime > 0)
		{
			isJumping = true;
			_isJumpCut = false;
			_isJumpFalling = false;
			Jump();

			//animManager.startedJumping = true;
		}
		#endregion

		#region DASH CHECKS
		if (CanDash() && lastPressedDashTime > 0)
		{
			//Freeze game for split second. Adds juiciness and a bit of forgiveness over directional input
			Sleep(statsManager.stats.dashSleepTime);

			//If not direction pressed, dash forward
			if (_moveInput != Vector2.zero)
				_lastDashDir = _moveInput;
			else
				_lastDashDir = isFacingRight ? Vector2.right : Vector2.left;



			isDashing = true;
			isJumping = false;
			_isJumpCut = false;

			StartCoroutine(nameof(StartDash), _lastDashDir);
		}
		#endregion

		#region GRAVITY
		if (!_isDashAttacking && !PlayerActions.GetIsDead())
		{
			//Higher gravity if the player released the jump input or is falling
			if (rig.velocity.y < 0 && _moveInput.y < 0)
			{
				//Higher gravity if holding down
				SetGravityScale(statsManager.stats.gravityScale * statsManager.stats.fastFallGravityMult);
				//Caps maximum fall speed, so when falling over large distances we don't accelerate to insanely high speeds
				rig.velocity = new Vector2(rig.velocity.x, Mathf.Max(rig.velocity.y, -statsManager.stats.maxFastFallSpeed));
			}
			else if (_isJumpCut)
			{
				//Higher gravity if jump button released
				SetGravityScale(statsManager.stats.gravityScale * statsManager.stats.jumpCutGravityMult);
				rig.velocity = new Vector2(rig.velocity.x, Mathf.Max(rig.velocity.y, -statsManager.stats.maxFallSpeed));
			}
			else if ((isJumping || _isJumpFalling) && Mathf.Abs(rig.velocity.y) < statsManager.stats.jumpHangTimeThreshold)
			{
				SetGravityScale(statsManager.stats.gravityScale * statsManager.stats.jumpHangGravityMult);
			}
			else if (rig.velocity.y < 0)
			{
				//Higher gravity if falling
				SetGravityScale(statsManager.stats.gravityScale * statsManager.stats.fallGravityMult);
				//Caps maximum fall speed, so when falling over large distances we don't accelerate to insanely high speeds
				rig.velocity = new Vector2(rig.velocity.x, Mathf.Max(rig.velocity.y, -statsManager.stats.maxFallSpeed));
			}
			else
			{
				//Default gravity if standing on a platform or moving upwards
				SetGravityScale(statsManager.stats.gravityScale);
			}
		}
		else
		{
			//No gravity when dashing (returns to normal once initial dashAttack phase over) or attacking
			SetGravityScale(0);
			if (!PlayerActions.GetIsDead())
				rig.velocity = new Vector2(0f, 0f);
		}
		#endregion

		TryRefillDash();
	}

	private void FixedUpdate()
	{
		//Handle Move
		if (!isDashing)
		{
			Move(1);
		}
		else if (_isDashAttacking)
		{
			Move(statsManager.stats.dashEndMoveLerp);
		}
	}

	#region INPUT CALLBACKS
	//Methods which whandle input detected in Update()
	public void OnJumpInput()
	{
		lastPressedJumpTime = statsManager.stats.jumpInputBufferTime;
	}

	public void OnJumpUpInput()
	{
		if (CanJumpCut())
			_isJumpCut = true;
	}

	public void OnDashInput()
	{
		lastPressedDashTime = statsManager.stats.dashInputBufferTime;
	}

	public void OnDashUpInput()
	{
		if (statsManager.stats.hasDashCancel)
		{
			CancelDash();
		}
	}


	#endregion

	#region GENERAL METHODS
	public void SetGravityScale(float scale)
	{
		rig.gravityScale = scale;
	}

	private void Sleep(float duration)
	{
		StartCoroutine(nameof(PerformSleep), duration);
	}

	private IEnumerator PerformSleep(float duration)
	{
		Time.timeScale = 0;
		yield return new WaitForSecondsRealtime(duration); //Must be Realtime since timeScale will be 0 
		Time.timeScale = 1;
	}
	#endregion

	//MOVEMENT METHODS
	#region RUN METHODS
	private void Move(float lerpAmount)
	{
		//Calculate the direction we want to move in and our desired velocity
		float targetSpeed = _moveInput.x * statsManager.stats.moveMaxSpeed;

		targetSpeed = Mathf.Lerp(rig.velocity.x, targetSpeed, lerpAmount);

		#region Calculate AccelRate
		float accelRate;

		//Gets an acceleration value based on if the player is accelerating (includes turning) 
		//or trying to decelerate (stop). As well as applying a multiplier if we're air borne.
		if (lastOnGroundTime > 0)
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? statsManager.stats.moveAccelAmount : statsManager.stats.moveDeccelAmount;
		else
			accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? statsManager.stats.moveAccelAmount * statsManager.stats.accelInAir : statsManager.stats.moveDeccelAmount * statsManager.stats.deccelInAir;
		#endregion

		#region Add Bonus Jump Apex Acceleration
		//Increase are acceleration and maxSpeed when at the apex of their jump, makes the jump feel a bit more bouncy, responsive and natural
		if ((isJumping || _isJumpFalling) && Mathf.Abs(rig.velocity.y) < statsManager.stats.jumpHangTimeThreshold)
		{
			accelRate *= statsManager.stats.jumpHangAccelerationMult;
			targetSpeed *= statsManager.stats.jumpHangMaxSpeedMult;
		}
		#endregion

		#region Conserve Momentum
		//The player won't be slowed down if they are moving in their desired direction but at a greater speed than their maxSpeed
		if (statsManager.stats.doConserveMomentum && Mathf.Abs(rig.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rig.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && lastOnGroundTime < 0)
		{
			//Prevent any deceleration from happening, or in other words conserve are current momentum
			accelRate = 0;
		}
		#endregion

		//Calculate difference between current velocity and desired velocity
		float speedDif = targetSpeed - rig.velocity.x;
		//Calculate force along x-axis to apply to the player

		float movement = speedDif * accelRate;

		//Convert this to a vector and apply to rigidbody
		rig.AddForce(movement * Vector2.right, ForceMode2D.Force);

		/*
		 * AddForce() do:
		 * rig.velocity = new Vector2(rig.velocity.x + (Time.fixedDeltaTime  * speedDif * accelRate) / rig.mass, rig.velocity.y);
		 * Time.fixedDeltaTime is by default in Unity 0.02 seconds equal to 50 FixedUpdate() calls per second
		*/
	}

	private void Turn()
	{
		//stores scale and flips the player along the x axis, 
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;

		isFacingRight = !isFacingRight;
	}
	#endregion

	#region JUMP METHODS
	private void Jump()
	{
		PlayerActions.CallJumpParticles();

		//PlayerActions.OnTriggerJump();

		#region SOUND PLAY
		float jumpInt = Random.Range(0, 100);
		if (jumpInt <= 20)
			SoundsManager.instance.PlayAudio(AudiosReference.jump0, AudioType.PLAYER, playerAudio.audioSource1);
		else if (jumpInt > 20 && jumpInt <= 40)
			SoundsManager.instance.PlayAudio(AudiosReference.jump1, AudioType.PLAYER, playerAudio.audioSource1);
		else if (jumpInt > 40 && jumpInt <= 60)
			SoundsManager.instance.PlayAudio(AudiosReference.jump2, AudioType.PLAYER, playerAudio.audioSource1);
		else if (jumpInt > 60 && jumpInt <= 80)
			SoundsManager.instance.PlayAudio(AudiosReference.jump3, AudioType.PLAYER, playerAudio.audioSource1);
		else if (jumpInt > 80 && jumpInt <= 100)
			SoundsManager.instance.PlayAudio(AudiosReference.jump4, AudioType.PLAYER, playerAudio.audioSource1);
		#endregion

		PlayerActions.CallMoveParticles(false);

		//Ensures we can't call Jump multiple times from one press
		lastPressedJumpTime = 0;
		lastOnGroundTime = 0;


		#region Perform Jump
		//We increase the force applied if the player is falling
		//This means the player always feel like they are jumping the same amount
		float force = statsManager.stats.jumpForce;
		if (jumpCount < statsManager.stats.jumpsAmount)
			force /= statsManager.stats.doubleJumpDivisor;
		if (rig.velocity.y < 0)
			force -= rig.velocity.y;

		rig.AddForce(Vector2.up * force, ForceMode2D.Impulse);

		jumpCount--;
		#endregion
	}

	//Jump method called when we hit an enemy or an obstacle
	private void CallFeedbackJump(float j_force)
	{
		isJumping = true;
		_isJumpCut = false;
		_isJumpFalling = false;

		PlayerActions.OnTriggerJump();

		rig.velocity = new Vector2(rig.velocity.x, 0);
		//Refill the dash when hit the target
		statsManager.currentDashAmount += 1f;

		//Ensures we can't call Jump multiple times from one press
		lastPressedJumpTime = 0;
		lastOnGroundTime = 0;

		PlayerActions.OnSetSpriteRotationByDashDir(Vector2.zero);

		StartCoroutine(IgnoreCollisionForShortPeriod());

		#region Perform Jump
		//We increase the force applied if the player is falling
		//This means the player always feel like they are jumping the same amount
		float force = j_force;
		if (rig.velocity.y < 0)
			force -= rig.velocity.y;

		rig.AddForce(Vector2.up * force, ForceMode2D.Impulse);
		#endregion
	}

	private IEnumerator IgnoreCollisionForShortPeriod()
	{
		if (!isDashing)
			PlayerActions.IgnoreEnemyCollision(true);

		yield return new WaitForSeconds(0.25f);

		if (!isDashing)
			PlayerActions.IgnoreEnemyCollision(false);
	}

	#endregion

	#region DASH METHODS
	//Dash Coroutine
	private IEnumerator StartDash(Vector2 dir)
	{
		SoundsManager.instance.PlayAudio(AudiosReference.dashStart, AudioType.PLAYER, playerAudio.audioSource2);
		SoundsManager.instance.PlayAudio(AudiosReference.dashFlame, AudioType.PLAYER, playerAudio.audioSource3);

		PlayerActions.OnTriggerDash();

		PlayerActions.ActivateAfterImage(true);

		PlayerActions.OnSetSpriteRotationByDashDir(dir);

		PlayerActions.CallDashStartParticles();

		statsManager.currentDashAmount--;
		if (statsManager.currentDashAmount < 0)
			statsManager.currentDashAmount = 0;

		StartCoroutine(CameraController.CameraShake(3f, 3f, 0.25f));

		lastOnGroundTime = 0;
		lastPressedDashTime = 0;

		float startTime = Time.time;

		_dashesLeft--;
		_isDashAttacking = true;

		SetGravityScale(0);

		//We keep the player's velocity at the dash speed during the "attack" phase
		while (Time.time - startTime <= statsManager.stats.dashAttackTime)
		{
			rig.velocity = dir.normalized * statsManager.stats.dashSpeed;
			//Pauses the loop until the next frame, creating something of a Update loop. 
			//This is a cleaner implementation opposed to multiple timers and this coroutine approach is actually what is used in Celeste
			yield return null;
		}

		startTime = Time.time;

		_isDashAttacking = false;

		//Begins the "end" of the dash where we return some control to the player but still limit run acceleration (see Update() and Move())
		SetGravityScale(statsManager.stats.gravityScale);
		rig.velocity = statsManager.stats.dashEndSpeed * dir.normalized;

		while (Time.time - startTime <= statsManager.stats.dashEndTime)
		{
			yield return null;
		}

		//Dash over
		isDashing = false;
		PlayerActions.ActivateAfterImage(false);
		Physics2D.IgnoreLayerCollision(9, 7, false);
		PlayerActions.OnSetSpriteRotationByDashDir(Vector2.zero);

		canDashRefill = true;
	}

	public void TryRefillDash()
	{
		if (!isDashing && lastOnGroundTime > 0 && !_dashRefilling && canDashRefill && statsManager.currentDashAmount <= 0)
		{
			canDashRefill = false;
			StartCoroutine(nameof(RefillDash), 1);
		}
	}

	//Short period before the player is able to dash again
	private IEnumerator RefillDash(int amount)
	{
		_dashRefilling = true;
		yield return new WaitForSeconds(statsManager.stats.dashRefillTime);

		if (statsManager.currentDashAmount < statsManager.stats.dashAmount)
			statsManager.currentDashAmount += amount;

		_dashRefilling = false;
	}

	private void CancelDash() {
		StopCoroutine(nameof(StartDash));
		lastPressedDashTime = 0;
		isDashing = false;
		_isDashAttacking = false;
		PlayerActions.ActivateAfterImage(false);
		SetGravityScale(statsManager.stats.gravityScale);
	}
	#endregion

	#region DAMAGE METHODS
	private void DamageImpulseFeedback()
	{
		PlayerActions.SetPlayerCollision(false);
		SetGravityScale(0f);

		float forceMagnitudeX = 60f;
		float forceMagnitudeY = 1.75f;
		int forceMultiplier = (isFacingRight) ? -1 : 1;

		rig.AddForce(new Vector2(forceMultiplier * forceMagnitudeX, 1 * forceMagnitudeY), ForceMode2D.Impulse);
	}
	#endregion

	#region CHECK METHODS
	public void CheckDirectionToFace(bool isMovingRight)
	{
		if (isMovingRight != isFacingRight)
			Turn();
	}

	private bool CanMove()
	{
		return !isDashing && canMove;
	}

	private bool CanJump()
	{
		return ((lastOnGroundTime > 0 || PlayerActions.OnCheckSlope()) && !isJumping && !isDashing && (jumpCount > 0) && !PlayerActions.GetIsDead());
	}

	private bool CanJumpCut()
	{
		return isJumping && rig.velocity.y > 0;
	}

	private bool CanDash()
	{
		return statsManager.currentDashAmount > 0 && !PlayerActions.GetIsDead();
	}
	#endregion

	private void OnGameStateChanged(GameState newGameState)
	{
		if (newGameState != GameState.Gameplay)
		{
			PlayerActions.OnMove(false);
			PlayerActions.OnDash(false);
			PlayerActions.OnJump(false);
			CancelDash();
			rig.velocity = Vector2.zero;
			lastPressedJumpTime = 0;
		}
	}


	#region EDITOR METHODS
	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize);
		Gizmos.color = Color.blue;
	}
	#endregion

	#region SOUND METHODS

	public void PlayFootstepsSound() 
	{
		SoundsManager.instance.PlayAudio(AudiosReference.footstep, AudioType.PLAYER, playerAudio.audioSource4);
	}
    #endregion
}
