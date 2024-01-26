using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class EnemyUnit : Unit
{
    public int ID;
    //The HUD of the enemies will work in a different way, so for now I will not focus on that

    public bool HasActed; //Check if the enemy already did an action;

    protected BattleSystem bs;


    #region SkillsPlay
    public virtual void IA_Logic()
    {
        //Access the battle systems
        bs = FindObjectOfType<BattleSystem>();

    }

    public virtual void DoNothing()
    {
        for (int i = 0; i < unitStat.characterSkills.Length; i++)
        {
            if (unitStat.characterSkills[i].name == "SkipTurn")
            {
                //Find the skill named "SkipTurn"
                bs.addSkillEnemy(unitStat.characterSkills[i], null);
                break;
            }
        }
    }

    #endregion

    
}
