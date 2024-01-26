using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield Upgrade", menuName = "Upgrades/Shield upgrade")]
public class UpgradeShield : Upgrade
{
    public override void ApplyEffect()
    {
        Debug.Log("Effect works");
    }
}
