using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueObject : TriggerEventObject
{

    public NarrationLine narrationLine;


    public override void InteractionEvent(object _data)
    {
        TriggerEvent.Raise(this, _data);
    }

    public override void InteractionEvent()
    {
        TriggerEvent.Raise(this, "");
    }
}
