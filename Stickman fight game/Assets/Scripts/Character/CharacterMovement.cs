using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerControlsNamespace;
using System.Linq;
using UnityEngine.Events;

/*
 * Handles character's movement and fighting methods
*/
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    #region Components
    public Rigidbody2D rig { get; private set; }
    public CharacterActions characterActions;
    public CharacterAnimationManager characterAnimationManager { get; private set; }
    public CharacterCollision characterCollision { get; private set; }
    public Character character { get; private set; }
    #endregion

    [Space(10)]

    #region Stats_Parameters
    [Header("Movement")]
    public float speed;
    public float maxSpeed;
    private bool canTurn;
    public bool isJumping;

    [Header("Combat")]
    public bool isAttacking;
    public bool canMove;
    public Vector2 attackArea;
    public Transform checkLinePoint;
    public Vector2 checkEnemyRange;
    public Transform attackPoint;
    
    [Space(2)]

    public float _COMBO_MIN_DELAY = 0.1f;
    public int _COMBO_MAX_STEP = 2;
    public int _comboHitStep;
    [HideInInspector] public Coroutine _comboAttackResetCoroutine;

    [Space(2)]
    public int _airAttackCount;
    [HideInInspector] public Coroutine _airAttackResetCoroutine;

    [Space(2)]
    public float knockdownStrength;
    public float knockdownDelay;
    public UnityEvent OnBegin, OnDone;
    #endregion

    [Space(10)]

    #region State_Parameters
    //Bools
    public bool isLookingRight;
    #endregion

    [Space(10)]

    #region Check_Parameters
    [Header("Checks")]
    public Transform groundCheckPos;
    public float groundCheckSize;

    #endregion

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        characterAnimationManager = GetComponent<CharacterAnimationManager>();
    }
    
    void OnEnable()
    {
        characterActions.isJumping += CheckJump;
        characterActions.IsAttackig += CheckIsAttacking;
        characterActions.OnCheckCanMove += CanMove;
        characterActions.CheckCanTurn += CanTurn;
    }

    void OnDisable()
    {
        characterActions.isJumping -= CheckJump;
        characterActions.IsAttackig -= CheckIsAttacking;
        characterActions.OnCheckCanMove -= CanMove;
        characterActions.CheckCanTurn -= CanTurn;
    }


    private void Update()
    {
        if (characterActions.IsAttackig() || characterActions.isKnockdown())
        {
            StopMove();
            canMove = false;
            canTurn = false;
        }
        else
        {
            speed = maxSpeed;
            canMove = true;
            canTurn = true;
        }

    }


    #region CHECK_METHODS
    //Check if can do things
    public void CheckFacingDirection(Vector2 dir)
    {
        if (dir.x > 0 && !isLookingRight && characterActions.CheckCanTurn())
            Turn();
        else if (dir.x < 0 && isLookingRight && characterActions.CheckCanTurn())
            Turn();
    }

    private bool CheckJump()
    {
        return isJumping;
    }

    private bool CanTurn()
    {
        return canTurn;
    }

    private bool CanMove()
    {
        return canMove;
    }

    private bool CheckIsAttacking()
    {
        return isAttacking;
    }

    #endregion

    #region MOVE_METHODS
    public void Move(Vector2 direction)
    {
        if(canMove && !characterActions.isKnockdown())
        {
            rig.velocity = direction * speed * Time.fixedDeltaTime;
            characterActions.OnMove(true);
        }

    }

    private void Turn()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        //transform.Rotate(0,180f,0);

        isLookingRight = !isLookingRight;
    }

    public void StopMove()
    {
        characterActions.OnMove(false);
        rig.velocity = Vector2.zero;
    }

    #endregion

    #region BATTLE_METHODS

    public virtual void OnAttackAction()
    {
        //Reached max combo
        if (_comboHitStep == _COMBO_MAX_STEP)
        {
            isAttacking = false;
            return;
        }

        float t = characterAnimationManager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

        /*
        if(_airAttackCount == -1 && (t >= 0.6f && t <= 0.8f)) //Check if is jumping and is in time interval
        {
            isAttacking = true;
            if (_airAttackResetCoroutine != null)
                StopCoroutine(_airAttackResetCoroutine);

            _airAttackCount++;
            characterAnimationManager.anim.SetBool(characterAnimationManager._animAirAttackCountParamHash, false);
            characterAnimationManager.anim.SetInteger(characterAnimationManager._animAirAttackCountParamHash, _airAttackCount);

            if (isLookingRight)
            {
                rig.AddForce(new Vector2(1, -0.75f).normalized, ForceMode2D.Impulse);
            }
            else
            {
                rig.AddForce(new Vector2(-1, -0.75f).normalized, ForceMode2D.Impulse);
            }


            _airAttackResetCoroutine = StartCoroutine(_ResetAirAttack());
            return;
        }
        */

        if (_comboHitStep == -1 || (t >= 0.6f && t <= 0.8f)) //Checks if call during the time interval 
        {
            isAttacking = true;
            if (_comboAttackResetCoroutine != null)
                StopCoroutine(_comboAttackResetCoroutine);

            _comboHitStep++;
            characterAnimationManager.anim.SetBool(characterAnimationManager._animAttackComboStepParamHash, false);//Stop current animation 
            characterAnimationManager.anim.SetInteger(characterAnimationManager._animAttackComboStepParamHash, _comboHitStep);//Start next combo animation
            
            _comboAttackResetCoroutine = StartCoroutine(_ResetAttackingCombo());
        }

    }

    private IEnumerator _ResetAirAttack()
    {
        yield return new WaitForEndOfFrame();//Make more smooth by the frame rate
        yield return new WaitForSeconds(characterAnimationManager.anim.GetAnimatorTransitionInfo(0).duration);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => characterAnimationManager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f);
        _airAttackCount = -1;
        characterAnimationManager.anim.SetInteger(characterAnimationManager._animAirAttackCountParamHash, _airAttackCount);
        isAttacking = false;
    }

    private IEnumerator _ResetAttackingCombo()
    {
        yield return new WaitForEndOfFrame();//Make more smooth by the frame rate
        yield return new WaitForSeconds(characterAnimationManager.anim.GetAnimatorTransitionInfo(0).duration);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => characterAnimationManager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f);
        _comboHitStep = -1;
        characterAnimationManager.anim.SetInteger(characterAnimationManager._animAttackComboStepParamHash, _comboHitStep);
        isAttacking = false;
        //_move = _moveAction.ReadValue<Vector2>();
        //if (_move.sqrMagnitude > 0.01f && _running)
        //  _animator.SetBool(_animRunningParamHash, true);
    } 
    

    public virtual void AttackCollider()
    {
        //Attack area
        Collider2D[] hitAttack = Physics2D.OverlapBoxAll(attackPoint.position, attackArea, 0f);

        //In same line
        Collider2D[] enemiesInLine = Physics2D.OverlapBoxAll(checkLinePoint.position, checkEnemyRange, 0f);

        //Enemies in same line and inside tha attack area
        IEnumerable<Collider2D> hitableEnemies = enemiesInLine.Select(n => n);

        foreach (Collider2D hit in hitAttack)
        {
            if (hitableEnemies.Contains(hit) && hit.gameObject != this.gameObject)
            {
                if(hit.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    if (_comboHitStep < _COMBO_MAX_STEP)
                        damageable.TakeNormalHit(1);
                    else
                        damageable.TakeStrongHit(3);
                }
            }
        }
    }

    public void OnKnockdownFeedback()
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        int forceMultiplier = (isLookingRight) ? -1 : 1;

        Vector2 direction = transform.position.normalized * forceMultiplier;
        rig.AddForce(direction * knockdownStrength, ForceMode2D.Impulse);

        StopCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(knockdownDelay);
        rig.velocity = new Vector2(0,rig.velocity.y);
        OnDone?.Invoke();
    }

    public void ResetAttack()
    {
        isAttacking = false;
        _comboHitStep = 0;
    }

    #endregion


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(checkLinePoint.position, checkEnemyRange);
        Gizmos.DrawWireCube(attackPoint.position, attackArea);
    }

    
}
