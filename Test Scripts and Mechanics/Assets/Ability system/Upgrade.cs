using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    //OnApplication, //New mechanics abilities
    OnApplicationDefense, //Affects the attack
    OnApplicationAttack, //Affects the take damage
    OnConsume //Increase stats
}
public abstract class Upgrade : ScriptableObject
{
    public UpgradeType upgradeType;

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

    public abstract void ApplyEffect();
}
