using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * It has a view area, it will attack the player if it enters in the range view
*/

public class EnemyStatic : Enemy
{
    public LayerMask playerLayer;
    public Transform attackPoint;
    public GameObject attackAreaFeedbackPrefab;
    public Vector2 attackSize;
    [SerializeField] private bool isAttacking;
    [SerializeField] private bool isSearching;

    public float flipTimer;
    public float attackTimer;
    public float checkDistancePlayer;

    protected override void Start()
    {
        base.Start();
        Vector3 newAttackPointPos = attackPoint.localPosition;
        if (isLookingRight)
        {
            dir = 1;
        }
        else
        {
            dir = -1;
            newAttackPointPos.x = -newAttackPointPos.x;
        }

        attackPoint.localPosition = newAttackPointPos;
    }

    private void Update()
    {
       PlayerDetectionCheck();
    }
    void PlayerDetectionCheck()
    {
        RaycastHit2D check = Physics2D.Raycast(groundCheck.transform.position, Vector2.right,dir * checkDistancePlayer, playerLayer);

        //Ataque
        if(check && !isAttacking)
        {
            StartCoroutine(nameof(TriggerAttack));
        }
        //Flip
        else if(!check && !isAttacking && !isSearching)
        {
            StartCoroutine(nameof(FlipTimer));
        }

    }


    IEnumerator TriggerAttack()
    {
        StopCoroutine(nameof(FlipTimer));
        isAttacking = true;
        //Delay the attack
        yield return new WaitForSeconds(attackTimer);
        
        anim.SetTrigger("Attack");
    }

    IEnumerator FlipTimer()
    {
        isSearching = true;
        yield return new WaitForSeconds(flipTimer);
        dir = dir * -1;
        Flip();
        isSearching = false;
    }
    
    void Attack()
    {
        Collider2D hitPlayer = Physics2D.OverlapBox(attackPoint.position, attackSize, 90f, playerLayer);
        
        if (hitPlayer != null)
        {
            hitPlayer.GetComponent<PlayerStatsManager>().SpendHP(1);

            isSearching = false;
            isAttacking = false;
        }
        isSearching = false;
        isAttacking = false;
    }

    protected override void Play()
    {
        base.Play();
        isSearching = false;
        isAttacking = false;
    }



    private void OnDrawGizmos()
    {
        if (isAttacking)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackPoint.position, attackSize);
        }
        Gizmos.DrawWireCube(attackPoint.position, attackSize);

        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x + checkDistancePlayer * dir, groundCheck.position.y));
    }
}
