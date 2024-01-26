using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Units/UnitSkill")]

public class Skills : ScriptableObject
{
    [SerializeField] private string SkillName;
    [SerializeField] private int SkillCost;
    [SerializeField] private Sprite SkillIcon;
    [SerializeField] private int Target; //0 to self, 1 to player, 2 to enemy, 3 to all players, 4 to all enemies
    [SerializeField] private float Damage; //value that will add or subtract the player's life
    [SerializeField] private float Accuracy;
    [SerializeField] private float Critical; //value between 0 and 100
    [SerializeField] private int EffectDuration; //only if it is a buff or a debuff

    public string Name { get => SkillName; private set => SkillName = value; }
    public int Cost { get => SkillCost; private set => SkillCost = value; }
    
    public Sprite Icon { get => SkillIcon; private set => SkillIcon = value; }

    public int skillTargetType { get => Target; private set => Target = value; }

    public float damage { get => Damage; private set => Damage = value; }

    public float accuracyRate { get => Accuracy; private set => Accuracy = value; }

    public float criticalRate { get => Critical; private set => Critical = value; }

    public int effectDuration { get => EffectDuration; private set => EffectDuration = value; }
}
