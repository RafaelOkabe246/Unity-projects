using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable  
{
    public void TakeNormalHit(int damage);
    public void TakeStrongHit(int damage);

    public void ReceiveAttack(int damage);
}
