using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StatsComponent : MonoBehaviour
{
    [Header("Upgrades")]
    //Permanent upgrades
    public Dictionary<Upgrade, int> currentUpgrades = new Dictionary<Upgrade, int>();

    [Header("Health")]
    public TextMeshPro lifeText;
    public int HP;
    public int maxHP;
    

}

