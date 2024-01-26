using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivesManager : MonoBehaviour
{
    //This script is responsible to manage the phase's objective, it can be capture a town or eliminate all the enemdies

    public static ObjectivesManager instance;

    private void Awake()
    {
        instance = this;
    }

}
