using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    void Start()
    {
        Destroy(this.gameObject, 0.1f);
    }


    void Update()
    {
        
    }
}
