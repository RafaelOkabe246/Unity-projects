using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    public CharacterActions characterActions;

    [Header("Combat parameters")]
    [SerializeField] private bool isAttacking;
    [Range(0.1f, 2)]
    public float attackSpeed;

    [Header("Combo parameters")]
    private int animAttackComboStepParamHash;
    public float _COMBO_MIN_DELAY;
    public int _COMBO_MAX_STEP;
    private int _comboHitStep;
    [HideInInspector]
    public Coroutine _comboAttackResetCoroutine;


    private void Awake()
    {
        animAttackComboStepParamHash = Animator.StringToHash("AttackComboStep");
        _comboHitStep = -1;
        _comboAttackResetCoroutine = null;
    }


    private void OnEnable()
    {
        characterActions.OnCheckComboStep += CheckComboStep;
        characterActions.OnCheckIsAttacking += CheckIsAttacking;
        characterActions.OnTriggerAttack += OnAttack;
        characterActions.OnTriggerRangeAttack += OnRangeAttack;
        characterActions.OnHeal += Heal;
        characterActions.OnTryInteract += TryToInteract;
    }
    private void OnDisable()
    {
        characterActions.OnCheckComboStep -= CheckComboStep;
        characterActions.OnCheckIsAttacking -= CheckIsAttacking;
        characterActions.OnTriggerAttack -= OnAttack;
        characterActions.OnTriggerRangeAttack -= OnRangeAttack;
        characterActions.OnHeal -= Heal;
        characterActions.OnTryInteract -= TryToInteract;
    }

    #region FUNC_METHODS

    int CheckComboStep()
    {
        return _comboHitStep;
    }

    bool CheckIsAttacking()
    {
        return isAttacking;
    }
    #endregion

    void TryToInteract()
    {
        Collider2D[] hitInteractable = Physics2D.OverlapCircleAll(transform.position, 0.1f);

        foreach (var hit in hitInteractable)
        {
            hit.TryGetComponent<IInteractable>(out IInteractable interactable);
            if (interactable is not null)
            {
                //Triger some animation
                interactable.InteractionEvent();
            }
        }
    }

    #region Attack_methods
    void OnRangeAttack()
    {
        characterActions.OnGetCharacterAnimations().anim.SetFloat("attackSpeed", attackSpeed);

        characterActions.OnGetCharacterAnimations().anim.SetTrigger("RangeAttack");
    }

    void OnAttack()
    {
        characterActions.OnGetCharacterAnimations().anim.SetFloat("attackSpeed", attackSpeed);

        //Reached max combo
        if (_comboHitStep == _COMBO_MAX_STEP)
        {
            isAttacking = false;
            return;
        }

        float t = characterActions.OnGetCharacterAnimations().anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

        if (_comboHitStep == -1 || (t >= 0.7f && t <= 0.9f)) //Checks if call during the time interval 
        {
            isAttacking = true;
            if (_comboAttackResetCoroutine != null)
                StopCoroutine(_comboAttackResetCoroutine);

            _comboHitStep++;
            characterActions.OnGetCharacterAnimations().anim.SetBool(animAttackComboStepParamHash, false);//Stop current animation 
            characterActions.OnGetCharacterAnimations().anim.SetInteger(animAttackComboStepParamHash, _comboHitStep);//Start next combo animation

            _comboAttackResetCoroutine = StartCoroutine(ResetAttackingCombo());
        }
    }

    private IEnumerator ResetAttackingCombo()
    {
        yield return new WaitForEndOfFrame();//Make more smooth by the frame rate
        yield return new WaitForSeconds(characterActions.OnGetCharacterAnimations().anim.GetAnimatorTransitionInfo(0).duration);
        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => characterActions.OnGetCharacterAnimations().anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f);
        _comboHitStep = -1;
        characterActions.OnGetCharacterAnimations().anim.SetInteger(animAttackComboStepParamHash, _comboHitStep);
        isAttacking = false;
    }

    public ProjectileStore projectileStore;

    public void RangeAttack()
    {
        SoundManager.instance.PlayAudio(AudiosReference.lightAttack, AudioType.CHARACTER, null);

        Vector2 tileTargetPos = characterActions.AttackLocation();
        BattleTile tileTarget = characterActions.OnGetLevelGrid().ReturnBattleTile(tileTargetPos);

        //Search for component that stores the attack objects

        //Instantiate projétil
        Projectile bullet = Instantiate(projectileStore.projectile);
        bullet.Initialize(characterActions.OnGetLevelGrid(), tileTarget, Mathf.Abs(tileTargetPos.x) - Mathf.Abs(characterActions.OnCheckPositionInGrid().x));
    }


    public void Attack()
    {
        Vector2 tileTargetPos = characterActions.AttackLocation();
        BattleTile tileTarget = characterActions.OnGetLevelGrid().ReturnBattleTile(tileTargetPos);
        ObjectTile occupyingObject = tileTarget.GetOccupyingObject();

        if (occupyingObject && occupyingObject.gameObject.layer != 7)
        {
            occupyingObject.GetComponent<IDamageable>().TakeDamage(characterActions.OnCheckCharacter().statsComponent.lightHitDamage);
        }
        else
        {
            characterActions.OnPlayAudio(AudiosReference.missAttack, false);
        }

        if (characterActions.OnCanIgnoreTakeDamageAnim()) 
            gameObject.layer = 6;
    }
    #endregion

    public void Heal(int _healPoints)
    {
        int healingValue = characterActions.OnGetCharacterStatsComponent().HP += _healPoints;
        if (healingValue <= characterActions.OnGetCharacterStatsComponent().maxHP)
            characterActions.OnGetCharacterStatsComponent().HP += _healPoints;
        else if (healingValue > characterActions.OnGetCharacterStatsComponent().HP)
            characterActions.OnGetCharacterStatsComponent().HP = characterActions.OnGetCharacterStatsComponent().maxHP;
    }

}
