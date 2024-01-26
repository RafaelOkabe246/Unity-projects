using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default", menuName = "Character stats")]
public class CharacterStats : ScriptableObject
{
	[Header("Health")]
	public int maxHP;

	[Space(10)]

	[Header("Movement")]
	public float minMoveSpeed;


	[Space(10)]

	[Header("Combat")]
	public int normalHitDamage;
	public int strongHitDamage;
}
