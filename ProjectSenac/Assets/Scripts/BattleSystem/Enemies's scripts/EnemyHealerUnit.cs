using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyHealerUnit : EnemyUnit
{
    public override void IA_Logic()
    {
        base.IA_Logic();
        HealAll();
    }

    #region Healers_functions

    public void HealAll()
    {
        Debug.Log("Skill added");
        bool enemiesLifeBellowMax = false;

        for(int i = 0; i < bs.Enemies.Length; i++)
        {
            if(bs.Enemies[i].HP < bs.Enemies[i].unitStat.UnitMaxHP)
            {
                enemiesLifeBellowMax = true;
                break;
            }
        }
 

        if (enemiesLifeBellowMax == true)
        {
            for (int i = 0; i < unitStat.characterSkills.Length; i++)
            {
                if (unitStat.characterSkills[i].name == "HealAll")
                {
                    bs.addSkillEnemy(unitStat.characterSkills[i], this.GetComponent<EnemyUnit>());
                }
            }
        }
        else
        {
            for (int i = 0; i < unitStat.characterSkills.Length; i++)
            {
                if (unitStat.characterSkills[i].name == "SkipTurn")
                {
                    bs.addSkillEnemy(unitStat.characterSkills[i], this.GetComponent<EnemyUnit>());
                }
            }
        }
    }

    public void SelfHeal()
    {

    }

    public void SingleHeal()
    {

    }

    #endregion
}

