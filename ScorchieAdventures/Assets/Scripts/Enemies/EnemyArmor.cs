using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * An enemy that can resist the player's normal attack, requires a condition to break its armor. Has the same behaviour of the patrol enemy
*/

public class EnemyArmor : EnemyPatrol
{
    [SerializeField] private bool hasArmor;

    public override void TakeDamage()
    {
        if (!hasArmor)
        {
            base.TakeDamage();

        }
    }
}
