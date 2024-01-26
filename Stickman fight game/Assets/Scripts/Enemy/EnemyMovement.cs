using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyMovement : CharacterMovement
{
    public EnemyAnimationManager enemyAnimationManager;
    public EnemyCollision enemyCollision;
    public CharacterActions enemyActions;
    
    public override void AttackCollider()
    {
        Collider2D[] hitAttack = Physics2D.OverlapBoxAll(attackPoint.position, attackArea, 0f);

        //In same line
        Collider2D[] enemiesInLine = Physics2D.OverlapBoxAll(checkLinePoint.position, checkEnemyRange, 0f);

        //Enemies in same line and inside tha attack area
        IEnumerable<Collider2D> hitableEnemies = enemiesInLine.Select(n => n);

        foreach (Collider2D hit in hitAttack)
        {
            if (hitableEnemies.Contains(hit))
            {
                if (hit.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    //if (_comboHitStep < 4)
                      //  damageable.TakeNormalHit(1);
                    //else
                        damageable.TakeStrongHit(3);
                }
            }
        }
    }
    
    public override void OnAttackAction()
    {
        StopMove();
        enemyAnimationManager.CallAttack();
    }
}
