using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Handles the player's colliders and collisions events, such as the Damage System
*/
[RequireComponent(typeof(CapsuleCollider2D))]
public class CharacterCollision : MonoBehaviour, IDamageable
{
    public Character character;
    public CharacterActions characterActions;
    public CharacterAnimationManager characterAnimationManager;
    public CharacterMovement characterMovement;
    public bool canTakeDamage;
    public bool isKnockdown;
    public float knockdownTime;

    [HideInInspector] public Coroutine _knockdownResetCoroutine;

    private void Start()
    {
        character = GetComponent<Character>();
    }

    void OnEnable()
    {
        characterActions.OnKnockDown += SetKnockdown;
        characterActions.isInvencible += CheckIsInvencible;
        characterActions.isKnockdown += CheckIsKnockdown;
    }

    void OnDisable()
    {
        characterActions.OnKnockDown -= SetKnockdown;
        characterActions.isInvencible -= CheckIsInvencible;
        characterActions.isKnockdown -= CheckIsKnockdown;
    }

    protected virtual IEnumerator KnockdownTimer()
    {
        yield return new WaitForSeconds(knockdownTime);
        characterActions.OnKnockDown(false);
    }
    /*
    protected virtual void KnockdownTimer()
    {
        float t = characterAnimationManager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if(t >= 1f) //Checks if ends the animation
        {
            isKnockdown = false;
        }
    }
    */

    void SetKnockdown(bool i)
    {
        isKnockdown = i;
    }


    private bool CheckIsKnockdown()
    {
        return isKnockdown;
    }

    private bool CheckIsInvencible()
    {
        return canTakeDamage;
    }

    public virtual void ReceiveAttack(int damage)
    {
        if(canTakeDamage)
            TakeNormalHit(damage);
    }

    public void TakeStrongHit(int trueDamage)
    {
        Debug.Log("Took strong damage");
        character.HP -= trueDamage;
        if (character.HP < 0)
        {
            //Die
            Debug.Log("is dead");
        }
        else
        {
            characterActions.OnKnockDown(true);
            //
            //characterMovement.OnKnockdownFeedback();
            StartCoroutine(KnockdownTimer());
        }
    }

    public void TakeNormalHit(int trueDamage)
    {
        character.HP -= trueDamage;
        if(character.HP < 0)
        {
            //Die
            Debug.Log("is dead");
        }
        else
        {
            character.characterAnimationManager.anim.SetTrigger("takeDamage");
            characterActions.TakeDamage();
        }
    }

}
