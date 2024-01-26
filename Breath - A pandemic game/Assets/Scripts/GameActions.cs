using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameActions : MonoBehaviour
{
    public static GameActions instance;

    private void Awake()
    {
        instance = this;
    }

}
