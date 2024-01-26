using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Increase max hp upgrade", menuName = "Upgrades/Increase HP Upgrade")]
public class IncreaseHpMaxUpgrade : Upgrade
{
    public int increasePoints;

    public override void TriggerEffect()
    {

    }

    public override void TriggerEffect(Character self)
    {
        self.statsComponent.maxHP += increasePoints;
        self.statsComponent.HP += increasePoints;
    }

    public override void TriggerEffect(Character owner, Character target)
    {

    }

    public override int TriggerEffect(Character owner, Character target, int damageToApply)
    {
        throw new System.NotImplementedException();
    }
}
