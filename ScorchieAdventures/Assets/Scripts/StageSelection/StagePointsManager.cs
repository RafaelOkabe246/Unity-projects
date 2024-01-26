using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePointsManager : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Start CALLED!!!");
        for (int i = 0; i < transform.childCount; i++) 
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

}
