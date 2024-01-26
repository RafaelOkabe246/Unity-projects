using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public enum Tipo_de_status
{
    Hungry,
    Thirst,
    Sanity
}
[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";

    public Sprite icon = null;

    [Header("True == aumenta status; False == diminiu status")]
    public bool ItemType;

    public Tipo_de_status _Tipo_De_Status;

    public int StatusAffectValue;

    private void Awake()
    {
        if (ItemType == true)
        {
            StatusAffectValue *= 1;
        }
        else
        {
            StatusAffectValue *= -1;
        }
    }

}
