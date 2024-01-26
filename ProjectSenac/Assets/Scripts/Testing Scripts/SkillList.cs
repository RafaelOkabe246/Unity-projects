using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillList : MonoBehaviour
{
    //This method contains the list of the skill methods

    [SerializeField]
    public Action CurrentAction;

    #region Attack_skills
    public void IndividualAttack()
    {
        float attackPoints = CurrentAction.Emissor.CharStats.AttackPoints;
        CurrentAction.Target.HP -= attackPoints;
    }

    public void GroupAttack()
    {
        float attackPoints = CurrentAction.Emissor.CharStats.AttackPoints;
        foreach (Character target in CurrentAction.Targets)
        {
            target.HP -= attackPoints;
        }
    }
    #endregion


    #region Heal_skills
    public void IndividualHeal()
    {

    }

    public void GroupHeal()
    {

    }
    #endregion

}
