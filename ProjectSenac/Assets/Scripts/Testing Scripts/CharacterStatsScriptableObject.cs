using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName ="Test scriptable objects/CharacterStats")]
public class CharacterStatsScriptableObject : ScriptableObject
{
    [Header("Status")]

    [SerializeField] private string Name;
    [SerializeField] private float maxHP;
    [SerializeField] private float baseAttackPoints;

    public string CharName { get => Name; set => Name = value; }
    public float MaxLife { get => maxHP; private set => maxHP = value; }

    public float AttackPoints { get => baseAttackPoints; private set => baseAttackPoints = value; }
}
