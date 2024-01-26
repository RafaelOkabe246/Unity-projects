using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead_Particle : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        Destroy(gameObject, 2f);
    }
}
