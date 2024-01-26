using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield Upgrade", menuName = "Upgrades/Shield upgrade")]
public class ShieldUpgrade : Upgrade
{
    public AbilityHolder abilityHolder;
    
    public int Test()
    {
        return 2;
    }

    public override void TriggerEffect()
    {

    }

    public override void TriggerEffect(Character self)
    {
        Debug.Log("RFWFRFEWF");

    }

    public override int TriggerEffect(Character owner, Character target, int damageToApply)
    {
        return -damageToApply;
    }

    public override void TriggerEffect(Character owner, Character target)
    {
        throw new System.NotImplementedException();
    }
}

