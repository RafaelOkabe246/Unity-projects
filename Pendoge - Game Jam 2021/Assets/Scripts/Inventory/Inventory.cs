using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There is more than one inventory");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack; 

   

    #region Inventory slots

    public InventorySlot[] InventorySlots;


    #endregion

    public PlayerControl _PlayerControl;



    public void AddItem(Item item, InventorySlot inventorySlot)
    {
        inventorySlot.AddItem(item);

        if(onItemChangedCallBack != null)
        onItemChangedCallBack.Invoke();
    }

    public void RemoveItem(Item item, List<Item> SelectedItemsList, InventorySlot inventorySlot)
    {
        SelectedItemsList.Remove(item);
        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke(); 
    }
    
}
