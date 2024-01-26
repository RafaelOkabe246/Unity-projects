using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningState : State
{
    public override void EnterState(GenericObject context)
    {
        Debug.Log("Is Burning");
        SpriteRenderer spr = context.objectActions.OnGetSpriteRenderer();
        spr.color = Color.red;
    }

    public override void OnCollisionEnter(GenericObject context, Collision2D coll)
    {


    }

    public override void OnTriggerEnter(GenericObject context, Collider2D coll)
    {
        GameObject collisionObj = coll.gameObject;
        StatesReference statesReference = new StatesReference();

        if (collisionObj.CompareTag("Water"))
        {
            stateManager.SwitchState(statesReference.weatState);
            Debug.Log("Change state");
        }
    }

    public override void UpdateState(GenericObject context)
    {

    }
}
