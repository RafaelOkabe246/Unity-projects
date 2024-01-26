using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game event")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> gameEventListeners = new List<GameEventListener>();
    
    public void Raise( object data)
    {
        for (int i = 0; i < gameEventListeners.Count; i++)
        {
            gameEventListeners[i].OnEventRaised( data);
        }
    }
    public void RegisterListener(GameEventListener listener)
    {
        if (!gameEventListeners.Contains(listener))
        {
            gameEventListeners.Add(listener);
        }
    }
    public void UnregisterListener(GameEventListener listener)
    {
        if (gameEventListeners.Contains(listener))
        {
            gameEventListeners.Remove(listener);
        }
    }
}
