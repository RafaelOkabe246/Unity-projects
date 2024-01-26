using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Default,
    Narrativo
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject Prefab;
    public ItemType Type;
    [TextArea(15,20)]
    public string Description;

}
