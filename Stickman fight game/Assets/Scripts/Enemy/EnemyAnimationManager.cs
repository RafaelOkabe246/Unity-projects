using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : CharacterAnimationManager
{
    public void CallAttack()
    {
        anim.SetTrigger("Attacking");
    }
}
