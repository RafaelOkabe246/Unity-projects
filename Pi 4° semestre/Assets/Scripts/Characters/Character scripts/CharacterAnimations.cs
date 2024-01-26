using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimations : MonoBehaviour
{
    public Animator anim;
    public SpriteRenderer gfx;
    public RobotPieces robotPieces;

    public CharacterActions characterActions;

    [HideInInspector]
    public bool isMoving;

    private Animator screenEffectAnim;

    [Space(10)]

    [Header("After Image")]
    private Transform characterTrans;
    public float distanceBetweenImages;
    private bool isAfterImageOn = false;
    protected Vector3 lastImagePos;

    [Space(10)]

    [Space(5)]
    [Header("Behaviour state")]
    public SpriteRenderer behaviourIcon;

    protected virtual void OnEnable()
    {
        characterActions.OnChangeBehaviourState += UpdateBehaviourIcon;
        characterActions.OnTakeDamage += TakeDamage;
        characterActions.OnCheckCurrentAnimationState += OnCurrentAnimationState;
        characterActions.OnGetSpriteGFX += GetSpriteGFX;
        characterActions.OnFlipSprite += FlipSprite;
        characterActions.OnGetCharacterAnimations += GetCharacterAnimations;
        characterActions.OnKnockDown += KnockDown;
        characterActions.OnGetRobotPiecesScript += GetRobotPiecesScript;
    }

    protected virtual void OnDisable()
    {
        characterActions.OnChangeBehaviourState -= UpdateBehaviourIcon;
        characterActions.OnTakeDamage -= TakeDamage;
        characterActions.OnCheckCurrentAnimationState -= OnCurrentAnimationState;
        characterActions.OnGetSpriteGFX -= GetSpriteGFX;
        characterActions.OnFlipSprite -= FlipSprite;
        characterActions.OnGetCharacterAnimations -= GetCharacterAnimations;
        characterActions.OnKnockDown -= KnockDown;
        characterActions.OnGetRobotPiecesScript -= GetRobotPiecesScript;
    }

    private RobotPieces GetRobotPiecesScript()
    {
        return robotPieces;
    }

    private SpriteRenderer GetSpriteGFX()
    {
        return gfx;
    }

    private void UpdateBehaviourIcon(BehaviourState behaviourState)
    {
        //Debug.Log("CAllled");
        switch (behaviourState)
        {
            case (BehaviourState.Engage):
                behaviourIcon.color = Color.red;
                break;
            case (BehaviourState.EngageRange):
                behaviourIcon.color = Color.yellow;
                break;
            case (BehaviourState.Follow):
                behaviourIcon.color = Color.blue;
                break;
            case (BehaviourState.Idle):
                behaviourIcon.color = Color.white;
                break;
        }
    }

    private void Update()
    {
        CallAfterImage();
    }

    protected virtual void FlipSprite(bool i)
    {
        gfx.flipX = i;
    }

    void KnockDown(bool i)
    {
        anim.SetBool("isKnockdown", i);
    }

    private CharacterAnimations GetCharacterAnimations()
    {
        return this;
    }

    public void TriggerMoveAnimation() 
    {
        anim.SetTrigger("Move");
    }

    #region ANIMATOR AND AFTER IMAGE
    private float OnCurrentAnimationState()
    {
        return anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    private void TakeDamage()
    {
        anim.SetTrigger("takeDamage");
    }

    private void CallAfterImage()
    {
        if (!isMoving)
            return;

        if (Mathf.Abs(Vector3.Distance(transform.position, lastImagePos)) > distanceBetweenImages)
        {
            SpawnAfterImage();
        }
    }

    protected virtual void SpawnAfterImage() 
    {
        lastImagePos = transform.position;

        ObjectPooler.Instance.SpawnAfterImageFromPool("CharacterAfterImage", gfx.transform.position, transform,
            Quaternion.identity, gfx.sprite, gfx.flipX);
    }

    #endregion


}
