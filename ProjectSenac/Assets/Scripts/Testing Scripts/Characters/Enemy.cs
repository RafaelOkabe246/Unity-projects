using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public int ID;
    //The HUD of the enemies will work in a different way, so for now I will not focus on that

    protected ClassBaseBattleSystem cbs;


    #region SkillsPlay
    public virtual void IA_Logic()
    {
        //Access the battle systems
        cbs = FindObjectOfType<ClassBaseBattleSystem>();

    }

    #endregion
}
