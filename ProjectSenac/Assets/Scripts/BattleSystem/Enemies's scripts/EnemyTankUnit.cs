using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTankUnit : EnemyUnit
{
    #region IA_logic

    public override void IA_Logic()
    {
        base.IA_Logic();
        

    }
    #endregion

    #region Autohealing_functions
    public void Autohealing()
    {
        //1. Get the healing skill
        //2. Mark itself as target
        //3. Execute the function when start the Enemy turn act

        for(int i = 0; i < unitStat.characterSkills.Length; i++)
        {
            if(unitStat.characterSkills[i].name == "Autohealing")
            {
                bs.addSkillEnemy(unitStat.characterSkills[i], this.gameObject.GetComponent<Unit>());
                Debug.Log("Autohealing skill added");
                break;
            }
        }

    }
    #endregion

    #region Attack_functions
    //Here there are many attack funcitions. 
    public void AttackingRandom()
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
                Debug.Log("Random attack skill added");
                break;
            }

        }
    }


    #endregion


}
