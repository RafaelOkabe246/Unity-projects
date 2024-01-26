using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVampireUnit : EnemyAttackUnit
{
    public override void IA_Logic()
    {
        bs = FindObjectOfType<BattleSystem>();
        AttackingRandom();
    }

    #region Steal_life_attack
    public override void AttackingRandom()
    {
        //1. Select random unity
        //2. Mark the target
        //3. Execute the function when start the Enemy turn act
        //Choose a target
        int randomPlayer = Random.Range(0, bs.Players.Length);


        for (int i = 0; i < unitStat.characterSkills.Length; i++)
        {
            if (unitStat.characterSkills[i].name == "StealLifeAttack")
            {
                //Find the skill named "Attack"
                bs.addSkillEnemy(unitStat.characterSkills[i], bs.Players[randomPlayer]);
                Debug.Log("Skill added");
                break;
            }

        }
    }

    #endregion

}
