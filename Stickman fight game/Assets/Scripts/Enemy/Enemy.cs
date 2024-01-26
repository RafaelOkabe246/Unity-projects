using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 * Contains the behaviour of the enemies Ai, each on with a variety of actions that can be executed by different logics
 * 
 */
public class Enemy : Character
{
    public EnemyAnimationManager enemyAnimationManager;
    public EnemyMovement enemyMovement;
    public EnemyCollision enemyCollision;
    public CharacterActions enemyActions;

    public Player player;
    public float midRangeDistance;
    public float closeRangeDistance;

    #region EnemyTatics 
    public enum EnemyTatics
    {
        Engage,
        Wait,
        KeepDistance
    }
    public EnemyTatics enemyTatics;

    #endregion

    public AiBehaviour aiBehaviour;

    public Enemy(Player _player, int _damage, int _HP)
    {
        player = _player;
        Damage = _damage;
        HP = _HP;
    }



    private void Update()
    {
        aiBehaviour.ListenCommands(player);
        aiBehaviour.ExecuteCommands(player,closeRangeDistance,midRangeDistance);

    }

    /*
    protected virtual void ExecuteCommands()
    {
        switch (enemyTatics)
        {
            case (EnemyTatics.Engage):
                Vector3 dir = player.transform.position - transform.position;
                if (Vector2.Distance(transform.position, player.transform.position) > closeRangeDistance)
                {
                    enemyMovement.CheckFacingDirection(dir);
                    //Move to player
                    enemyMovement.Move(dir);
                }
                else
                {
                    //Player is in range
                    enemyMovement.OnAttackAction();
                }
                break;
            case (EnemyTatics.KeepDistance):
                Vector3 _dir = player.transform.position - transform.position;
                if (Vector2.Distance(transform.position, player.transform.position) < midRangeDistance)
                {
                    enemyMovement.CheckFacingDirection(-_dir);
                    //Distance from player
                    characterMovement.Move(-_dir);
                }
                break;
            case (EnemyTatics.Wait):
                characterMovement.StopMove();
                break;
        }
    }

    //Ai logic
    protected virtual void ListenCommands()
    {
        //Here is the method that contains the logic of the ai

        //Engage when player has not knockdown and retreat when it is
        
        if (!player.characterActions.isKnockdown())
        {
            ChangeEnemyTatic(EnemyTatics.Engage);
        }
        else
        {
            ChangeEnemyTatic(EnemyTatics.KeepDistance);
        }
    }

    */

    public void ChangeEnemyTatic(EnemyTatics newTatic)
    {
        enemyTatics = newTatic;
    }

}
