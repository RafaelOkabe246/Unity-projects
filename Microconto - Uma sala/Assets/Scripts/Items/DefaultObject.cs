using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New defeault object", menuName = "Inventory/ Items/ Default")]
public class DefaultObject : ItemObject
{
    private void Awake()
    {
        Type = ItemType.Default;
    }
}
