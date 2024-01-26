using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventObject : ObjectTile, IInteractable
{
    public float triggerTime;   
    public GameEvent TriggerEvent;

    public override void Initialize()
    {
        currentTile = levelGrid.ReturnBattleTile(currentGridPosition);
        currentTile.AddInteractableObject(this);
        transform.position = currentTile.transform.position;
    }
    

    public virtual void SetCountdownInteractableEvent(bool _hasPlayer)
    {
        if (_hasPlayer)
        {
            StopCoroutine(nameof(CountdownInteractableEvent));
            StartCoroutine(nameof(CountdownInteractableEvent));
        }
        else
        {
            StopCoroutine(nameof(CountdownInteractableEvent));
        }
    }

    protected virtual IEnumerator CountdownInteractableEvent()
    {
        yield return new WaitForSeconds(triggerTime);
        InteractionEvent();
    }

    public virtual void InteractionEvent(object _data)
    {
        TriggerEvent.Raise(this, _data);
    }

    public virtual void InteractionEvent()
    {
        TriggerEvent.Raise(this, "");

    }
}
