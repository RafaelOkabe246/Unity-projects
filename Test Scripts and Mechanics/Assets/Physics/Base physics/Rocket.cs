using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private Rigidbody rig;

    private const float THRUST_FORC_MAGNITUDE = 30000; //Força de impulso

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rig.AddForce(Vector3.up * THRUST_FORC_MAGNITUDE);
    }
}
