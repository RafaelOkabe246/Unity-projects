using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Game Event", menuName = "Event")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise(Component _sender, object _data)
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventRaise(_sender, _data);
        }
    }

    public void RegisterListener(GameEventListener _listener)
    {
        if (!listeners.Contains(_listener))
            listeners.Add(_listener);
    }

    public void UnregisterListener(GameEventListener _listener)
    {
        if (listeners.Contains(_listener))
            listeners.Remove(_listener);
    }
}
