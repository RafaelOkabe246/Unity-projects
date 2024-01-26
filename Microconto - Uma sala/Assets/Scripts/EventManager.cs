using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EventManager 
{
    //First act events
    public bool HasKey;
    public bool HasExitHouse;
    public bool HasFollowShadownGirl;

    public EventManager(CharacterEventSystem player)
    {
        HasKey = player.HasKey;
        HasExitHouse = player.HasExitHouse;
        HasFollowShadownGirl = player.HasFollowShadownGirl;
    }
}
