using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerActions : MonoBehaviour
{

    public static PlayerActions instance;

    private void Awake()
    {
        instance = this;
    }

    //Navigation press
    public  Action<int> dirNavigation;


    public  Action select;
}
