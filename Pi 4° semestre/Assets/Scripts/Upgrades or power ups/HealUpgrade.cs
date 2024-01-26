using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal Upgrade", menuName = "Upgrades/Heal upgrade")]
public class HealUpgrade : Upgrade
{
    public int increasePoints;

    public override void TriggerEffect()
    {

    }


    public override void TriggerEffect(Character self)
    {
        self.statsComponent.HP += increasePoints;
        if (self.statsComponent.HP > self.statsComponent.maxHP)
        {
            self.statsComponent.HP = self.statsComponent.maxHP;
        }

    }

    public override int TriggerEffect(Character owner, Character target, int damageToApply)
    {
        throw new System.NotImplementedException();
    }

    public override void TriggerEffect(Character owner, Character target)
    {
        throw new System.NotImplementedException();
    }
}
