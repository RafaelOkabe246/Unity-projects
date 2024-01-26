using UnityEngine;

/*
 * Scriptable Object which holds all the player's parameters
*/

[CreateAssetMenu(menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
	[Header("Health")]
	public float maxHP;

	[Space(20)]

	[Header("Gravity")]
	[Tooltip("Downwards force (gravity) needed for the desired jumpHeight and jumpTimeToApex.")]
	[HideInInspector] public float gravityStrength;
	[Tooltip("Strength of the player's gravity as a multiplier of gravity (set in ProjectSettings/Physics2D).")]
	[HideInInspector] public float gravityScale;

	[Space(5)]
	[Tooltip("Multiplier to the player's gravityScale when falling.")]
	public float fallGravityMult;
	[Tooltip("Maximum fall speed (terminal velocity) of the player when falling.")]
	public float maxFallSpeed;
	[Space(5)]
	[Tooltip("Larger multiplier to the player's gravityScale when they are falling and a downwards input is pressed. Seen in games such as Celeste, lets the player fall extra fast if they wish.")]
	public float fastFallGravityMult;
	[Tooltip("Maximum fall speed(terminal velocity) of the player when performing a faster fall.")]
	public float maxFastFallSpeed;

	[Space(20)]

	[Header("Movement")]
	[Tooltip("Target speed we want the player to reach.")]
	public float moveMaxSpeed;
	[Tooltip("The speed at which our player accelerates to max speed, can be set to moveMaxSpeed for instant acceleration down to 0 for none at all")]
	public float moveAcceleration;
	[Tooltip("The actual force (multiplied with speedDiff) applied to the player.")]
	[HideInInspector] public float moveAccelAmount;
	[Tooltip("The speed at which our player decelerates from their current speed, can be set to moveMaxSpeed for instant deceleration down to 0 for none at all")]
	public float moveDecceleration;
	[Tooltip("Actual force (multiplied with speedDiff) applied to the player.")]
	[HideInInspector] public float moveDeccelAmount;
	[Space(5)]
	[Tooltip("Multiplier applied to acceleration rate when airborne.")]
	[Range(0f, 1)] public float accelInAir;
	[Tooltip("Multiplier applied to acceleration rate when airborne.")]
	[Range(0f, 1)] public float deccelInAir;
	[Space(5)]
	[Tooltip("Momentum is recommended on games like Celeste, Sonic or Super Meat Boy. However it's not recommended in action games with precise movements such as Hollow Knight, Megaman or Ori")]
	public bool doConserveMomentum = true;

	[Space(20)]
	[Header("Jump")]
	[Tooltip("Height of the player's jump")]
	public float jumpHeight;
	[Tooltip("Time between applying the jump force and reaching the desired jump height. These values also control the player's gravity and jump force.")]
	public float jumpTimeToApex;
	[Tooltip("The actual force applied (upwards) to the player when they jump.")]
	[HideInInspector] public float jumpForce;
	public int jumpsAmount;
	public float doubleJumpDivisor;

	[Header("Jump Multipliers")]
	[Tooltip("Multiplier to increase gravity if the player releases thje jump button while still jumping")]
	public float jumpCutGravityMult;
	[Tooltip("Reduces gravity while close to the apex (desired max height) of the jump")]
	[Range(0f, 1)] public float jumpHangGravityMult;
	[Tooltip("Speeds (close to 0) where the player will experience extra 'jump hang'. The player's velocity.y is closest to 0 at the jump's apex (think of the gradient of a parabola or quadratic function)")]
	public float jumpHangTimeThreshold;
	[Space(0.5f)]
	public float jumpHangAccelerationMult;
	public float jumpHangMaxSpeedMult;

	[Space(20)]

	[Header("Dash")]
	public float dashAmount;
	[Tooltip("The dash movement speed")]
	public float dashSpeed;
	[Tooltip("Duration for which the game freezes when we press dash but before we read directional input and apply a force.")]
	public float dashSleepTime;
	[Space(5)]
	public float dashAttackTime;
	[Space(5)]
	[Tooltip("Time after you finish the inital drag phase, smoothing the transition back to idle (or any standard state).")]
	public float dashEndTime;
	[Tooltip("Slows down player, makes dash feel more responsive.")]
	public Vector2 dashEndSpeed;
	[Tooltip("Slows the affect of player movement while dashing.")]
	[Range(0f, 1f)] public float dashEndMoveLerp;
	[Space(5)]
	public float dashRefillTime;
	[Space(5)]
	[Range(0.01f, 0.5f)] public float dashInputBufferTime;
	public bool hasDashCancel;

	[Space(10)]

	[Header("Dash Ripple Effect")]
	public float dashRefractionStrength = 0.6f;
	public float dashReflectionStrength = 0.7f;
	public float dashWaveSpeed = 1.3f;
	public float dashDropInterval = 0.5f;
	public Color dashRefractionColor;

	[Space(20)]

	[Header("Assists")]
	[Tooltip("Grace period after falling off a platform, where the player can still jump")]
	[Range(0.01f, 0.5f)] public float coyoteTime;
	[Tooltip("Grace period after pressing jump where a jump will be automatically performed once the requirements (being on the ground) are met.")]
	[Range(0.01f, 0.5f)] public float jumpInputBufferTime;

	private void OnValidate()
	{
		//Calculate gravity strength using the formula (gravity = 2 * jumpHeight / timeToJumpApex^2) 
		gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);

		//Calculate the rigidbody's gravity scale (ie: gravity strength relative to unity's gravity value, see project settings/Physics2D)
		gravityScale = gravityStrength / Physics2D.gravity.y;

		//Calculate are move acceleration & deceleration forces using formula: amount = ((1 / Time.fixedDeltaTime) * acceleration) / moveMaxSpeed
		moveAccelAmount = (50 * moveAcceleration) / moveMaxSpeed;
		moveDeccelAmount = (50 * moveDecceleration) / moveMaxSpeed;

		//Calculate jumpForce using the formula (initialJumpVelocity = gravity * timeToJumpApex)
		jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;

		#region Variable Ranges
		moveAcceleration = Mathf.Clamp(moveAcceleration, 0.01f, moveMaxSpeed);
		moveDecceleration = Mathf.Clamp(moveDecceleration, 0.01f, moveMaxSpeed);
		#endregion
	}
}


