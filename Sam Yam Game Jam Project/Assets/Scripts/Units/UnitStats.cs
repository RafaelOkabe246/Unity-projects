using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Units/UnitStats")]
public class UnitStats : ScriptableObject
{
    [SerializeField] private int AttackValue;
    [SerializeField] private int DefenseValue;
    //[SerializeField] private int MoralValue;
    [SerializeField] private int SpeedValue;
    [SerializeField] private int UnitLife;
    [SerializeField] private int ChargeValue;

    public int Attack { get => AttackValue; set => AttackValue = value; }
    public int Defense { get => DefenseValue; set => DefenseValue = value; }
    //public int Moral { get => MoralValue; set => MoralValue = value; }
    public int Speed { get => SpeedValue; set => SpeedValue = value; }
    public int HP { get => UnitLife; set => UnitLife = value; }
    public int Charge { get => ChargeValue; set => ChargeValue = value; }


}


