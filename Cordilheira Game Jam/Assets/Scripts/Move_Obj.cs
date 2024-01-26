using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Obj : MonoBehaviour
{
    private Rigidbody rig;
    public float MoveVelocity;
    public float MoveDistance;
    public Transform PointA;
    public Transform PointB;

    private void Start()
    {
        rig = this.GetComponent<Rigidbody>();
        transform.position = PointA.position;
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, PointA.position) < 1f)
        {
            Move(PointB.position.normalized);
        }
        if (Vector3.Distance(transform.position, PointB.position) < 1f)
        {
            Move(PointA.position.normalized);
        }
    }

    void Move(Vector3 direction)
    {
        rig.MovePosition((Vector3)transform.position + (MoveVelocity * direction * Time.deltaTime));
    }

    private void OnDrawGizmos()
    {
        
    }
}
