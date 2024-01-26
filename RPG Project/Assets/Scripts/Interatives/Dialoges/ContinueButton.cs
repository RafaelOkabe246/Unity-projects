using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ContinueButton : MonoBehaviour
{
    public UnityEvent ButtonClick;

    void Awake()
    {
        if(ButtonClick == null)
        {
            ButtonClick = new UnityEvent();
        }
    }

    void OnMouseDown()
    {
        
    }

    void OnMouseUp()
    {
        Debug.Log("Is working");
        ButtonClick.Invoke();
    }

    public void Contacting_the_DialogueBox()
    {

    }
}
