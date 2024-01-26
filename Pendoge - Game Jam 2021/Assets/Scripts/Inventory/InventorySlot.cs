using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    //First item in slot
    //This item is the reference for the rest of the objects
    public Item ReferenceItem;

    public Image icon;

    public int ItemQuantity;
    public TextMeshProUGUI ItemQuantityText;

    private void Start()
    {
        UpdateItemsQuantity();

    }

    public void AddItem(Item newItem)
    {
        if(newItem == ReferenceItem)
        {
            //item = newItem;
            UpdateItemsQuantity();
            //items.Add(newItem);

        }
        else if (newItem != ReferenceItem)
        {
            UpdateItemsQuantity();
            return;
        }

    }

    public void UpdateItemsQuantity()
    {
        //ItemQuantity = items.Count;

        ItemQuantityText.text = "" + ItemQuantity + "";
        icon.sprite = ReferenceItem.icon;
        icon.enabled = true;
    }


}
