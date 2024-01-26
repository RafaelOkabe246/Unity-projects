using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyAttackUnit : EnemyUnit
{
    #region IA_logic
    public override void IA_Logic()
    {
        base.IA_Logic();
        AttackingRandom();
    }

    #endregion


    #region Attack_functions
    //Here there are many attack funcitions. 
    public virtual void AttackingRandom()
    {
        //1. Select random unity
        //2. Mark the target
        //3. Execute the function when start the Enemy turn act
        //Choose a target
        int randomPlayer = Random.Range(0, bs.Players.Length);


        for (int i = 0; i < unitStat.characterSkills.Length; i++)
        {
            if (unitStat.characterSkills[i].name == "Attack")
            {
                //Find the skill named "Attack"
                bs.addSkillEnemy(unitStat.characterSkills[i], bs.Players[randomPlayer]);
                Debug.Log("Skill added");
                break;
            }

        }
    }

    public void AttackingWeakUnit()
    {
        //1. Find the weak unity in the Player list
        //2. Mark the target index
        //3. Execute the function when start the Enemy turn act

        int index = 0;
        float lifeReference = 10;

        for (int i = 0; i < bs.Players.Length; i++)
        {
            if (bs.Players[i].HP < lifeReference)
            {
                lifeReference = bs.Players[i].HP;
                index = i;
            }
        }

        for (int i = 0; i < unitStat.characterSkills.Length; i++)
        {
            if (unitStat.characterSkills[i].name == "Attack")
            {
                //Find the skill named "Attack"
                bs.addSkillEnemy(unitStat.characterSkills[0], bs.Players[index]);
                Debug.Log("Enemy attack weakest");
                break;
            }

        }


    }

    public void AttackStrongerUnit()
    {
        //1. Find the strongest unity in the Player list
        //2. Mark the target
        //3. Execute the function when start the Enemy turn act

        int index = 0;
        float lifeReference = 0;

        for (int i = 0; i < bs.Players.Length; i++)
        {
            if (bs.Players[i].HP > lifeReference)
            {
                lifeReference = bs.Players[i].HP;
                index = i;
            }
        }

        for (int i = 0; i < unitStat.characterSkills.Length; i++)
        {
            if (unitStat.characterSkills[i].name == "Attack")
            {
                //Find the skill named "Attack"
                bs.addSkillEnemy(unitStat.characterSkills[0], bs.Players[index]);
                Debug.Log("Enemy attack strongest");
                break;
            }

        }
    }



    #endregion

}
