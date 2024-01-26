using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    [SerializeField] Event _event;
    [SerializeField] UnityEvent _unityEvent;

    /*
    void Awake() => _event.Register(gameEventListener: this);

    void OnDestroy() => _event.Deregister(gameEventListener: this);

    public void RaiseEvent() => _unityEvent.Invoke();
    */
    
}
