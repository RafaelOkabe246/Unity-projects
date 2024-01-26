using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour, IInteractable
{
    [SerializeField] protected Animator objAnim;
    public Vector3 initialPosition;



    protected virtual void OnEnable()
    {
        initialPosition = transform.localPosition;
        transform.localPosition = initialPosition;
    }

    public virtual void Event()
    {

    }

    public virtual void TriggerEvent()
    {

    }
}
