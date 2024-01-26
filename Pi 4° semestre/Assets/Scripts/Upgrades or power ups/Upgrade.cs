using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    OnApplication, //New mechanics abilities
    EditDamage, //Modifies the applied damage
    Defense, //Affects the attack
    Attack, //Affects the take damage
    Consume //Increase stats
}
public abstract class Upgrade : ScriptableObject
{
    //public void FinishUpgrade() //-> Disable every delegates connected to this upgrade

    public UpgradeType upgradeType;// -> OnApplication, OnConsume 

    public bool doesStack;

    [Header("Shop information")]
    public Sprite icon;
    public string upgradeName;
    public string upgradeDescription;
    public int cost;

    [Header("Cooldown and casting time")]
    public bool hasCoolDown;
    public float coolDown;
    public float castingTime;
    
    [Header("Allowed States")]
    public List<CharacterStates> allowedCharacterStates =
        new List<CharacterStates>() { CharacterStates.Alive };

    public virtual void OnAbilityUpdate(AbilityHolder holder) { }
    public abstract void TriggerEffect(); //For itself
    public abstract void TriggerEffect(Character self); //For itself
    public abstract void TriggerEffect(Character owner, Character target); //For other object

    public abstract int TriggerEffect(Character owner, Character target, int damageToApply); //For other object
}
