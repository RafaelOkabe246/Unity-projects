using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEventsCharacter : MonoBehaviour
{
    public CharacterEventSystem PlayerEvents;

    private void Start()
    {
        PlayerEvents = GameObject.FindGameObjectWithTag(Tags.PlayerEvents).GetComponent<CharacterEventSystem>();
    }

    public void HasKey_true()
    {
        PlayerEvents.HasKey = true;
    }
    public void HasExitHouse_true()
    {
        PlayerEvents.HasExitHouse = true;
    }
    public void HasFollowShadownGirl_true()
    {
        PlayerEvents.HasFollowShadownGirl = true;
    }

}
