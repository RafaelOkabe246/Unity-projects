using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Handles character's animations
*/
[RequireComponent(typeof(Animator))]
public class CharacterAnimationManager : MonoBehaviour
{
    public CharacterMovement characterMovement;
    public CharacterCollision characterCollision;
    public CharacterActions characterActions;

    public Animator anim;
    public SpriteRenderer gfx;

    
    [Header("Combo parameters")]
    public int _animAttackComboStepParamHash;
    public int _animAirAttackCountParamHash;

    private void Awake()
    {
        _animAttackComboStepParamHash = Animator.StringToHash("AttackComboStep");
        characterMovement._comboHitStep = -1;
        characterMovement._comboAttackResetCoroutine = null;

        _animAirAttackCountParamHash = Animator.StringToHash("AirAttackCount");
        characterMovement._airAttackCount = -1;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        characterActions.OnKnockDown += OnKnockdown;
        characterActions.CallJump += OnJump; 
        characterActions.TakeDamage += OnTakeDamage;
        characterActions.OnMove += _OnMove; 
    }

    void OnDisable()
    {
        characterActions.OnKnockDown -= OnKnockdown;
        characterActions.CallJump -= OnJump;
        characterActions.TakeDamage -= OnTakeDamage;
        characterActions.OnMove -= _OnMove;
    }

    #region ANIMATOR
    void OnKnockdown(bool i)
    {
        anim.SetBool("isKnockdown", i);
    }
    void OnTakeDamage()
    {
        anim.SetTrigger("takeDamage");
    }
    void _OnMove(bool i)
    {
        anim.SetBool("isWalking", i);
    }
    void OnJump()
    {
        anim.SetTrigger("Jump");
    }


    void OnGetUp()
    {
        anim.SetTrigger("getUp");
    }
    #endregion
}
