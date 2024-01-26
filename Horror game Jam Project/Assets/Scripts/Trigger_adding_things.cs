using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_adding_things : MonoBehaviour
{
    public GameObject Object;



    private void Awake()
    {
        Object.gameObject.SetActive(false);
    }

    public void Adding_objects()
    {
        Object.gameObject.SetActive(true);  
    }

}
