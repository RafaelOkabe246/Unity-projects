using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourController : MonoBehaviour
{
    private Enemy enemy;
    private float timerToChangeBehaviour;

    private void Start()
    {
        enemy = GetComponent<Enemy>();

        CallChangeBehaviour();
    }

    private void Update()
    {
        timerToChangeBehaviour += Time.deltaTime;

    }

    public void CallChangeBehaviour()
    {
        InvokeRepeating(nameof(ChangeEnemiesBehaviour), 0, Random.Range(2f, 4f));
    }

    void ChangeEnemiesBehaviour()
    {
        timerToChangeBehaviour = 0;
        //Checks enemies behaviour state

        int attackChance = Random.Range(0, 5);
        if (enemy.gameObject.activeSelf)
        {
            if (attackChance <= enemy.enemyBehaviourTree.agressivePoints) //Attack
            {
                int attackRangeChance = Random.Range(0, 10);

                if(attackRangeChance < enemy.enemyBehaviourTree.chanceToAttackRange)
                    enemy.characterActions.OnChangeBehaviourState(BehaviourState.EngageRange);
                else if (attackRangeChance > enemy.enemyBehaviourTree.chanceToAttackRange)
                    enemy.characterActions.OnChangeBehaviourState(BehaviourState.Engage);
            }
            else if (attackChance > enemy.enemyBehaviourTree.agressivePoints) //Follow
            {
                enemy.characterActions.OnChangeBehaviourState(BehaviourState.Follow);
            }
        }
        
    }
}
