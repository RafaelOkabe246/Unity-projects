using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum AbilityStates
{
    ReadyToUse = 0,
    Casting = 1,
    Cooldown = 2
}
public class AbilityHolder : MonoBehaviour
{
    public Character owner;

    public Upgrade ability;

    public AbilityStates currentAbilityState = AbilityStates.ReadyToUse;

    //public UnityEvent OnTriggerAbility;
    


    public void TriggerAbility()
    {
        if (currentAbilityState != AbilityStates.ReadyToUse)
            return;

        //if (!CharacterIsOnAllowedState())
        //  return;

        StartCoroutine(HandleAbilityUsage_CO());
        
        /*
        switch (ability.upgradeType)
        {
            case (UpgradeType.OnApplicationDefense):
                ability.TriggerEffect(owner);
                break;
            case (UpgradeType.OnApplicationAttack):
                break;
        }
        */
    }

    
    private IEnumerator HandleAbilityUsage_CO()
    {
        Debug.Log("Triggered ability fewefe");
        currentAbilityState = AbilityStates.Casting;
        

        yield return new WaitForSeconds(ability.castingTime);

        
        switch (ability.upgradeType)
        {
            case (UpgradeType.Defense):
                ability.TriggerEffect(owner);
                break;
            case (UpgradeType.Attack):
                break;
        }    
        
        currentAbilityState = AbilityStates.Cooldown;

        //OnTriggerAbility?.Invoke();

        if (ability.hasCoolDown)
        {
            StartCoroutine(HandleAbilityCooldown_CO());
        }

    }

    private IEnumerator HandleAbilityCooldown_CO()
    {
        yield return new WaitForSeconds(ability.coolDown);

        currentAbilityState = AbilityStates.ReadyToUse;

    }
}
