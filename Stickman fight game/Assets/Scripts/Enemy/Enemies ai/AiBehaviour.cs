using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBehaviour : MonoBehaviour
{
    public Enemy self;
    #region EnemyStates;
    public enum EnemyState
    {
        knockdown,
        sawPlayer,
        fleeing,
        idle
    }
    public EnemyState currentEnemyState;

    #endregion

    #region EnemyTatics 
    public enum EnemyTatics
    {
        Engage,
        Wait,
        KeepDistance
    }
    public EnemyTatics enemyTatics;

    #endregion
    public enum BehaviourType
    {
        Agressive,
        Normal,
        Cautious
    }

    public BehaviourType currentBehaviour;

    public void ExecuteCommands(Player player, float closeRangeDistance, float midRangeDistance)
    {
        switch (enemyTatics)
        {
            case (EnemyTatics.Engage):
                Vector3 dir = player.transform.position - transform.position;
                if (Vector2.Distance(transform.position, player.transform.position) > closeRangeDistance)
                {
                    self.enemyMovement.CheckFacingDirection(dir);
                    //Move to player
                    self.enemyMovement.Move(dir);
                }
                else
                {
                    Debug.Log("Player in range");
                    //Player is in range
                    self.enemyMovement.OnAttackAction();
                }
                break;
            case (EnemyTatics.KeepDistance):
                Vector3 _dir = player.transform.position - transform.position;
                if (Vector2.Distance(transform.position, player.transform.position) < midRangeDistance)
                {
                    self.enemyMovement.CheckFacingDirection(-_dir);
                    //Distance from player
                    self.enemyMovement.Move(-_dir);
                }
                break;
            case (EnemyTatics.Wait):
                self.enemyMovement.StopMove();
                break;
        }
    }

    public void ListenCommands(Player player)
    {
        switch (currentBehaviour)
        {
            case (BehaviourType.Agressive):
                if (!player.characterActions.isKnockdown())
                {
                    ChangeEnemyTatic(EnemyTatics.Engage);
                }
                else if(player.characterActions.isKnockdown())
                {
                    ChangeEnemyTatic(EnemyTatics.Wait);
                }
                break;
            case (BehaviourType.Normal):
                if (!player.characterActions.isKnockdown())
                {
                    ChangeEnemyTatic(EnemyTatics.Engage);
                }
                else if(player.characterActions.isKnockdown())
                {
                    ChangeEnemyTatic(EnemyTatics.KeepDistance);
                }
                break;
            case (BehaviourType.Cautious):
                if (!player.characterActions.isKnockdown())
                {
                    ChangeEnemyTatic(EnemyTatics.KeepDistance);
                }
                break;
        }
    }
    public void ChangeEnemyTatic(EnemyTatics newTatic)
    {
        enemyTatics = newTatic;
    }
}
