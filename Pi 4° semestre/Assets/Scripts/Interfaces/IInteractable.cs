using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    public void InteractionEvent(object _data);
    public void InteractionEvent();
}
