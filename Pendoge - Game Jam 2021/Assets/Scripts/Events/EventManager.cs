using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventManager : MonoBehaviour
{
    [SerializeField]
    private MainGameController GameController;

    private void Start()
    {
        GameController = GetComponentInParent<MainGameController>();
    }

    [SerializeField] Event[] Events;

    public void PlayRandomEvent()
    {
        //Event randoness
        int randonessEvent = Random.Range(0, Events.Length);

        //Event that affects one character
        int tripulanteRandom = Random.Range(0, GameController._TripulantesStatusController.Tripulantes.Length);

        //Check the event
        CheckEvent(Events[randonessEvent], GameController._TripulantesStatusController.Tripulantes[tripulanteRandom], GameController._PlayerControl._Inventory.InventorySlots);
    }

    //Play the event
    void CheckEvent(Event _event, Tripulante tripulante, InventorySlot[] inventorySlots)
    {
        GameController._DialogueManager.
            EnterDialogueMode(_event.EventText);

        //Check the type of event
        switch (_event._EventType)
        {
            case (EventType.ChoiceEvent):
                PlayChoiceEvent(_event, tripulante, inventorySlots);
                break;

            case (EventType.NormalEvent):
                PlayNormalEvent(_event, tripulante, inventorySlots);
                break;

            case (EventType.NarrativeEvent):
                break;
        }
        
    }

    void PlayNormalEvent(Event _event, Tripulante tripulante, InventorySlot[] inventorySlots)
    {
        switch (_event._EventAffectType)
        {
            //Adiciona ou tira itens do inventório
            case (EventAffectType.ItemEvent):
                //1.Will add an amount of an specific type of item to a slot
                //2.Check the inventory slots for the specific item reference
                //3.Add more itens according to the event
                _event.PlayInventoryEvent(_event, inventorySlots);

                break;

            //Afeta diretamente os status de um tripulante ou de todos
            case (EventAffectType.StatusEvent):
                switch (_event._AffectedCharacters)
                {
                    case (AffectedCharacters.All):
                        foreach (Tripulante _tripulante in GameController._TripulantesStatusController.Tripulantes)
                        {
                            _event.PlayStatusEvent(_event,_tripulante, inventorySlots);
                        }
                        break;

                    case (AffectedCharacters.One):
                        _event.PlayStatusEvent(_event,tripulante, inventorySlots);
                        break;
                }
                break;
        }
    }

    void PlayNarrativeEvent(Event _event, Tripulante tripulante, InventorySlot[] inventorySlots)
    {

    }

    void PlayChoiceEvent(Event _event, Tripulante tripulante, InventorySlot[] inventorySlots)
    {
        
    }



}
