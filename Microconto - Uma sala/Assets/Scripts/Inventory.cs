using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New inventory", menuName = "Inventory")]
public class Inventory : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();
    public void AddItem(ItemObject _item, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++)
        {
            if(Container[i].Item == _item)
            {
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if (!hasItem)
        {
            Container.Add(new InventorySlot(_item, _amount));
        }

    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject Item;
    public int Amount;
    public InventorySlot(ItemObject _item, int _amount)
    {
        Item = _item;
        Amount = _amount;
    }
    public void AddAmount(int value)
    {
        Amount += value;
    }

}
