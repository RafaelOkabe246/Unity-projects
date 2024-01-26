using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TriggerObject : InteractiveObject
{
    public List<InteractiveObject> triggerObjectsEvents;

    public override void Event()
    {
        for (int i = 0; i < triggerObjectsEvents.Count; i++)
        {
            triggerObjectsEvents[i].TriggerEvent();
        }
    }
}
