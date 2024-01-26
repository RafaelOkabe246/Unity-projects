using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBreak : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private float breakerForceMagnitude;

    [SerializeField] private bool isUsingEquilibriumForceWhenBreaking; //Cheat to stop the car

    [SerializeField] private Rigidbody rig;

    public bool _isBreaking;

    private Vector3 stopPosition;

    private PhysicsControls physicsActions;

    private void Awake()
    {
        physicsActions = new PhysicsControls();    
    }

    bool IsCarStopped()
    {
        if (rig.velocity.x <= 0.01f && _isBreaking)
        {
            rig.velocity = Vector3.zero;
            return true;
        }
        else
            return false;
    }

    private void Update()
    {
        if (physicsActions.Car.Breaking.IsPressed())
            _isBreaking = true;

        if (!isUsingEquilibriumForceWhenBreaking)
        {
            if (IsCarStopped())
            {
                stopPosition = rig.position;
                rig.useGravity = false;
            }
            else
            {
                rig.useGravity = true;
            }
        }
    }

    void Breaking(float _breakerForceMagnitude)
    {
        rig.AddForce(Vector3.left * _breakerForceMagnitude);
    }

    private float GetEquelibriumForceMagnitude(float angle)
    {
        //Force = mass * -1 * acceleration * angle 
        return rig.mass * -1 * Physics.gravity.y * Mathf.Sin(-1 * angle);
    }

    private void FixedUpdate()
    {
        if(_isBreaking && !IsCarStopped())
        {
            Breaking(breakerForceMagnitude);
        }
        
        else if (_isBreaking && IsCarStopped())
        {
            if (isUsingEquilibriumForceWhenBreaking)
            {
                Breaking(GetEquelibriumForceMagnitude(Mathf.Deg2Rad *transform.rotation.x));
            }
            else
            {
                rig.position = stopPosition;
            }
        }

    }
}
