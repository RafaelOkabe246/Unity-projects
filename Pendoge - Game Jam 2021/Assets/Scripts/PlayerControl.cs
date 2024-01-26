using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tripulantes
{
    Fortão,
    Excapitão,
    Marinheiro,
    Corsário
}

public class PlayerControl : MonoBehaviour
{
    public MainGameController _MainGameController;

    [Header("Inventory and items")]
    public Inventory _Inventory;
    public InventorySlot SelectedSlot;
    public Item SelectedItem;

    [Header("Tripulantes")]
    public Tripulante SelectedTripulante;
    //public Tripulantes TripulantesSelector;

    public DialoguesStore PlayerDialoguesStore;

    private void Start()
    {
        PlayerDialoguesStore = this.GetComponent<DialoguesStore>();
    }
    public void UseItem(Tripulante tripulante, Item item, InventorySlot inventorySlot)
    {
        if (inventorySlot.ItemQuantity > 0) 
        {
            //If the item + status affected is not greater than 10
            
            UsingItem(item, tripulante, inventorySlot);


        }
        else
        {
            return;
        }
    }

    public void UsingItem(Item item, Tripulante tripulante, InventorySlot inventorySlot)
    {
        switch (item._Tipo_De_Status)
        {
            case (Tipo_de_status.Hungry):
                tripulante.Hungry = AffectStatus(item.StatusAffectValue, tripulante.Hungry, inventorySlot);
                break;

            case (Tipo_de_status.Thirst):
                tripulante.Thirst = AffectStatus(item.StatusAffectValue, tripulante.Thirst, inventorySlot);
                break;

            case (Tipo_de_status.Sanity):
                tripulante.Sanity = AffectStatus(item.StatusAffectValue, tripulante.Sanity, inventorySlot);
                break;
        }

    }

    public float AffectStatus(int AffectPoints, float StatusType, InventorySlot inventorySlot)
    {
        float result = StatusType + AffectPoints;
        if (result > 5)
        {
            return StatusType;
        }
        else
        {
            inventorySlot.ItemQuantity = inventorySlot.ItemQuantity - 1;
            inventorySlot.UpdateItemsQuantity();
            SelectedItem = null;
            return result;
        }
    }

    public float AffectStatus(int AffectPoints, float StatusType)
    {
        float result = StatusType + AffectPoints;
        if (result > 10)
        {
            return StatusType;
        }
        else
        {
            return result;
        }
    }

    public void TripulanteSelecionado(Tripulante tripulante)
    {
        SelectedTripulante = tripulante;
    }

    public void SelectingItemSlot(InventorySlot inventorySlot)
    {
        //Selected the clicked slot
        //SelectedSlot = inventorySlot;
        if (inventorySlot.ItemQuantity > 0)
        {
            SelectedItem = inventorySlot.ReferenceItem;
            SelectedSlot = inventorySlot;
        }
        else
        {
            _MainGameController._DialogueManager.EnterDialogueMode(PlayerDialoguesStore.dialogueText);
        }
    }
}
