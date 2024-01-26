using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] protected State currentState;
    public GenericObject genericObject;

    public void SetState(State _state)
    {
        currentState = _state;
        currentState.SetStateManager(this);
    }

    public void SwitchState(State newState)
    {
        SetState(newState);
        currentState.EnterState(genericObject);
    }

    private void Awake()
    {
        var sR = new StatesReference();
        SetState(sR.burningState);
    }

    protected virtual void Start()
    {
        currentState.EnterState(genericObject);
    }

    protected virtual void Update()
    {
        GenericObject _genericObject = genericObject;
        currentState.UpdateState(_genericObject);
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.OnTriggerEnter(genericObject, collision);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter(genericObject, collision);
    }
}