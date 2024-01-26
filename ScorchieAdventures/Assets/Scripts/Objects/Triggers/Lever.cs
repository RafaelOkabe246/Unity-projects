using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Can be activated just once
public class Lever : TriggerObject
{
    //Checks if is already activate
    public bool hasChangeState;
    public GameObject fireParticles;

    private void Start()
    {
        fireParticles.SetActive(false);
        hasChangeState = false;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        fireParticles.SetActive(false);
        hasChangeState = false;
    }

    public override void TriggerEvent()
    {
        base.TriggerEvent();
        if (!hasChangeState)
        {
            fireParticles.SetActive(true);
            hasChangeState = true;
            Event();
        }
    }


}
