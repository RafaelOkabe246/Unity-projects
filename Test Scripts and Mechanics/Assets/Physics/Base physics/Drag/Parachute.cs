using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : MonoBehaviour
{
    private Rigidbody rig;
    public bool activeParachute;
    

    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (activeParachute)
        {
            rig.drag = 1.784f;
        }
        else
        {
            rig.drag = 0.1784f;
        }
            

    }

    float dragValue()
    {
        //return 14.27f / 80f;
        return rig.mass * Physics.gravity.y / rig.velocity.y;
    }
}
