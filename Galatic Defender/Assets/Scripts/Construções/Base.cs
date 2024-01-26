using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : Building
{
    public int missles;
    public bool CanShoot;
    public List<Transform> missleSlots;

    private void Update()
    {
        if(isDestroyed == false && missles > 0)
        {
            CanShoot = true;
        }
        else 
        {
            CanShoot = false;
        }
    }

    public void DecreaseMissle(int i)
    {
        missles -= i;
    }
}
