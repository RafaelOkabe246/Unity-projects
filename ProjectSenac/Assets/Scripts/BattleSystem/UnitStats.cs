using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName ="Units/UnitStats")]
public class UnitStats : ScriptableObject
{
    [Header("Status")]

    [SerializeField] private string Name;
    [SerializeField] private float maxHP;

    [Header("Skills")]

    [SerializeField] private Skills[] unitSkills; //list of unit's skills

    public string UnitName { get => Name; set => Name = value; }
    public float UnitMaxHP { get => maxHP; private set => maxHP = value; }
    public Skills[] characterSkills { get => unitSkills; private set => unitSkills = value;} //We can't set a new value, we can only get the actual value
    

}
