using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Ink.Runtime;

public enum StatusType
{
    Hungry,
    Thirst,
    Sanity,
    Autority,
    None
}

public enum ItemType
{
    Comida,
    Agua,
    None
}

public enum EventAffectType
{
    ItemEvent,
    StatusEvent,
    StatusItemEvent
}

public enum AffectedCharacters
{
    One,
    All,
    OneAndAll,
    None
}

public enum EventType
{
    ChoiceEvent,
    NormalEvent,
    NarrativeEvent
}

[CreateAssetMenu(fileName = "New event", menuName = "Events/Event")]

public class Event : ScriptableObject
{

    new public string name = "New event";

    public TextAsset EventText;

    public AffectedCharacters _AffectedCharacters;

    [Header("Tipo de efeito")]
    public EventAffectType _EventAffectType;

    [Header("Consequences variables")]
    public StatusType _StatusType; 
    public int StatusAffectValue;

    [Header("Items variables")]
    public ItemType _ItemType;
    public int GainedItems;
    public Item EventItemReference;

    [Header("Tipo de evento")]
    public EventType _EventType;

    public virtual void PlayStatusEvent(Event _event, Tripulante tripulante, InventorySlot[] inventorySlots)
    {
        switch (_event._StatusType)
        {
            case (StatusType.Hungry):
                tripulante.Hungry = tripulante.AffectStatus(_event.StatusAffectValue, tripulante.Hungry);
                break;

            case (StatusType.Thirst):
                tripulante.Thirst = tripulante.AffectStatus(_event.StatusAffectValue, tripulante.Thirst);
                break;

            case (StatusType.Sanity):
                tripulante.Sanity = tripulante.AffectStatus(_event.StatusAffectValue, tripulante.Sanity);
                break;
        }
    }

    public virtual void PlayInventoryEvent(Event _event, InventorySlot[] inventorySlots)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].ReferenceItem == _event.EventItemReference)
            {
                //Here is the main fuction
                inventorySlots[i].ItemQuantity = inventorySlots[i].ItemQuantity + _event.GainedItems;
                inventorySlots[i].UpdateItemsQuantity();
                break;
            }
        }
    }
}
