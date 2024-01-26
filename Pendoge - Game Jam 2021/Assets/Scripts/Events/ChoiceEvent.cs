using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using Ink.Runtime;




[CreateAssetMenu(fileName = "New event", menuName = "Events/Choice Event")]

public class ChoiceEvent : Event
{
    public Event Option_0;
    public Event Option_1;

    new public string name = "New choice event";


}
