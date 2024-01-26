using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossBT : EnemyBT
{
    protected override NodeBehaviourtree SetupTree()
    {
        NodeBehaviourtree root = new Selector(new List<NodeBehaviourtree>
        {
            new Sequence(new List<NodeBehaviourtree>
            {
                new TaskFindPath(this, characterActions, characterActions.OnGetLevelGrid(), player, characterActions.OnGetEnemiesManager()),
                new TaskDelay(this, moveDelay),
                new TaskMovePatrol(this, characterActions, characterActions.OnGetLevelGrid())
            }),
            new Sequence(new List<NodeBehaviourtree>
            {
                new TaskDelay(this, attackDelay),
                new CheckAttackArea(this,characterActions, characterActions.OnGetLevelGrid()),
                new TaskAttack(this,characterActions)
            })
        });
        return root;
    }
}
