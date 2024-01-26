using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Money Upgrade", menuName = "Upgrades/Money Drop")]
public class MoneyDropUpgrade : Upgrade
{
    public override void TriggerEffect()
    {
        CoinsManager.instance.GainCoins();
    }

    public override void TriggerEffect(Character self)
    {
        CoinsManager.instance.GainCoins();
    }

    public override void TriggerEffect(Character owner, Character target)
    {
        throw new System.NotImplementedException();
    }

    public override int TriggerEffect(Character owner, Character target, int damageToApply)
    {
        throw new System.NotImplementedException();
    }
}
