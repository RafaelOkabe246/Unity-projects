using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    public GameStateManager stateManager;

    public void SetStateManager(GameStateManager _stateManager)
    {
        stateManager = _stateManager;
    }

    public abstract void EnterState(GenericObject context);

    public abstract void UpdateState(GenericObject context);

    public virtual void OnTriggerEnter(GenericObject context, Collider2D coll)
    {

    }

    public virtual void OnCollisionEnter(GenericObject context, Collision2D coll)
    {

    }
}
