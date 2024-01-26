using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : interactiveObject
{
    public bool isOpen;
    public UnityEvent noKeyDialogueTrigger;

    public override void PlayEvent(Player player)
    {
        if (isOpen)
        {
            objectEvent.Invoke();
        }
        else if (!isOpen && player.hasKey)
        {
            isOpen = true;
            player.hasKey = false;
        }
        else if (!isOpen && !player.hasKey)
        {
            Debug.Log("Você precisa de uma chave!");
            noKeyDialogueTrigger.Invoke();
        }
    }


}
