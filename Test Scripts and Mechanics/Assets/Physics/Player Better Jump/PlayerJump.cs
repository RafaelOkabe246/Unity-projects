using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 1. Faster fall
/// 2. Jump Height
/// 3. Max fall speed
/// 4. Jump Hang time
/// 5. Coyotte time
/// 6. Bonus air time
/// 7. Bonus peak time
/// 8. Jump force
/// 9. Grace timers
/// 10. Double jumps
/// </summary>
public class PlayerJump : MonoBehaviour
{
    public Rigidbody2D rig;

    public float jumpSpeed;
    public float fallMultipliyer;
    public float maxFallVelocity;

    public PlayerJumpControls inputActions;

    private void Awake()
    {
        inputActions = new PlayerJumpControls();
    }
    private void OnValidate()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        inputActions.Player.Jump.performed += Jump;
    }

    private void OnDisable()
    {
        inputActions.Player.Jump.performed -= Jump;
    }

    void TriggerJump(InputAction.CallbackContext context)
    {

    }

    void Jump(InputAction.CallbackContext context)
    {
    }

}
